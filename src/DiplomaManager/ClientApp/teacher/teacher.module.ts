import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { routes } from './teacher.routes';
import { TeacherComponent } from './app/app.component';
import { HomeComponent } from './home/home.component';
import { DiplomaRequestsComponent } from './diplomarequests/diplomarequests.component'

import { HttpModule } from '@angular/http';
import { SelectModule } from 'ng2-select';
import { BusyModule } from 'angular2-busy';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';
import { Ng2TableModule } from 'ng2-table/ng2-table';

@NgModule(({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        RouterModule.forRoot(routes),
        SelectModule,
        BusyModule,
        Ng2Bs3ModalModule,
        Ng2TableModule
    ],
    declarations: [
        TeacherComponent,
        HomeComponent,
        DiplomaRequestsComponent
    ],
    bootstrap: [TeacherComponent]
}) as any)
export class TeacherModule { }