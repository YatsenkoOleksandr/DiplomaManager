import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { TeacherService } from './diplomarequests.service';
import { RequestTeacher } from './requestTeacher.model';

@Component({
    moduleId: module.id,
    selector: 'diploma-requests',
    templateUrl: './diplomarequests.component.html',
    providers: [TeacherService]
})
export class DiplomaRequestsComponent implements OnInit {

    public requests: Array<RequestTeacher> = [];
    @ViewChild('diplomaRequestModal') diplomaRequestModal: ModalComponent;
    busy: Subscription;
    projectTitlesFGroup: FormGroup;

    constructor(private dataService: TeacherService) { }

    ngOnInit() {
        this.busy = this.dataService.getDiplomaRequests(2).subscribe((data) => {
            this.requests = data;
        });

        this.projectTitlesFGroup = new FormGroup({
            title: new FormControl('', Validators.required)
        });
    }

    editRequest(request: RequestTeacher) {
        this.diplomaRequestModal.open();
    }
}
