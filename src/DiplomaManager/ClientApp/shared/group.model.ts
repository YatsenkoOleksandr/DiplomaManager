export class Group {
    id: number;
    name: string;
    degreeId: number;

    constructor(id: number, name: string, degreeId: number) {
        this.id = id;
        this.name = name;
        this.degreeId = degreeId;
    }
}