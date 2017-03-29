﻿import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map';

import { Teacher } from '../../shared/teacher.model';
import { Degree } from './degree.model';
import { DevelopmentArea } from './developmentArea.model';
import { Request } from './request.model';
import { Capacity } from './capacity.model';
import { Group } from '../../shared/group.model';
import { PeopleName, NameKind } from '../../shared/peopleName.model';

@Injectable()
export class DiplomaRequestService {

    constructor(private http: Http) { }

    getTeachers(daId: number): Observable<Teacher[]> {
        return this.http.get('/Request/GetTeachers/?daId=' + daId).map((resp: Response) => {
            let teacherList = resp.json();
            let teachers: Teacher[] = [];
            for (let index in teacherList) {
                let teacher = teacherList[index];
                teachers.push(
                    new Teacher(teacher.id, teacher.firstName, teacher.lastName, teacher.patronymic, teacher.positionName, teacher.email)
                );
            }
            return teachers;
        });
    }

    getDegrees(): Observable<Degree[]> {
        return this.http.get('/Request/GetDegrees').map((resp: Response) => {
            return resp.json();
        });
    }

    getDevelopmentAreas(): Observable<DevelopmentArea[]> {
        return this.http.get('/Request/GetDevelopmentAreas').map((resp: Response) => {
            return resp.json();
        });
    }

    getCapacity(degreeId: number, teacherId: number): Observable<Capacity> {
        return this.http.get(`/Request/GetCapacity/?degreeId=${degreeId}&teacherId=${teacherId}`).map((resp: Response) => {
            return resp.json();
        });
    }

    getGroups(degreeId: number): Observable<Group[]> {
        return this.http.get(`/Request/GetGroups/?degreeId=${degreeId}`).map((resp: Response) => {
            return resp.json();
        });
    }

    getStudentNames(query: string, nameKind: NameKind, maxItems: number = 10):Observable<PeopleName[]> {
        let params = new URLSearchParams();
        params.set("query", query);
        params.set("nameKind", nameKind.toString());
        params.set("maxItems", maxItems.toString());

        return this.http.get('/Request/GetStudentNames', {
            search: params
        }).map((resp: Response) => {
            return resp.json();
        });
    }

    sendRequest(request: Request) {
        const body = JSON.stringify(request);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post('/Request/SendRequest', body, { headers: headers })
            .map((resp: Response) => resp.json());
    }
}