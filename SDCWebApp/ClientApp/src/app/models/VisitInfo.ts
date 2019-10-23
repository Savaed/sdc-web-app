import { BaseResponseDataType } from './BaseResponseDataType';

export interface VisitInfo extends BaseResponseDataType {
    description: string;
    maxChildAge: number;
    maxAllowedGroupSize: number;
    maxTicketOrderInterval: number;
    sightseeingDuration: number;
    openingHours: Array<OpeningHours>;
}

export interface OpeningHours {
    openingHour: Date;
    closingHour: Date;
    dayOfWeek: DayOfWeek;
}

export enum DayOfWeek {
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}
