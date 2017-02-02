import { Component, OnInit } from '@angular/core';
import { Response } from '@angular/http';
import { DataService } from './data.service';
import { Request } from './request';

@Component({
    selector: 'my-app',
    template: `<form>
     <ul><li *ngFor="let response of responses">
                    <p>Имя пользователя: {{response?.name}}</p>
                    </li></ul>
    <div>
        <div class="form-group">
            <label for="teacher">Преподаватель</label>
            <select id="teacher"
                    class="form-control"></select>
        </div>
        <div class="form-group">
            <label for="degree">Образовательный уровень</label>
            <select id="degree"
                    class="form-control"></select>
        </div>
        <div class="form-group">
            <label for="developmentArea">Предметная область</label>
            <select id="developmentArea"
                    class="form-control"></select>
        </div>
        <div class="form-group">
            <label for="lastName">Фамилия</label>
            <input type="text" id="lastName" class="form-control" placeholder="Введите фамилию" />
        </div>
        <div class="form-group">
            <label for="firstName">Имя</label>
            <input type="text" id="firstName" class="form-control" placeholder="Введите имя" />
        </div>
        <div class="form-group">
            <label for="patronymic">Отчество</label>
            <input type="text" id="patronymic" class="form-control" placeholder="Введите отчество" />
        </div>
        <div class="form-group">
            <label for="title">Тема работы</label>
            <textarea type="text" id="title" class="form-control" placeholder="Введите тему работы"
                      rows="5"></textarea>
        </div>
        <div class="form-group">
            <input type="submit" value="Подать" class="btn btn-primary" />
        </div>
    </div>
</form>`,
    providers: [DataService]
})
export class AppComponent implements OnInit {

    responses: Response[] = [];

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.getData().subscribe((data: Response) => this.responses = data.json());
    }
}