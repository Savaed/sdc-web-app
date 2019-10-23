import { BaseResponseDataType } from './BaseResponseDataType';

export interface SightseeingGroup extends BaseResponseDataType {
    sightseeingDate: Date;
    maxGroupSize: number;
    currentGroupSize: number;
    isAvailablePlace: boolean;
}
