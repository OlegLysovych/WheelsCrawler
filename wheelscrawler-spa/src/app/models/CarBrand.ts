import { CarModel } from "./CarModel";

export interface CarBrand {
    wheelsName: string;
    carModels?: CarModel[];
}
