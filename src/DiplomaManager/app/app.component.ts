import { Component, OnInit } from '@angular/core';
import { Response } from '@angular/http';
import { DataService } from './data.service';
import { Degree } from './degree';

@Component({
    selector: 'my-app',
    templateUrl: 'app/app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {
    teachers: Array<SelectItem>;
    degrees: Array<SelectItem>;
    das: Array<SelectItem>;

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.getTeachers().subscribe((data) => {
            this.teachers = new Array<SelectItem>(data.length);
            let index = 0;
            for (let teacher of data) {
                this.teachers[index] = new SelectItem(teacher.id, teacher.toString());
                index++;
            }
        });

        this.dataService.getDegrees().subscribe((data) => {
            this.degrees = new Array<SelectItem>(data.length);
            let index = 0;
            for (let degree of data) {
                this.degrees[index] = new SelectItem(degree.id, degree.name);
                index++;
            }
        });

        this.dataService.getDevelopmentAreas().subscribe((data) => {
            this.das = new Array<SelectItem>(data.length);
            let index = 0;
            for (let da of data) {
                this.das[index] = new SelectItem(da.id, da.name);
                index++;
            }
        });
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