import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CarModel } from '../models/CarModel';
import { SearchService } from '../_services/search.service';

@Injectable({
  providedIn: 'root'
})
export class SearchModelsResolver implements Resolve<CarModel[]> {
  constructor(
    private searchService: SearchService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<CarModel[]> {
    return this.searchService.getModels().pipe(
      catchError((error) => {
        this.toastr.error('Problem retrieving carBrands');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}
