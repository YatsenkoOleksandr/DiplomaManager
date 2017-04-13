import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Response } from '@angular/http';

import { DataService } from './data.service';

@Injectable()
export class AuthService {
    private accountApi: string;

    constructor(private dataService: DataService) {
        this.accountApi = 'Account';
    }

    getUserName(): Observable<string> {
        return this.dataService
            .get(this.accountApi + '/GetUserDisplayName')
            .map((resp: Response) => {
                return resp.text();
            });
    }
}