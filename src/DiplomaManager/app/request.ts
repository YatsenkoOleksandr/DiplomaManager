import { SelectItem } from './selectItem';

export class RequestFormGroup {
    das: Array<SelectItem>;
    teachers: Array<SelectItem>;

    firstName: string;
    lastName: string;
    patronymic: string;
    title: string;
}

export class Request {
    daId: number;
    teacherId: number;
    firstName: string;
    lastName: string;
    patronymic: string;
    title: string;

    constructor(daId: number, teacherId: number, firstName: string, lastName: string,
        patronymic: string, title: string) {
        this.daId = daId;
        this.teacherId = teacherId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
        this.title = title;
    }
}