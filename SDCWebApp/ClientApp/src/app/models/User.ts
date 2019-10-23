import { BaseResponseDataType } from './BaseResponseDataType';

export interface User extends BaseResponseDataType {
    userName: string;
    loggedOn: Date;
}
