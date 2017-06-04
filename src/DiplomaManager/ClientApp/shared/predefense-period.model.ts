import { Degree } from './degree.model';
import { TimeSpan } from './timespan.model';

export class PredefensePeriod {
    id: number;
    degreeId: number;
    degree: Degree;
    graduationYear: number;
    startDate: Date;
    finishDate: Date;
    predefenseStudentTime: TimeSpan;
}