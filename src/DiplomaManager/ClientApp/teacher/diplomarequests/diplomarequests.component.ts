import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators, FormArray, FormBuilder } from '@angular/forms';

import { TeacherService, ProjectEditTitle } from './diplomarequests.service';
import { RequestTeacher } from './requestTeacher.model';
import { ProjectTitle } from './projectTitle.model';

@Component({
    moduleId: module.id,
    selector: 'diploma-requests',
    templateUrl: './diplomarequests.component.html',
    providers: [TeacherService]
})
export class DiplomaRequestsComponent implements OnInit {

    requests: Array<RequestTeacher> = [];
    @ViewChild('diplomaRequestModal') diplomaRequestModal: ModalComponent;
    busy: Subscription;
    projectTitlesFGroup: FormGroup;
    selectedRequest: RequestTeacher;

    constructor(private dataService: TeacherService, private formBuilder: FormBuilder) {
        this.projectTitlesFGroup = formBuilder.group({
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
        let titleControls = this.projectTitlesFGroup.controls["projectTitles"] as FormArray;
        while (titleControls.length) {
            titleControls.removeAt(titleControls.length - 1);
        }
        for (let pTitle of request.projectTitles) {
            titleControls.push(new FormControl(pTitle.title, Validators.required));
        }
        this.diplomaRequestModal.open();
    }

    requestWindowClosed() {
        let titleControls = this.projectTitlesFGroup.controls["projectTitles"] as FormArray;
        let selectedProjectTitles = this.selectedRequest.projectTitles;
        this.saveEditedProjectTitles(titleControls, selectedProjectTitles);
    }

    private saveEditedProjectTitles(titleControls: FormArray, selectedProjectTitles: Array<ProjectTitle>) {
        let projectEditTitles = new Array<ProjectEditTitle>();
        for (let i = 0; i < titleControls.length; i++) {
            projectEditTitles.push(new ProjectEditTitle(selectedProjectTitles[i].id, titleControls.controls[i].value));
        }
        this.busy = this.dataService.editProjectTitles(projectEditTitles).subscribe(data => {
            if (data.message) {
                console.info(data.message);
                for (let i = 0; i < titleControls.length; i++) {
                    selectedProjectTitles[i].title = projectEditTitles[i].title;
                }
            } else {
                console.error(data.errorMessage);
            }
        });
    }
}

