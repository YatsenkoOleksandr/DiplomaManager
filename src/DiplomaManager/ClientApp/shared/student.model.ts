import { User } from '../shared/user.model';

export class Student extends User {
    groupId: number;
    
    constructor(id: number, firstName: string, lastName: string, patronymic: string, email: string, groupId: number) {
        super(id, firstName, lastName, patronymic, email);
        this.groupId = groupId;
    }
}