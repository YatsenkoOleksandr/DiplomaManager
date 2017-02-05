import { Component, OnInit, ViewChild } from '@angular/core';
import { Response } from '@angular/http';
import { NgForm } from '@angular/forms';
import { DataService } from './data.service';
import { Degree } from './degree';
import { Subscription } from 'rxjs';
import { SelectComponent } from 'ng2-select';
import { FormControl, FormGroup } from '@angular/forms';
import { RequestFormGroup, Request } from './request';
import { SelectItem } from './selectItem';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {
    dAreasList: Array<SelectItem>;
    degrees: Array<SelectItem>;
    teachersList: Array<SelectItem>;

    busy: Subscription;
    @ViewChild('teachersSelect') teachersSelect: SelectComponent;
    requestFGroup: FormGroup;

    constructor(private dataService: DataService) { }

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
            das: new FormControl(''),
            teachers: new FormControl(''),
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            patronymic: new FormControl(''),
            title: new FormControl('')
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

    onSubmit({ value, valid }: { value: RequestFormGroup, valid: boolean }) {
        console.log(value, valid);
    }
}