export class SearchRequest {
    query: string;
    limit: number;
    currentPage: number;

    constructor(query: string, limit: number, currentPage: number) {
        this.query = query;
        this.limit = limit;
        this.currentPage = currentPage;
    }
}