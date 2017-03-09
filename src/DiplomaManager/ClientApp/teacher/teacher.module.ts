import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { routes } from './teacher.routes';
import { TeacherComponent } from './app/app.component';
import { HomeComponent } from './home/home.component';
import { DiplomaRequestsComponent } from './diplomarequests/diplomarequests.component'
import { AcceptedPipe } from '../shared/pipes/accepted.pipe';
import { CapitalizeFirstPipe } from '../shared/pipes/capitalizeFirst.pipe';

import { HttpModule } from '@angular/http';
import { SelectModule } from 'ng2-select';
import { BusyModule, BusyConfig } from 'angular2-busy';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Ng2TableModule } from 'ng2-table/ng2-table';
import { NgxMyDatePickerModule } from 'ngx-mydatepicker';

import { PaginationModule } from 'ng2-bootstrap';

@NgModule(({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        RouterModule.forRoot(routes),
        SelectModule,
        BusyModule.forRoot(
            new BusyConfig({
                message: 'Загрузка...',
                backdrop: true,
                delay: 200,
                minDuration: 600,
                wrapperClass: 'loading'
            })
        ),
        Ng2Bs3ModalModule,
        Ng2TableModule,
        NgxMyDatePickerModule,
        PaginationModule.forRoot()
    ],
    declarations: [
        TeacherComponent,
        HomeComponent,
        DiplomaRequestsComponent,

        AcceptedPipe,
        CapitalizeFirstPipe
    ],
    bootstrap: [TeacherComponent]
}) as any)
export class TeacherModule { }