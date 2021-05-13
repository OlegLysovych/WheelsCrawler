import { Injectable } from '@angular/core';
import {
  Router,
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CarBrand } from '../models/CarBrand';
import { SearchService } from '../_services/search.service';

@Injectable({
  providedIn: 'root',
})
export class SearchBrandsResolver implements Resolve<CarBrand[]> {
  /**
   *
   */
  constructor(
    private searchService: SearchService,
    private router: Router,
    private toastr: ToastrService
  ) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<CarBrand[]> {
    return this.searchService.getBrands().pipe(
      catchError((error) => {
        this.toastr.error('Problem retrieving carBrands');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}
