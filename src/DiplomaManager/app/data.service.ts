import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response } from '@angular/http';
import { Teacher } from './teacher';
import { Degree } from './degree';
import { DevelopmentArea } from './developmentArea';
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

    constructor(private http: Http) { }

    getTeachers(): Observable<Teacher[]> {
        return this.http.get('/Project/GetTeachers').map((resp: Response) => {
            let teacherList = resp.json();
            let teachers: Teacher[] = [];
            for (let index in teacherList) {
                let teacher = teacherList[index];
                teachers.push(
                    new Teacher(teacher.id, teacher.firstName, teacher.lastName, teacher.patronymic, teacher.positionName)
                );
            }
            return teachers;
        });
    }

    getDegrees(): Observable<Degree[]> {
        return this.http.get('/Project/GetDegrees').map((resp: Response) => {
            let degreesList = resp.json();
            return degreesList;
        });
    }

    getDevelopmentAreas(): Observable<DevelopmentArea[]> {
        return this.http.get('/Project/GetDevelopmentAreas').map((resp: Response) => {
            let developmentAreaList = resp.json();
            return developmentAreaList;
        });
    }
}