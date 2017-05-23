import { User, UserFGroup, UserInfo } from '../shared/user.model';
import { SelectItem } from './selectItem';

export class Student extends User {
    groupId: number;
    
    constructor(id: number, email: string, groupId: number) {
        super(id, email);
        this.groupId = groupId;
    }
}

export class StudentInfo extends UserInfo {
    groupId: number;

    constructor(id: number, shortName: string, fullName: string, email: string, groupId: number) {
        super(id, shortName, fullName, email);
        this.groupId = groupId;
    }
}

export class StudentFGroup extends UserFGroup {
    groups: Array<SelectItem>;

    constructor(users: Array<SelectItem>, email: string, groups: Array<SelectItem>) {
        super(users, email);
        this.groups = groups;
    }
}