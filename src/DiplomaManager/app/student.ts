import { SelectItem } from './selectItem';

abstract class StudentInfo {
    firstName: string;
    lastName: string;
    patronymic: string;
    email: string;

    constructor(firstName: string, lastName: string, patronymic: string, email: string) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
        this.email = email;
    }
}

export class StudentFGroup extends StudentInfo {
    firstName: string;
    lastName: string;
    patronymic: string;
    email: string;
    degrees: Array<SelectItem>;

    constructor(firstName: string, lastName: string, patronymic: string, email: string) {
        super(firstName, lastName, patronymic, email);
    }
}

export class Student extends StudentInfo {
    degreeId: number;
    
    constructor(firstName: string, lastName: string, patronymic: string, email: string, degreeId: number) {
        super(firstName, lastName, patronymic, email);
        this.degreeId = degreeId;
    }
}