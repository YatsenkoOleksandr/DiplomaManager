import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { routes } from './student.routes';
import { AppComponent } from './app/app.component';
import { HomeComponent } from './home/home.component';
import { DiplomaRequestComponent } from './diplomarequest/diplomarequest.component';

import { HttpModule } from '@angular/http';
import { SelectModule } from 'ng2-select';
import { BusyModule, BusyConfig } from 'angular2-busy';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';

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
        Ng2Bs3ModalModule],
    declarations: [
        AppComponent,
        HomeComponent,
        DiplomaRequestComponent
    ],
    bootstrap: [AppComponent]
}) as any)
export class StudentModule { }