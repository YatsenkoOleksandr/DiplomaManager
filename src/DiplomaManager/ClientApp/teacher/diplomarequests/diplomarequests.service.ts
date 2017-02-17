import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { RequestTeacher } from './requestteacher';
import { Student } from '../../shared/student.model';

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
}