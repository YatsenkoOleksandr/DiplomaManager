import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators, FormArray, FormBuilder } from '@angular/forms';
import { IMyOptions, IMyDate, IMyDateModel } from 'ngx-mydatepicker';
import { ToastrService } from 'ngx-toastr';

import { TeacherService, ProjectEdit, ProjectEditTitle, RespondProjectRequest } from './diplomarequests.service';
import { RequestTeacher } from './requestTeacherResponse.model';
import { ProjectTitle } from './projectTitle.model';
import { GetProjectsRequest } from './getProjectsRequest.model';
import { TranslationService } from '../../shared/services/translation.service'

@Component({
    moduleId: module.id,
    selector: 'diploma-requests',
    templateUrl: './diplomarequests.component.html',
    providers: [TeacherService, TranslationService]
})
export class DiplomaRequestsComponent implements OnInit {

    requests: Array<RequestTeacher> = [];

    totalItems: number;
    currentPage = 1;
    itemsPerPage = 5;

    query: string;
    queryFieldControl = new FormControl('');
    isSearchActive: boolean;

    @ViewChild('diplomaRequestModal') diplomaRequestModal: ModalComponent;
    @ViewChild('acceptModal') acceptModal: ModalComponent;
    @ViewChild('declineModal') declineModal: ModalComponent;
    busy: Subscription;
    modalBusy: Subscription;
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

    constructor(private dataService: TeacherService, private formBuilder: FormBuilder, private toastrService: ToastrService,
        private translationService: TranslationService) {
        this.projectFGroup = formBuilder.group({
            practiceJournalPassed: [''],
            projectTitles: formBuilder.array([])
        });

        this.queryFieldControl.valueChanges
            .debounceTime(100)
            .subscribe(newValue => {
                if (newValue && newValue.length > 2) {
                    this.query = String(newValue);
                    this.getProjects();
                    this.isSearchActive = true;
                } else {
                    if (this.isSearchActive) {
                        this.query = null;
                        this.getProjects();
                        this.isSearchActive = false;
                    }
                }
            });
    }

    ngOnInit() {
        this.getProjects();
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

    getProjects() {
        let request = new GetProjectsRequest(2, this.query, this.itemsPerPage, this.currentPage);
        this.busy = this.dataService.getDiplomaRequests(request).subscribe((response) => {
            this.totalItems = response.total;
            this.requests = response.data;
        });
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
            this.toastrService.success('Заявка успешно принята');
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
            this.toastrService.success('Заявка успешно отклонена');
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
                this.toastrService.success('Заявка успешно отредактирована');
            } else {
                this.toastrService.error('Ошибка редактирования заявки');
                console.error(project.errorMessage);
            }
        });
    }

    pageChanged(event: any): void {
        this.currentPage = event.page;
        this.getProjects();
    }

    translateProjectTitle(projectTitle: ProjectTitle, title: FormControl, invariantTitle: FormControl) {
        let text = invariantTitle.value;
        let lang = projectTitle.locale.name;
        this.modalBusy = this.translationService.translate(text, lang).subscribe(res => {
            if (res) {
                title.setValue(res.translationText)
                this.toastrService.success(`Текст успешно переведен (${res.from}-${res.to})`);
            }
            else {
                this.toastrService.error('Не удалось перевести');
            }
        }, err => {
            this.toastrService.error('Ошибка перевода текста');
            console.log(err);
        });
    }
}

