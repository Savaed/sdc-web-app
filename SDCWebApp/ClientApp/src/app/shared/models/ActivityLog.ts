import { BaseResponseDataType } from './BaseResponseDataType';

export interface ActivityLog extends BaseResponseDataType {
    date: Date;
    user: string;
    type: ActivityLogType;
    description: string;
}

export enum ActivityLogType {
    LogIn,
    LogOut,
    PasswordChange,
    CreateAccount,
    DeleteAccount,
    CreateResource,
    EditResource,
    GetResource,
    DeleteResource,
    StatisticCreate,
    StatisticDownload
}
