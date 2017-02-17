export abstract class User {
    id: number;
    firstName: string;
    lastName: string;
    patronymic: string;
    email: string;

    constructor(id: number, firstName: string, lastName: string, patronymic: string, email: string) {
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