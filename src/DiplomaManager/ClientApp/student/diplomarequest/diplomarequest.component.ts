﻿import { Component, OnInit, ViewChild } from '@angular/core';
import { Response } from '@angular/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { SelectComponent } from 'ng2-select';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { SelectItem } from '../../shared/selectItem';

import { DiplomaRequestService } from './diplomarequest.service';
import { Degree } from './degree.model';
import { RequestFormGroup, Request } from './request.model';
import { Capacity } from './capacity.model';

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

    busy: Subscription;
    @ViewChild('teachersSelect') teachersSelect: SelectComponent;
    @ViewChild('reqModal') reqModal: ModalComponent;
    requestFGroup: FormGroup;
    confirmMessage: string;
    capacity: Capacity;

    constructor(private dataService: DiplomaRequestService) { }

    ngOnInit() {
        this.busy = this.dataService.getDegrees().subscribe((data) => {
            this.degrees = new Array<SelectItem>(data.length);
            let index = 0;
            for (let degree of data) {
                this.degrees[index] = new SelectItem(degree.id, degree.name);
                index++;
            }
        });

        this.busy = this.dataService.getDevelopmentAreas().subscribe((data) => {
            this.dAreasList = new Array<SelectItem>(data.length);
            let index = 0;
            for (let da of data) {
                this.dAreasList[index] = new SelectItem(da.id, da.name);
                index++;
            }
        });

        this.requestFGroup = new FormGroup({
            das: new FormControl('', Validators.required),
            teachers: new FormControl('', Validators.required),
            degrees: new FormControl('', Validators.required),
            title: new FormControl('', [Validators.required, Validators.minLength(5)]),

            studentFGroup: new FormGroup({
                firstName: new FormControl('', [Validators.required, Validators.minLength(3)]),
                lastName: new FormControl('', [Validators.required, Validators.minLength(3)]),
                patronymic: new FormControl('', [Validators.required, Validators.minLength(3)]),
                email: new FormControl('', [Validators.required, Validators.pattern("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A - Za - z0 - 9 -]+(\\.[A - Za - z0 - 9] +)*(\\.[A - Za - z]{2,})$")])
            })
        });
    }

    dasSelected(event) {
        this.busy = this.dataService.getTeachers(event.id).subscribe((data) => {
            if (this.teachersSelect) {
                let activeItem = this.teachersSelect.activeOption;
                if (activeItem) {
                    this.teachersSelect.remove(activeItem);
                }
            }
            this.teachersList = new Array<SelectItem>(data.length);
            let index = 0;
            for (let teacher of data) {
                this.teachersList[index] = new SelectItem(teacher.id, teacher.toString());
                index++;
            }
        });
    }

    degreeSelected(event) {
        var degreeid = event.id;
        var teacherId = this.requestFGroup.value.teachers[0].id;

        this.busy = this.dataService.getCapacity(degreeid, teacherId).subscribe((data) => {
            this.capacity = data;
        });
    }

    onSubmit({ value, valid }: { value: RequestFormGroup, valid: boolean }) {
        if (value && valid) {
            let request = new Request(value.das[0].id, value.teachers[0].id, value.studentFGroup, value.title);
            this.busy = this.dataService.sendRequest(request).subscribe(data => {
                if (data.message) {
                    this.confirmMessage = data.message + " =)";
                } else {
                    this.confirmMessage = data.errorMessage;
                }
                this.reqModal.open();
            });
        }
    }
}