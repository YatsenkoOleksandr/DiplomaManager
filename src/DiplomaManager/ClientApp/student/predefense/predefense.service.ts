import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';

import { DataService } from '../../shared/services/data.service';
import { Degree } from '../../shared/degree.model';
import { Group } from '../../shared/group.model';
import { User } from '../../shared/user.model';
import { PredefenseSchedule } from '../../shared/predefense-schedule.model';

@Injectable()
export class PredefenseService {
    private apiBasePath = 'Predefense';

    constructor(private dataService: DataService) { }

    getDegrees(): Observable<Degree[]> {
        return this.dataService
            .get(this.apiBasePath + '/GetDegrees')
            .map((resp: Response) => {
                return resp.json();
            });
    }

    getGraduationYears(degreeId: number): Observable<number[]> {
        return this.dataService
            .get(`${this.apiBasePath}/GetGraduationYears`, { degreeId: degreeId })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    getPredefenseSchedule(degreeId: number, graduationYear: number): Observable<PredefenseSchedule[]> {
        return this.dataService
            .get(`${this.apiBasePath}/GetPredefenseSchedule`, { degreeId: degreeId, graduationYear: graduationYear })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    //getGroups(degreeId: number, graduationYear: number): Observable<Group[]> {
    //    return this.dataService
    //        .get(`${this.apiBasePath}/GetGroups`, { degreeId: degreeId, graduationYear: graduationYear })
    //        .map((resp: Response) => {
    //            return resp.json();
    //        });
    //}

    //getFreeStudents(groupId: number): Observable<User[]> {
    //    return this.dataService
    //        .get(`${this.apiBasePath}/GetFreeStudents`, { groupId: groupId })
    //        .map((resp: Response) => {
    //            return resp.json();
    //        });
    //}

    submitToPredefense(studentId: number, predefenseId: number) {
        return this.dataService
            .post(`${this.apiBasePath}/SubmitToPredefense`, { studentId: studentId, predefenseId: predefenseId })
            .map((resp: Response) => {
                return resp.json();
            });
    }
}