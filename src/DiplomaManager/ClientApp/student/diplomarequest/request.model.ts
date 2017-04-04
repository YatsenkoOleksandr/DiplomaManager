import { SelectItem } from '../../shared/selectItem';
import { Student, StudentFGroup } from '../../shared/student.model';

export class RequestFormGroup {
    das: Array<SelectItem>;
    teachers: Array<SelectItem>;
    studentFGroup: StudentFGroup;

    title: string;
}

export class Request {
    daId: number;
    teacherId: number;

    student: Student;

    title: string;

    constructor(daId: number, teacherId: number, student: Student, title: string) {
        this.daId = daId;
        this.teacherId = teacherId;
        this.student = new Student(0, student.firstNameId, student.lastNameId, student.patronymicId, student.email, student.groupId);
        this.title = title;
    }
}