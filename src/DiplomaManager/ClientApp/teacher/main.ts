import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { TeacherModule } from './teacher.module';
const platform = platformBrowserDynamic();
platform.bootstrapModule(TeacherModule);