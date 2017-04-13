import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams } from '@angular/http';

@Injectable()
export class DataService {
    constructor(private http: Http) {
    }

    public get(url: string, params?: any) {
        return this.http.get(url, {
            search: params
        });
    }

    public post(url: string, data?: any) {
        const body = JSON.stringify(data);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post(url, body, { headers: headers });
    }

    private buildUrlSearchParams(params: any): URLSearchParams {
        let searchParams = new URLSearchParams();
        for (let key in params) {
            if (params.hasOwnProperty(key))
                searchParams.append(key, params[key]);
        }
        return searchParams;
    }
}