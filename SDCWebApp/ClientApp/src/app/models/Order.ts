import { Customer } from './Customer';
import { Ticket } from './Ticket';
import { ShallowTicket } from './ShallowTicket';

export interface OrderRequest {
    customer: Customer;
    tickets: ShallowTicket[];
}

export interface OrderResponse {
    id: string;
    customer: Customer;
    tickets: Ticket[];
}
