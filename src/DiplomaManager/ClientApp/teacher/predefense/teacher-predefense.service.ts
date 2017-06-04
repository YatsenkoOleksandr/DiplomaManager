import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';

import { DataService } from '../../shared/services/data.service';
import { Predefense } from '../../shared/predefense.model';
import { PredefenseSchedule } from '../../shared/predefense-schedule.model';
import { TeacherPredefensePeriod } from './teacher-predefense-period.model';

@Injectable()
export class TeacherPredefenseService {
    private apiBasePath = 'Teacher/Predefense';

    constructor(private dataService: DataService) { }

    getTeacherPeriods(teacherId: number): Observable<TeacherPredefensePeriod> {
        return this.dataService
            .get(`${this.apiBasePath}/GetTeacherPeriods`, { teacherId: teacherId })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    getTeacherPredefenseSchedule(teacherId: number, predefensePeriodId: number): Observable<PredefenseSchedule> {
        return this.dataService
            .get(`${this.apiBasePath}/GetTeacherPredefenseSchedule`, { teacherId: teacherId, predefensePeriodId: predefensePeriodId })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    getPredefenseSchedule(predefensePeriodId: number): Observable<PredefenseSchedule> {
        return this.dataService
            .get(`${this.apiBasePath}/GetPredefenseSchedule`, { predefensePeriodId: predefensePeriodId })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    savePredefenseResults(teacherId: number, predefense: Predefense) {
        return this.dataService
            .post(`${this.apiBasePath}/SavePredefenseResults`, { teacherId: teacherId, predefense: predefense })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    submitToPredefenseDate(teacherId: number, predefenseDateId: number) {
        return this.dataService
            .post(`${this.apiBasePath}/SubmitToPredefenseDate`, { teacherId: teacherId, predefenseDateId: predefenseDateId })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    denySubmitToPredefenseDate(teacherId: number, predefenseDateId: number) {
        return this.dataService
            .post(`${this.apiBasePath}/DenySubmitToPredefenseDate`, { teacherId: teacherId, predefenseDateId: predefenseDateId })
            .map((resp: Response) => {
                return resp.json();
            });
    }
}