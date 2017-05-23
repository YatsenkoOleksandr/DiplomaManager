import { SelectItem } from './selectItem';

export abstract class User {
    id: number;
    email: string;

    protected constructor(id: number, email: string) {
        this.id = id;
        this.email = email;
    }
}

export abstract class UserInfo {
    id: number;
    fullName: string;
    shortName: string;
    email: string;

    protected constructor(id: number, shortName: string, fullName: string, email: string) {
        this.id = id;
        this.fullName = fullName;
        this.shortName = shortName;
        this.email = email;
    }
} 

export abstract class UserFGroup {
    users: Array<SelectItem>;
    email: string;

    protected constructor(users: Array<SelectItem>, email: string) {
        this.users = users;
        this.email = email;
    }
} 