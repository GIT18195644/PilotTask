export interface Response<T> {
    succeeded: boolean;
    message: string | null;
    payload: T | null;
}