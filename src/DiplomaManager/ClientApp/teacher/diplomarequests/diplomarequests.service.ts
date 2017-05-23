import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { RequestTeacher } from './requestTeacherResponse.model';
import { StudentInfo } from '../../shared/student.model';
import { GetProjectsRequest } from './getProjectsRequest.model';
import { IPagedResponse } from '../../shared/pagedResponse.model';

@Injectable()
export class TeacherService {
    private apiBasePath = "Teacher/Project";

    constructor(private http: Http) { }

    getDiplomaRequests(getProjectsRequest: GetProjectsRequest): Observable<IPagedResponse<RequestTeacher>> {
        const body = JSON.stringify(getProjectsRequest);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post(`${this.apiBasePath}/GetDiplomaRequests`, body, { headers: headers })
            .map((resp: Response) => {
                let requestsList = resp.json();
                for (let request of requestsList.data) {
                    request.student = new StudentInfo(request.student.id,
                        request.student.shortName,
                        request.student.fullName,
                        request.student.email,
                        request.groupId);
                }
            return requestsList;
        });
    }

    editProject(projectEdit: ProjectEdit) {
        const body = JSON.stringify(projectEdit);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post(`${this.apiBasePath}/EditDiplomaProject`, body, { headers: headers })
            .map((resp: Response) => resp.json());
    }

    respondDiplomaRequest(respondProjectRequest: RespondProjectRequest) {
        const body = JSON.stringify(respondProjectRequest);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post(`${this.apiBasePath}/RespondDiplomaRequest`, body, { headers: headers })
            .map((resp: Response) => resp.json());
    }
}

export class ProjectEdit {
    id: number;
    practiceJournalPassed: Date;
    projectTitles: Array<ProjectEditTitle>;
}

export class ProjectEditTitle {
    id: number;
    title: string;
    localeId: number;

    constructor(id: number, title: string, localeId: number) {
        this.id = id;
        this.title = title;
        this.localeId = localeId;
    }
}

export class RespondProjectRequest {
    projectId: number;
    accepted: boolean;
    comment: string;

    constructor(projectId: number, accepted: boolean, comment: string) {
        this.projectId = projectId;
        this.accepted = accepted;
        this.comment = comment;
    }
}