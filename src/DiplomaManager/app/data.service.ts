import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response } from '@angular/http';
import { Teacher } from './teacher';
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

    constructor(private http: Http) { }

    getData():Observable<Teacher[]> {

        return this.http.get('/Project/GetTeachers').map((resp: Response) => {
            let teacherList = resp.json();
            let teachers: Teacher[] = [];
            for (let index in teacherList) {
                let teacher = teacherList[index];
                teachers.push(
                    new Teacher(teacher.id, teacher.firstName, teacher.lastName, teacher.patronymic, null)
                    //{
                    //id: teacher.id,
                    //firstName: teacher.firstName,
                    //lastName: teacher.lastName,
                    //patronymic: teacher.patronymic}
                );
            }
            return teachers;
        });
    }
}