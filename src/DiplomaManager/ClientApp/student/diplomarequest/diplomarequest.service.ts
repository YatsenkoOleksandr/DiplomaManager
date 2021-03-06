﻿import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map';

import { Teacher } from '../../shared/teacher.model';
import { Degree } from './degree.model';
import { DevelopmentArea } from './developmentArea.model';
import { Request } from './request.model';
import { Capacity } from './capacity.model';

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
            let degreesList = resp.json();
            return degreesList;
        });
    }

    getDevelopmentAreas(): Observable<DevelopmentArea[]> {
        return this.http.get('/Request/GetDevelopmentAreas').map((resp: Response) => {
            let developmentAreaList = resp.json();
            return developmentAreaList;
        });
    }

    getCapacity(degreeId: number, teacherId: number): Observable<Capacity> {
        return this.http.get(`/Request/GetCapacity/?degreeId=${degreeId}&teacherId=${teacherId}`).map((resp: Response) => {
            let capacity = resp.json();
            return capacity;
        });
    }

    sendRequest(request: Request) {
        const body = JSON.stringify(request);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post('/Request/SendRequest', body, { headers: headers })
            .map((resp: Response) => resp.json());
    }
}