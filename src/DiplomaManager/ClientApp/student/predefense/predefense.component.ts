import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { SelectComponent } from 'ng2-select';
import { CustomValidators } from 'ng2-validation';
import { ToastrService } from 'ngx-toastr';

import { SelectItem } from '../../shared/selectItem';
import { PredefenseService } from './predefense.service';
import { DataService } from "../../shared/services/data.service";
import { PredefenseSchedule } from "../../shared/predefense-schedule.model";

@Component({
    moduleId: module.id,
    selector: 'predefense',
    templateUrl: './predefense.component.html',
    providers: [DataService, PredefenseService]
})
export class PredefenseComponent implements OnInit {
    degreesList: Array<SelectItem>;
    yearsList: Array<SelectItem>;

    predefenseFGroup: FormGroup;
    busy: Subscription;

    predefenseSchedule: Array<PredefenseSchedule>;

    constructor(private dataService: PredefenseService, private toastrService: ToastrService) { }

    ngOnInit() {
        this.predefenseFGroup = new FormGroup({
            degrees: new FormControl('', Validators.required),
            graduationYears: new FormControl('', Validators.required)
        });

        this.busy = this.dataService.getDegrees().subscribe(data => {
            this.setItemsToSelectList(data, "degreesList");
        });
    }

    degreeSelected(event) {
        let degreeid = event.id;

        this.busy = this.dataService.getGraduationYears(degreeid).subscribe(data => {
            this.yearsList = data.map(y => new SelectItem(y, y.toString()));
        });
    }

    yearSelected(degrees, event) {
        let year = event.id;

        this.busy = this.dataService.getPredefenseSchedule(degrees[0].id, year).subscribe(data => {
            this.predefenseSchedule = data;
        });
    }

    onSubmit({ value, valid }: { value: any, valid: boolean }) {
        if (value && valid) {
            this.busy = this.dataService.submitToPredefense(1, 1).subscribe(data => {
                if (data.message) {
                    this.toastrService.success(data.message + ' =)');
                } else {
                    this.toastrService.error('Ошибка записи =(');
                    console.error(data.errorMessage);
                }
            });
        }
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