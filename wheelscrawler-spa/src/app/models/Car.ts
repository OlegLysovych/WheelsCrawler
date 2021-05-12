import { Url } from "./Url";

export interface Car {
    id:             number;
    description:    string;
    name:           string;
    pictureUri:     string;
    carUri:         string;
    price:          number;
    kilometrage:    number;
    enginecapacity: number;
    city:           string;
    plate?:          string;//////
    publishdate:    string;
    cargearbox:     string;//////
    carbrand:       string;
    cartype:        string;
    carfuel:        string;
    carmodel:       string;
}
