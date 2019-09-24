import { JwtToken } from './JwtToken';
import { User } from './User';

export interface LoginInfo {
    user: User;
    accessToken: JwtToken;
    refreshToken: JwtToken;
}
