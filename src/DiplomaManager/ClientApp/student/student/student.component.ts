import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { SelectItem } from '../../shared/selectItem';
import { CustomValidators } from 'ng2-validation';

import { StudentService } from './student.service';
import { DataService } from '../../shared/services/data.service';
import { Student } from '../../shared/student.model';

@Component({
    moduleId: module.id,
    selector: 'student',
    templateUrl: './student.component.html',
    providers: [DataService, StudentService]
})
export class StudentComponent implements OnInit {
    groupsList: Array<SelectItem>;
    studentsList: Array<SelectItem>;

    busy: Subscription;
    studentFGroup: FormGroup;

    constructor(private dataService: StudentService) { }

    ngOnInit() {
        this.studentFGroup = new FormGroup({
            groups: new FormControl('', Validators.required),
            users: new FormControl('', Validators.required)
        });

        //this.busy = this.dataService.getGraduationYears(1).subscribe((data) => {
        //    this.yearsList = data.map(y => new SelectItem(y, y.toString()));
        //});
    }

    yearSelected(degrees, event) {
        let year = event.id;

        this.busy = this.dataService.getGroups(degrees, year).subscribe((data) => {
            this.setItemsToSelectList(data, "groupsList");
        });
    }

    groupSelected(event) {
        let groupid = event.id;

        this.busy = this.dataService.getFreeStudents(groupid).subscribe((data) => {
            this.setItemsToSelectList(data, "studentsList", "fullName");
        });
    }

    private setItemsToSelectList<T>(data: Array<T>, selectListName: string,
        labelPropertyName: string = "name", idPropertyName: string = "id") {
        this[selectListName] = new Array<SelectItem>(data.length);
        for (let index = 0; index < data.length; index++) {
            let item = data[index];
            let name = item[labelPropertyName];
            if (!name)
                name = item.toString();
            this[selectListName][index] = new SelectItem(item[idPropertyName], name);
        }
    }
}