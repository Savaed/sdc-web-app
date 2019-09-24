import { BaseResponseDataType } from './BaseResponseDataType';

export interface Customer extends BaseResponseDataType {
    dateOfBirth: Date;
    isChild: boolean;
    isDisabled: boolean;
    hasFamilyCard: boolean;
    emailAddress: string;
}
