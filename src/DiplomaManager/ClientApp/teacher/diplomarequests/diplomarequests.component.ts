import { Component, OnInit, ViewChild, Input, Output, ChangeDetectionStrategy, EventEmitter } from '@angular/core';

import { TeacherService } from './diplomarequests.service'
import { RequestTeacher } from './requestteacher'

@Component({
    moduleId: module.id,
    selector: 'diploma-requests',
    templateUrl: './diplomarequests.component.html',
    providers: [TeacherService]
})
export class DiplomaRequestsComponent implements OnInit {

    public requests: Array<RequestTeacher> = [];

    constructor(private dataService: TeacherService) { }

    ngOnInit() {
        this.dataService.getDiplomaRequests(2).subscribe((data) => {
            this.requests = data;
        });
    }
}
