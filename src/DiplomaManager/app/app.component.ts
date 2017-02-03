import { Component, OnInit } from '@angular/core';
import { Response } from '@angular/http';
import { DataService } from './data.service';
import { Teacher } from './teacher';

@Component({
    selector: 'my-app',
    template: `<form>
    <div class="col-md-4">
        <div class="form-group">
            <label for="teacher">Преподаватель</label>
            <ng-select id="teacher"
                          class="ui-select"
                          [items]="teachers"
                          placeholder="Не выбран преподаватель">
              </ng-select>
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
    teachers: Array<SelectItem>;
    
    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.getData().subscribe((data) => {
            this.teachers = new Array<SelectItem>(data.length);
            let index = 0;
            for (let teacher of data) {
                this.teachers[index] = new SelectItem(teacher.id, teacher.toString());
                index++;
            }
        });
    }
}
export class SelectItem {
    id: number;
    text: string;

    constructor(id: number, text: string) {
        this.id = id;
        this.text = text;
    }
}