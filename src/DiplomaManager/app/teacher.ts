export class Teacher {
    id: number;
    firstName: string;
    lastName: string;
    positionName: string;
    patronymic: string;

    constructor(id: number, firstName: string, lastName: string, patronymic: string, positionName: string) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
        this.positionName = positionName;
    }

    public toString = (): string => {
        return `${this.lastName} ${this.firstName.substring(0, 1)}. ${this.patronymic.substring(0, 1)}.`;
    }
} 