import { PredefenseDate } from './predefense-date.model';
import { TeacherInfo } from './teacher.model';

export class PredefenseSchedule {
    predefenseDate: PredefenseDate;
    teachers: Array<TeacherInfo>;
}