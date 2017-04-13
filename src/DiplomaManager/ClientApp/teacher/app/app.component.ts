import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { DataService } from '../../shared/services/data.service';
import { AuthService } from '../../shared/services/auth.service';

@Component({
    moduleId: module.id,
    selector: 'teacher-app',
    templateUrl: './app.component.html',
    providers: [DataService, AuthService]
})
export class TeacherComponent implements OnInit {
    constructor(private authService: AuthService, private toastrService: ToastrService) { }

    userDisplayName: string;

    ngOnInit() {
        this.authService.getUserName().subscribe(data => {
            this.userDisplayName = data;
        }, err => {
            this.toastrService.error('Ошибка получения имени пользователя');
            console.log(err);
        });
    }
}