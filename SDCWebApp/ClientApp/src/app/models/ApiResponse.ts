export interface ApiResponse<T> {
    data: T;
    error: ApiError;
}

export interface ApiError {
    statusCode: number;
    type: string;
    message: string;
}
