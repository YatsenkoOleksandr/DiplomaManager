import { SearchRequest } from '../../shared/searchRequest.model';

export class GetProjectsRequest extends SearchRequest {
    teacherId: number;

    constructor(teacherId: number, query: string, limit: number, currentPage: number) {
        super(query, limit, currentPage);
        this.teacherId = teacherId;
    }
}