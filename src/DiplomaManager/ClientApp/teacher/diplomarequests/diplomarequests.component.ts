import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators, FormArray, FormBuilder } from '@angular/forms';
import { IMyOptions, IMyDate, IMyDateModel } from 'ngx-mydatepicker';

import { TeacherService, ProjectEdit, ProjectEditTitle, RespondProjectRequest } from './diplomarequests.service';
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
    @ViewChild('acceptModal') acceptModal: ModalComponent;
    @ViewChild('declineModal') declineModal: ModalComponent;
    busy: Subscription;
    projectFGroup: FormGroup;
    selectedRequest: RequestTeacher;
    commentModal: string;

    private myOptions: IMyOptions = {
        dayLabels: { su: "Вс", mo: "Пн", tu: "Вт", we: "Ср", th: "Чт", fr: "Пт", sa: "Сб" },
        monthLabels: { 1: "Янв", 2: "Фев", 3: "Март", 4: "Апр", 5: "Май", 6: "Июнь", 7: "Июль", 8: "Авг", 9: "Сент", 10: "Окт", 11: "Ноя", 12: "Дек" },
        dateFormat: "dd.mm.yyyy",
        todayBtnTxt: "Сегодня",
        firstDayOfWeek: "mo",
        sunHighlight: true
    };

    constructor(private dataService: TeacherService, private formBuilder: FormBuilder) {
        this.projectFGroup = formBuilder.group({
            practiceJournalPassed: [''],
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

        let practiceJournalPassedDate = request.practiceJournalPassed;
        if (practiceJournalPassedDate != null) {
            practiceJournalPassedDate = new Date(practiceJournalPassedDate);
            this.projectFGroup.controls["practiceJournalPassed"].setValue({
                date: {
                    year: practiceJournalPassedDate.getFullYear(),
                    month: practiceJournalPassedDate.getMonth() + 1,
                    day: practiceJournalPassedDate.getDate()
                }
            });
        }

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

    acceptDiplomaRequest(request: RequestTeacher) {
        this.selectedRequest = request;
        this.acceptModal.open();
    }

    acceptModalClosed() {
        let respondDiplomaRequest = new RespondProjectRequest(this.selectedRequest.id, true, this.commentModal);
        this.busy = this.dataService.respondDiplomaRequest(respondDiplomaRequest).subscribe(data => {
            this.selectedRequest.accepted = true;
        });
    }

    declineDiplomaRequest(request: RequestTeacher) {
        this.selectedRequest = request;
        this.declineModal.open();
    }

    declineModalClosed() {
        let respondDiplomaRequest = new RespondProjectRequest(this.selectedRequest.id, false, this.commentModal);
        this.busy = this.dataService.respondDiplomaRequest(respondDiplomaRequest).subscribe(data => {
            this.selectedRequest.accepted = false;
        });
    }
    
    private saveEditedProject(projectFGroup: FormGroup, selectedProject:RequestTeacher) {
        let projectEdit = new ProjectEdit();
        projectEdit.id = selectedProject.id;
        let pDate = projectFGroup.controls["practiceJournalPassed"].value as IMyDateModel;

        if (pDate && pDate.date.year > 1)
            projectEdit.practiceJournalPassed = pDate.jsdate;
        else
            projectEdit.practiceJournalPassed = null;

        let titleControls = projectFGroup.controls["projectTitles"] as FormArray;
        let selectedProjectTitles = selectedProject.projectTitles;

        let projectEditTitles = new Array<ProjectEditTitle>();
        for (let i = 0; i < titleControls.length; i++) {
            let selectedProjTitle = selectedProjectTitles[i];
            projectEditTitles.push(new ProjectEditTitle(selectedProjTitle.id,
                titleControls.controls[i].value, selectedProjTitle.locale.id));
        }
        projectEdit.projectTitles = projectEditTitles;

        this.busy = this.dataService.editProject(projectEdit).subscribe(project => {
            if (!project.errorMessage) {
                selectedProject.practiceJournalPassed = project.practiceJournalPassed;
                for (let i = 0; i < titleControls.length; i++) {
                    selectedProjectTitles[i].id = project.projectTitles[i].id;
                    selectedProjectTitles[i].title = project.projectTitles[i].title;
                }
            } else {
                console.error(project.errorMessage);
            }
        });
    }
}

