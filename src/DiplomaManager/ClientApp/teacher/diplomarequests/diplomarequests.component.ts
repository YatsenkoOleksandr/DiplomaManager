import { Component, OnInit, ViewChild, Input, Output, ChangeDetectionStrategy, EventEmitter } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';

import { TeacherService } from './diplomarequests.service';
import { RequestTeacher } from './requestteacher';

@Component({
    moduleId: module.id,
    selector: 'diploma-requests',
    templateUrl: './diplomarequests.component.html',
    providers: [TeacherService]
})
export class DiplomaRequestsComponent implements OnInit {

    public requests: Array<RequestTeacher> = [];
    @ViewChild('diplomaRequestModal') diplomaRequestModal: ModalComponent;

    constructor(private dataService: TeacherService) { }

    ngOnInit() {
        this.dataService.getDiplomaRequests(2).subscribe((data) => {
            this.requests = data;
        });
    }

    editRequest(request: RequestTeacher) {
        this.diplomaRequestModal.open();
    }
}
