import { TicketTariff } from './TicketTariff';
import { BaseResponseDataType } from './BaseResponseDataType';

export interface VisitTariff extends BaseResponseDataType {
    name: string;
    ticketTariffs?: TicketTariff[];
}
