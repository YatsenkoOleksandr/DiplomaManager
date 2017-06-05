import { StudentInfo } from './student.model';

export class Predefense {
    id: number;
    predefenseDateId: number;
    studentId: number;
    student: StudentInfo;
    time: Date;

    passed: boolean;

    softwareReadiness: number;
    writingReadiness: number;
    presentationReadiness: number;

    reportExist: boolean;
    safetySigned: boolean;
    economySigned: boolean;
    controlSigned: boolean;
}