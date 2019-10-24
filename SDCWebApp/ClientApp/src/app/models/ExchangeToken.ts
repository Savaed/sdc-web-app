import { JwtToken } from './JwtToken';

export interface ExchangeToken {
    accessToken: JwtToken;
    refreshToken: JwtToken;
}
