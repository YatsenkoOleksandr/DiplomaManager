import { TeacherInfo } from "../../shared/teacher.model";

export class PredefenseTeacherCapacity {
    id: number;
    predefensePeriodId: number;
    teacherId: number;
    teacher: TeacherInfo;
    total: number;
    count: number;
}