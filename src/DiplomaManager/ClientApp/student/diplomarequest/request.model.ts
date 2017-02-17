import { SelectItem } from '../../shared/selectItem';
import { Student } from '../../shared/student.model';

export class RequestFormGroup {
    das: Array<SelectItem>;
    teachers: Array<SelectItem>;
    studentFGroup: Student;

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
        this.student = new Student(0, student.firstName, student.lastName, student.patronymic, student.email, student.groupId);
        this.title = title;
    }
}