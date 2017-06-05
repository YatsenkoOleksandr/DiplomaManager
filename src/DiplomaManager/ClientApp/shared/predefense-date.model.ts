import { Predefense } from "./predefense.model";

export class PredefenseDate {
    id: number;
    predefensePeriodId: number;
    date: Date;
    beginTime: Date;
    finishTime: Date;

    predefenses: Array<Predefense>;
}