import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Car } from '../models/Car';
import { PaginatedResult } from '../models/pagination';
import { UserParams } from '../models/userParams';
import { AccountService } from './account.service';

const httpOptions = {
  headers: new HttpHeaders({
    AuthorizationHeader:
      'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
  }),
};

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  baseUrl = environment.apiUrl;
  carCache = new Map();
  userParams: UserParams;


  constructor(private http: HttpClient, private accountService: AccountService) {
    this.userParams = new UserParams();
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams();
    return this.userParams;
  }

  getCars(userParams: UserParams) {
    var response = this.carCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('engineCapacityFrom', userParams.engineCapacityFrom.toString());
    params = params.append('engineCapacityto', userParams.engineCapacityTo.toString());
    params = params.append('priceFrom', userParams.priceFrom.toString());
    params = params.append('priceTo', userParams.priceTo.toString());
    params = params.append('kilometrageFrom', userParams.kilometrageFrom.toString());
    params = params.append('kilometrageTo', userParams.kilometrageTo.toString());
    params = params.append('city', userParams.city);
    params = params.append('orderBy', userParams.orderBy);
    
    return this.getPaginatedResult<Car[]>(this.baseUrl + 'cars', params)
      .pipe(map(response => {
        this.carCache.set(Object.values(userParams).join('-'), response);
        return response;
      }));
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
