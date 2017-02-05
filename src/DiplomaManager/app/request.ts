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
}