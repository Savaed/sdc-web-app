import { BaseResponseDataType } from './BaseResponseDataType';

export interface Article extends BaseResponseDataType {
    title: string;
    text: string;
    author?: string;
}
