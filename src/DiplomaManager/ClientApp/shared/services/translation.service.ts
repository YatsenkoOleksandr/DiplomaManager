import { Injectable } from '@angular/core';
import { Http, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { TranslationResult } from '../translationResult.model';

@Injectable()
export class TranslationService {
    private apiBasePath = "Translation";

    constructor(private http: Http) { }

    translate(text: string, lang: string): Observable<TranslationResult> {
        let params = new URLSearchParams();
        params.set("text", text);
        params.set("lang", lang);

        return this.http.get(this.apiBasePath + '/Translate', {
            search: params
        }).map((resp: Response) => {
            return resp.json();
        });
    }
}