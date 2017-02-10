import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { HttpModule } from '@angular/http';
import { SelectModule } from 'ng2-select';
import { BusyModule } from 'angular2-busy';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';

@NgModule({
    imports: [BrowserModule, FormsModule, ReactiveFormsModule, HttpModule, SelectModule, BusyModule, Ng2Bs3ModalModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }