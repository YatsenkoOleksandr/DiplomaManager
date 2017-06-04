﻿import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map';

import { TeacherInfo } from '../../shared/teacher.model';
import { Degree } from './degree.model';
import { DevelopmentArea } from './developmentArea.model';
import { Request } from './request.model';
import { Capacity } from './capacity.model';
import { Group } from '../../shared/group.model';
import { User } from '../../shared/user.model';

@Injectable()
export class DiplomaRequestService {
    private apiBasePath = 'Project';

    constructor(private http: Http) { }

    getTeachers(daId: number): Observable<TeacherInfo[]> {
        return this.http.get(`${this.apiBasePath}/GetTeachers/?daId=${daId}`).map((resp: Response) => {
            return resp.json();
        });
    }

    getDegrees(): Observable<Degree[]> {
        return this.http.get(`${this.apiBasePath}/GetDegrees`).map((resp: Response) => {
            return resp.json();
        });
    }

    getDevelopmentAreas(): Observable<DevelopmentArea[]> {
        return this.http.get(`${this.apiBasePath}/GetDevelopmentAreas`).map((resp: Response) => {
            return resp.json();
        });
    }

    getCapacity(degreeId: number, teacherId: number): Observable<Capacity> {
        return this.http.get(`${this.apiBasePath}/GetCapacity/?degreeId=${degreeId}&teacherId=${teacherId}`).map((resp: Response) => {
            return resp.json();
        });
    }

    getGraduationYears(degreeId: number): Observable<number[]> {
        return this.http.get(`${this.apiBasePath}/GetGraduationYears/?degreeId=${degreeId}`).map((resp: Response) => {
            return resp.json();
        });
    }

    getGroups(degreeId: number, graduationYear: number): Observable<Group[]> {
        return this.http.get(`${this.apiBasePath}/GetGroups/?degreeId=${degreeId}&graduationYear=${graduationYear}`).map((resp: Response) => {
            return resp.json();
        });
    }

    getStudents(groupId: number): Observable<User[]>  {
        return this.http.get(`${this.apiBasePath}/GetStudents/?groupId=${groupId}`).map((resp: Response) => {
            return resp.json();
        });
    }

    sendRequest(request: Request) {
        const body = JSON.stringify(request);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post(`${this.apiBasePath}/SendRequest`, body, { headers: headers })
            .map((resp: Response) => resp.json());
    }
}