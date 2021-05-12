import { Url } from "./Url";

export interface User {
    username: string;
    token: string;
    roles: string[];
    interestedUrls?: Url[];
}
