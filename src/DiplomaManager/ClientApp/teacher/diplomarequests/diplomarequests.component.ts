import { Component } from '@angular/core';

@Component({
    moduleId: module.id,
    selector: 'diplomarequests',
    templateUrl: './diplomarequests.component.html'
})
export class DiplomaRequestsComponent {
    public rows: Array<any> = [];
    /*public columns: Array<any> = [
        { title: 'Name', name: 'name', filtering: { filterString: '', placeholder: 'Filter by name' } },
        {
            title: 'Position',
            name: 'position',
            sort: false,
            filtering: { filterString: '', placeholder: 'Filter by position' }
        },
        { title: 'Office', className: ['office-header', 'text-success'], name: 'office', sort: 'asc' },
        { title: 'Extn.', name: 'ext', sort: '', filtering: { filterString: '', placeholder: 'Filter by extn.' } },
        { title: 'Start date', className: 'text-warning', name: 'startDate' },
        { title: 'Salary ($)', name: 'salary' }
    ];*/

    public columns: Array<any> = [
        { title: '№ заявки', name: 'id' },
        { title: 'ФИО студента', name: 'FIOstud', filtering: { filterString: '', placeholder: 'Фильтрация по ФИО' } },
        { title: 'Тема', name: 'title', sort: '', filtering: { filterString: '', placeholder: 'Фильтрация по теме' } },
        { title: 'Дата подачи', className: 'text-warning', name: 'creationDate' },
        { title: 'Принята?', name: 'accepted' }
    ];

    public config: any = {
        paging: true,
        sorting: { columns: this.columns },
        filtering: { filterString: '' },
        className: ['table-striped', 'table-bordered']
    };

}
