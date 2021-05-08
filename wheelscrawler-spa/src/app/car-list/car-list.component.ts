import { Component, OnInit } from '@angular/core';
import { Car } from '../models/Car';
import { Pagination } from '../models/pagination';
import { UserParams } from '../models/userParams';
import { CarsService } from '../_services/cars.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  cars: Car[];
  pagination: Pagination;
  userParams: UserParams;


  constructor(private carService: CarsService) {
     this.userParams = this.carService.getUserParams();
  }

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars() {
    this.carService.setUserParams(this.userParams);
    this.carService.getCars(this.userParams).subscribe(cars => {
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
