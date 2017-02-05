import { Component, OnInit, ViewChild } from '@angular/core';
import { Response } from '@angular/http';
import { NgForm } from '@angular/forms';
import { DataService } from './data.service';
import { Degree } from './degree';
import { Subscription } from 'rxjs';
import { SelectComponent } from 'ng2-select';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {
    teachers: Array<SelectItem>;
    degrees: Array<SelectItem>;
    das: Array<SelectItem>;
    firstName: string;
    lastName: string;
    patronymic: string;
    title: string;

    busy: Subscription;
    @ViewChild('teachersSelect') teachersSelect: SelectComponent;

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
            this.das = new Array<SelectItem>(data.length);
            let index = 0;
            for (let da of data) {
                this.das[index] = new SelectItem(da.id, da.name);
                index++;
            }
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
            this.teachers = new Array<SelectItem>(data.length);
            let index = 0;
            for (let teacher of data) {
                this.teachers[index] = new SelectItem(teacher.id, teacher.toString());
                index++;
            }
        });
    }

    sendRequest() {
        console.log("submit");
    }
}
export class SelectItem {
    id: number;
    text: string;

    constructor(id: number, text: string) {
        this.id = id;
        this.text = text;
    }
}