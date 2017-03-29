import { Component, OnInit, ViewChild } from '@angular/core';
import { Response } from '@angular/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { SelectComponent } from 'ng2-select';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { SelectItem } from '../../shared/selectItem';
import { CustomValidators } from 'ng2-validation';
import { ToastrService } from 'ngx-toastr';

import { DiplomaRequestService } from './diplomarequest.service';
import { Degree } from './degree.model';
import { RequestFormGroup, Request } from './request.model';
import { Capacity } from './capacity.model';
import { PeopleName, NameKind } from '../../shared/peopleName.model';

@Component({
    moduleId: module.id,
    selector: 'diploma-request',
    templateUrl: './diplomarequest.component.html',
    providers: [DiplomaRequestService]
})
export class DiplomaRequestComponent implements OnInit {
    dAreasList: Array<SelectItem>;
    degrees: Array<SelectItem>;
    teachersList: Array<SelectItem>;
    groupsList: Array<SelectItem>;

    firstNames: Array<SelectItem>;
    lastNames: Array<SelectItem>;
    patronymics: Array<SelectItem>;

    busy: Subscription;
    @ViewChild('teachersSelect') teachersSelect: SelectComponent;
    requestFGroup: FormGroup;
    confirmMessage: string;
    capacity: Capacity;

    constructor(private dataService: DiplomaRequestService,  private toastrService: ToastrService) { }

    ngOnInit() {
        this.busy = this.dataService.getDegrees().subscribe((data) => {
            this.setItemsToSelectList(data, "degrees");
        });

        this.busy = this.dataService.getDevelopmentAreas().subscribe((data) => {
            this.setItemsToSelectList(data, "dAreasList");
        });

        this.requestFGroup = new FormGroup({
            das: new FormControl('', Validators.required),
            teachers: new FormControl('', Validators.required),
            degrees: new FormControl('', Validators.required),
            title: new FormControl('', [Validators.required, Validators.minLength(5)]),

            studentFGroup: new FormGroup({
                groups: new FormControl('', Validators.required),
                firstName: new FormControl('', [Validators.required, Validators.minLength(3)]),
                lastName: new FormControl('', [Validators.required, Validators.minLength(3)]),
                patronymic: new FormControl('', [Validators.required, Validators.minLength(3)]),
                email: new FormControl('', [Validators.required, CustomValidators.email])
            })
        });

        this.firstNameSearchChanged("");
        this.lastNameSearchChanged("");
        this.patronymicSearchChanged("");
    }

    dasSelected(event) {
        this.busy = this.dataService.getTeachers(event.id).subscribe((data) => {
            if (this.teachersSelect) {
                let activeItem = this.teachersSelect.activeOption;
                if (activeItem) {
                    this.teachersSelect.remove(activeItem);
                }
            }
            this.setItemsToSelectList(data, "teachersList");
        });
    }

    degreeSelected(event) {
        let degreeid = event.id;

        if (this.requestFGroup.value.teachers) {
            let teacherId = this.requestFGroup.value.teachers[0].id;
            this.busy = this.dataService.getCapacity(degreeid, teacherId).subscribe((data) => {
                this.capacity = data;
            });
        }

        this.busy = this.dataService.getGroups(degreeid).subscribe((data) => {
            this.setItemsToSelectList(data, "degrees");
        });
    }

    firstNameSearchChanged(typedText) {
        this.busy = this.dataService.getStudentNames(typedText, NameKind.FirstName).subscribe((data) => {
            this.setItemsToSelectList(data, "firstNames");
        });
    }

    lastNameSearchChanged(typedText) {
        this.busy = this.dataService.getStudentNames(typedText, NameKind.LastName).subscribe((data) => {
            this.setItemsToSelectList(data, "lastNames");
        });
    }

    patronymicSearchChanged(typedText) {
        this.busy = this.dataService.getStudentNames(typedText, NameKind.Patronymic).subscribe((data) => {
            this.setItemsToSelectList(data, "patronymics");
        });
    }

    onSubmit({ value, valid }: { value: RequestFormGroup, valid: boolean }) {
        if (value && valid) {
            let request = new Request(value.das[0].id, value.teachers[0].id, value.studentFGroup, value.title);
            this.busy = this.dataService.sendRequest(request).subscribe(data => {
                if (data.message) {
                    this.toastrService.success(data.message + ' =)');
                } else {
                    this.toastrService.error('Ошибка подачи заявки =(');
                    console.error(data.errorMessage);
                }
            });
        }
    }

    private setItemsToSelectList<T>(data: Array<T>, selectListName: string) {
        this[selectListName] = new Array<SelectItem>(data.length);
        let index = 0;
        for (let name of data) {
            this[selectListName][index] = new SelectItem(name["id"], name["name"]);
            index++;
        }
    }
}