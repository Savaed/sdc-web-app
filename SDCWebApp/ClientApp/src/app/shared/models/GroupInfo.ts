import { BaseResponseDataType } from './BaseResponseDataType';

export interface GroupInfo extends BaseResponseDataType {
    sightseeingDate: Date;
    availablePlaces: number;
}
