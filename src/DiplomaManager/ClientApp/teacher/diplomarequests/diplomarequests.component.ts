import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators, FormArray, FormBuilder } from '@angular/forms';

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
    selectedRequest: RequestTeacher;

    constructor(private dataService: TeacherService, private formBuilder: FormBuilder) {
        this.projectTitlesFGroup = formBuilder.group({
            title: ['', [Validators.required]],
            projectTitles: formBuilder.array([])
        });
    }

    ngOnInit() {
        this.busy = this.dataService.getDiplomaRequests(2).subscribe((data) => {
            this.requests = data;
        });
    }

    editRequest(request: RequestTeacher) {
        this.selectedRequest = request;
        for (let pTitle of request.projectTitles) {
            (this.projectTitlesFGroup.controls["projectTitles"] as FormArray)
                .push(new FormControl(pTitle.title, Validators.required));
        }
        this.diplomaRequestModal.open();
    }
}
