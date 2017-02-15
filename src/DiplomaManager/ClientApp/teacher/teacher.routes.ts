import { Routes, RouterModule } from '@angular/router';

import { TeacherComponent } from './app/app.component';
import { HomeComponent } from './home/home.component';
import { DiplomaRequestsComponent } from './diplomarequests/diplomarequests.component'

export const routes: Routes = [
    { path: 'teacher', component: HomeComponent },
    { path: 'teacher/diplomarequests', component: DiplomaRequestsComponent }
];
