import { Component, OnInit } from '@angular/core';
import { Response } from '@angular/http';
import { DataService } from './data.service';
import { Degree } from './degree';

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
            <ng-select id="degree"
                          class="ui-select"
                          [items]="degrees"
                          placeholder="Не выбран образовательный уровень">
              </ng-select>
        </div>
        <div class="form-group">
            <label for="developmentArea">Предметная область</label>
            <ng-select id="developmentArea"
                          class="ui-select"
                          [items]="das"
                          placeholder="Не выбрана предметная область">
              </ng-select>
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
    degrees: Array<SelectItem>;
    das: Array<SelectItem>;

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.dataService.getTeachers().subscribe((data) => {
            this.teachers = new Array<SelectItem>(data.length);
            let index = 0;
            for (let teacher of data) {
                this.teachers[index] = new SelectItem(teacher.id, teacher.toString());
                index++;
            }
        });

        this.dataService.getDegrees().subscribe((data) => {
            this.degrees = new Array<SelectItem>(data.length);
            let index = 0;
            for (let degree of data) {
                this.degrees[index] = new SelectItem(degree.id, degree.name);
                index++;
            }
        });

        this.dataService.getDevelopmentAreas().subscribe((data) => {
            this.das = new Array<SelectItem>(data.length);
            let index = 0;
            for (let da of data) {
                this.das[index] = new SelectItem(da.id, da.name);
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