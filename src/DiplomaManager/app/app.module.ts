import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { HttpModule } from '@angular/http';
import { SelectModule } from 'ng2-select';
import { BusyModule } from 'angular2-busy';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, SelectModule, BusyModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }