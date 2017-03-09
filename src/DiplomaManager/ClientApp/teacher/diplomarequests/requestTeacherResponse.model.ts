import { Student } from '../../shared/student.model';
import { ProjectTitle } from './projectTitle.model';

export class RequestTeacher {
    id: number;
    projectTitles: Array<ProjectTitle>;
    student: Student;
    creationDate: Date;
    practiceJournalPassed: Date;
    accepted: boolean;

    constructor(id: number, projectTitles: Array<ProjectTitle>, student: Student, creationDate: Date, practiceJournalPassed: Date, accepted: boolean) {
        this.id = id;
        this.projectTitles = projectTitles;
        this.student = student;
        this.creationDate = creationDate;
        this.practiceJournalPassed = practiceJournalPassed;
        this.accepted = accepted;
    }
}