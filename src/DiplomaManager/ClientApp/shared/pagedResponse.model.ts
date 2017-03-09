export interface IPagedResponse<T> {
    total: number;
    data: T[];
}