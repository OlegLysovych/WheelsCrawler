import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { take } from 'rxjs/operators';
import { Car } from 'src/app/models/Car';
import { Pagination } from 'src/app/models/pagination';
import { SearchRequestParams } from 'src/app/models/SearchRequestParams';
import { Url } from 'src/app/models/Url';
import { UserParams } from 'src/app/models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { CarsService } from 'src/app/_services/cars.service';

@Component({
  selector: 'app-car-list-detailed',
  templateUrl: './car-list-detailed.component.html',
  styleUrls: ['./car-list-detailed.component.css']
})
export class CarListDetailedComponent implements OnInit {
  @Input() url: string;
  @Output() myEvent = new EventEmitter();
  cars: Car[];
  pagination: Pagination;
  userParams: UserParams;
  interestedUrls: Url[];
  constructor(
    private carService: CarsService,
    private accService: AccountService
  ) {
    this.userParams = this.carService.getUserParams();
    this.accService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.interestedUrls = user.interestedUrls;
    });
  }

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars() {
    this.userParams.exactUrl = this.url === undefined ? '' : this.url;
    this.carService.setUserParams(this.userParams);
    this.carService.getCars(this.userParams).subscribe((cars) => {
      this.cars = cars.result;
      this.pagination = cars.pagination;
    });
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.carService.setUserParams(this.userParams);
    this.loadCars();
  }

  resetFilters() {
    this.userParams = this.carService.resetUserParams();
    this.loadCars();
  }
}

