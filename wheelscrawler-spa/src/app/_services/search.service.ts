import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Car } from '../models/Car';
import { CarBrand } from '../models/CarBrand';
import { CarFuel } from '../models/CarFuel';
import { CarGearbox } from '../models/CarGearbox';
import { CarModel } from '../models/CarModel';
import { CarType } from '../models/CarType';
import { PaginatedResult } from '../models/pagination';
import { SearchRequestParams } from '../models/SearchRequestParams';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  baseUrl = environment.apiUrl;
  carCache = new Map();
  searchParams: SearchRequestParams;


  constructor(private http: HttpClient, private accountService: AccountService) {
    this.searchParams = new SearchRequestParams();
  }

  getSearchParams() {
    return this.searchParams;
  }

  setSearchParams(params: SearchRequestParams) {
    this.searchParams = params;
  }

  resetSearchParams() {
    this.searchParams = new SearchRequestParams();
    return this.searchParams;
  }

  getCrawledCars(searchParams: SearchRequestParams) {
    var response = this.carCache.get(Object.values(searchParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = this.getPaginationHeaders(searchParams.pageNumber, searchParams.pageSize);

    params = params.append('brand', searchParams.Brand.toString());
    params = params.append('model', searchParams.Model.toString());
    params = params.append('fuel', searchParams.Fuel.toString());
    params = params.append('gearbox', searchParams.Gearbox.toString());
    params = params.append('isNeedToSave', searchParams.IsNeedToSave.toString());

    params = params.append('engineCapacityFrom', searchParams.engineCapacityFrom.toString());
    params = params.append('engineCapacityto', searchParams.engineCapacityTo.toString());
    params = params.append('priceFrom', searchParams.priceFrom.toString());
    params = params.append('priceTo', searchParams.priceTo.toString());
    params = params.append('kilometrageFrom', searchParams.kilometrageFrom.toString());
    params = params.append('kilometrageTo', searchParams.kilometrageTo.toString());
    params = params.append('city', searchParams.city);
    params = params.append('orderBy', searchParams.orderBy);
    params = params.append('exactUrl', searchParams.exactUrl);
    
    return this.getPaginatedResult<Car[]>(this.baseUrl + 'search/crawl', params)
      .pipe(map(response => {
        this.carCache.set(Object.values(searchParams).join('-'), response);
        return response;
      }));
  }

  getBrands() {
    return this.http.get<Partial<CarBrand[]>>(this.baseUrl + 'search/brands');
  }
  getModels() {
    return this.http.get<Partial<CarModel[]>>(this.baseUrl + 'search/models');
  }
  getFuels() {
    return this.http.get<Partial<CarFuel[]>>(this.baseUrl + 'search/fuels');
  }
  getTypes() {
    return this.http.get<Partial<CarType[]>>(this.baseUrl + 'search/types');
  }
  getGearboxes() {
    return this.http.get<Partial<CarGearbox[]>>(this.baseUrl + 'search/gearboxes');
  }

  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http
      .get<T>(url, { observe: 'response', params })
      .pipe(
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return params;
  }


}
