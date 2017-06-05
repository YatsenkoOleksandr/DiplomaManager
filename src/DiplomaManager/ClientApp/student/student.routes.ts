import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app/app.component';
import { HomeComponent } from './home/home.component';
import { DiplomaRequestComponent } from './diplomarequest/diplomarequest.component';
import { PredefenseComponent } from './predefense/predefense.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'diplomarequest', component: DiplomaRequestComponent },
    { path: 'predefense', component: PredefenseComponent }
];