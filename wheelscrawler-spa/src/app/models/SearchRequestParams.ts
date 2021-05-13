import { UserParams } from "./userParams";

export class SearchRequestParams extends UserParams {
    Brand: string = '';
    Model: string = '';
    Fuel: string = '';
    Gearbox: string = '';
    Type: string = '';
    IsNeedToSave: boolean = false;

    // city: string = '';
    // engineCapacityFrom: number = 0.0;
    // engineCapacityTo = 10.0;
    // priceFrom = 0;
    // priceTo = 1_000_000;
    // kilometrageFrom = 0;
    // kilometrageTo = 1_000_000;  
    // pageNumber = 1;
    // pageSize = 21;  
}
