import { Component, OnInit } from '@angular/core';
import { Car } from '../models/Car';
import { CarsService } from '../_services/cars.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent implements OnInit {
  cars: Car[];

  constructor(private carService: CarsService) { }

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars() {
    this.carService.getCars().subscribe(cars => {
      this.cars = cars;
    });
  }
}
