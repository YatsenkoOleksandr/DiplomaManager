import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators, FormArray, FormBuilder } from '@angular/forms';

import { TeacherService, ProjectEdit, ProjectEditTitle } from './diplomarequests.service';
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
    projectFGroup: FormGroup;
    selectedRequest: RequestTeacher;

    constructor(private dataService: TeacherService, private formBuilder: FormBuilder) {
        this.projectFGroup = formBuilder.group({
            practiceJournalPassed: formBuilder.control(''),
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
        let titleControls = this.projectFGroup.controls["projectTitles"] as FormArray;

       this.projectFGroup.controls["practiceJournalPassed"]
            .setValue(new Date(this.selectedRequest.practiceJournalPassed));
        while (titleControls.length) {
            titleControls.removeAt(titleControls.length - 1);
        }
        for (let pTitle of request.projectTitles) {
            titleControls.push(new FormControl(pTitle.title, Validators.required));
        }

        this.diplomaRequestModal.open();
    }

    requestWindowClosed() {
        this.saveEditedProject(this.projectFGroup, this.selectedRequest);
    }

    private saveEditedProject(projectFGroup: FormGroup, selectedProject:RequestTeacher) {
        let projectEdit = new ProjectEdit();
        projectEdit.id = selectedProject.id;
        projectEdit.practiceJournalPassed = projectFGroup.controls["practiceJournalPassed"].value;

        let titleControls = projectFGroup.controls["projectTitles"] as FormArray;
        let selectedProjectTitles = selectedProject.projectTitles;

        let projectEditTitles = new Array<ProjectEditTitle>();
        for (let i = 0; i < titleControls.length; i++) {
            let selectedProjTitle = selectedProjectTitles[i];
            projectEditTitles.push(new ProjectEditTitle(selectedProjTitle.id,
                titleControls.controls[i].value, selectedProjTitle.locale.id));
        }
        projectEdit.projectTitles = projectEditTitles;

        this.busy = this.dataService.editProject(projectEdit).subscribe(data => {
            if (data.message) {
                console.info(data.message);
                selectedProject.practiceJournalPassed = projectEdit.practiceJournalPassed;
                for (let i = 0; i < titleControls.length; i++) {
                    selectedProjectTitles[i].title = projectEditTitles[i].title;
                }
            } else {
                console.error(data.errorMessage);
            }
        });
    }
}

