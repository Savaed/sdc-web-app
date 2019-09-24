import { BaseResponseDataType } from './BaseResponseDataType';

export interface Ticket extends BaseResponseDataType {
    ticketUniqueId: string;
    purchaseDate: Date;
    validFor: Date;
    price: number;
    _links: ApiLink[];
}

export interface ApiLink {
    rel: string;
    href: string;
    method: string;
}
