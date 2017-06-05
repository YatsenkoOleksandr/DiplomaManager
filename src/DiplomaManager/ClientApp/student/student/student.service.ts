import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';

import { DataService } from '../../shared/services/data.service';
import { Group } from '../../shared/group.model';
import { User } from '../../shared/user.model';

@Injectable()
export class StudentService {
    private apiBasePath = 'Predefense';

    constructor(private dataService: DataService) { }

    getGraduationYears(degreeId: number): Observable<number[]> {
        return this.dataService
            .get(`${this.apiBasePath}/GetGraduationYears`, { degreeId: degreeId })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    getGroups(degreeId: number, graduationYear: number): Observable<Group[]> {
        return this.dataService
            .get(`${this.apiBasePath}/GetGroups`, { degreeId: degreeId, graduationYear: graduationYear })
            .map((resp: Response) => {
                return resp.json();
            });
    }

    getFreeStudents(groupId: number): Observable<User[]> {
        return this.dataService
            .get(`${this.apiBasePath}/GetFreeStudents`, { groupId: groupId })
            .map((resp: Response) => {
                return resp.json();
            });
    }
}