import { User } from '../shared/user.model';

export class Teacher extends User {
    positionName: string;

    constructor(id: number, firstName: string, lastName: string, patronymic: string, positionName: string, email: string) {
        super(id, firstName, lastName, patronymic, email);
        this.positionName = positionName;
    }
} 