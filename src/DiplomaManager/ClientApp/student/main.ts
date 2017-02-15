import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { StudentModule } from './student.module';
const platform = platformBrowserDynamic();
platform.bootstrapModule(StudentModule);