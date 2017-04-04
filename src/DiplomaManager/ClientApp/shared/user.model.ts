import { SelectItem } from './selectItem';

export abstract class User {
    id: number;
    firstNameId: number;
    lastNameId: number;
    patronymicId: number;
    email: string;

    protected constructor(id: number, firstName: number, lastName: number, patronymic: number, email: string) {
        this.id = id;
        this.firstNameId = firstName;
        this.lastNameId = lastName;
        this.patronymicId = patronymic;
        this.email = email;
    }
}

export abstract class UserInfo {
    id: number;
    firstName: string;
    lastName: string;
    patronymic: string;
    email: string;

    protected constructor(id: number, firstName: string, lastName: string, patronymic: string, email: string) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
        this.email = email;
    }

    toString() {
        return `${this.lastName} ${this.firstName.substring(0, 1)}. ${this.patronymic.substring(0, 1)}.`;
    }
} 

export abstract class UserFGroup {
    id: number;
    firstNames: Array<SelectItem>;
    lastNames: Array<SelectItem>;
    patronymics: Array<SelectItem>;
    email: string;

    protected constructor(id: number, firstName: Array<SelectItem>, lastName: Array<SelectItem>, patronymic: Array<SelectItem>, email: string) {
        this.id = id;
        this.firstNames = firstName;
        this.lastNames = lastName;
        this.patronymics = patronymic;
        this.email = email;
    }
} 