import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Car } from '../models/Car';

const httpOptions = {
  headers: new HttpHeaders({
    AuthorizationHeader: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class CarsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCars() {
    return this.http.get<Car[]>(this.baseUrl + 'cars');
  }
}
