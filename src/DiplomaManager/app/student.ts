abstract class User {
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

export class Student extends User {
    groupId: number;
    
    constructor(firstName: string, lastName: string, patronymic: string, email: string, groupId: number) {
        super(firstName, lastName, patronymic, email);
        this.groupId = groupId;
    }
}