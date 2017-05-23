import { UserInfo } from '../shared/user.model';

export class TeacherInfo extends UserInfo {
    positionName: string;

    constructor(id: number, shortName: string, fullName: string, positionName: string, email: string) {
        super(id, shortName, fullName, email);
        this.positionName = positionName;
    }
} 