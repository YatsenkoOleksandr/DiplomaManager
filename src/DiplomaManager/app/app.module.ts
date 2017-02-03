﻿import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { HttpModule } from '@angular/http';
import { SelectModule } from 'ng2-select';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, SelectModule],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }