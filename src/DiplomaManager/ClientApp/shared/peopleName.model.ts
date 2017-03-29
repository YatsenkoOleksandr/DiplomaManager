export enum NameKind {
    FirstName,
    LastName,
    Patronymic
}

export class PeopleName {
    id: number;
    localeId: number;
    name: string;
    nameKind: NameKind;
    creationDate: Date;
}