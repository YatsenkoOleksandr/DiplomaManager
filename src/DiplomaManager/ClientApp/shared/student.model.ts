import { User, UserFGroup, UserInfo } from '../shared/user.model';
import { SelectItem } from './selectItem';

export class Student extends User {
    groupId: number;
    
    constructor(id: number, firstName: number, lastName: number, patronymic: number, email: string, groupId: number) {
        super(id, firstName, lastName, patronymic, email);
        this.groupId = groupId;
    }
}

export class StudentInfo extends UserInfo {
    groupId: number;

    constructor(id: number, firstName: string, lastName: string, patronymic: string, email: string, groupId: number) {
        super(id, firstName, lastName, patronymic, email);
        this.groupId = groupId;
    }
}

export class StudentFGroup extends UserFGroup {
    groups: Array<SelectItem>;

    constructor(id: number, firstNames: Array<SelectItem>, lastNames: Array<SelectItem>, patronymics: Array<SelectItem>,
        email: string, groups: Array<SelectItem>) {
        super(id, firstNames, lastNames, patronymics, email);
        this.groups = groups;
    }
}