import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { RequestTeacher } from './requestTeacher.model';
import { Student } from '../../shared/student.model';
import { ProjectTitle } from './projectTitle.model';

@Injectable()
export class TeacherService {

    constructor(private http: Http) { }

    getDiplomaRequests(teacherId: number): Observable<RequestTeacher[]> {
        return this.http.get('/Teacher/Request/GetDiplomaRequests?teacherId=' + teacherId).map((resp: Response) => {
            let requestsList = resp.json();
            for (let request of requestsList) {
                request.student = new Student(request.student.id,
                    request.student.firstName,
                    request.student.lastName,
                    request.student.patronymic,
                    request.student.email,
                    request.groupId);
            }
            return requestsList;
        });
    }

    editProject(projectEdit: ProjectEdit) {
        const body = JSON.stringify(projectEdit);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post('/Teacher/Request/EditDiplomaProject', body, { headers: headers })
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

    constructor(id: number, title: string) {
        this.id = id;
        this.title = title;
    }
}