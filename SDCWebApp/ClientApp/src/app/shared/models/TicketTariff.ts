import { BaseResponseDataType } from './BaseResponseDataType';

export interface TicketTariff extends BaseResponseDataType {
    description: string;
    isPerHour: boolean;
    isPerPerson: boolean;
    defaultPrice: number;
}
