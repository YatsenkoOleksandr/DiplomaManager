import { Student } from '../../shared/student.model'

export class RequestTeacher {
    id: number;
    projectTitle: string;
    student: Student;
    creationDate: Date;
    accepted: boolean;

    constructor(id: number, projectTitle: string, student: Student, creationDate: Date, accepted: boolean) {
        this.id = id;
        this.projectTitle = projectTitle;
        this.student = student;
        this.creationDate = creationDate;
        this.accepted = accepted;
    }
}