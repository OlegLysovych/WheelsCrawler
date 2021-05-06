import { Component, Input, OnInit } from '@angular/core';
import { Car } from '../models/Car';

@Component({
  selector: 'app-cars-card',
  templateUrl: './cars-card.component.html',
  styleUrls: ['./cars-card.component.css'],
})
export class CarsCardComponent implements OnInit {
  @Input() car: Car;

  constructor() {}

  ngOnInit(): void {}

  sourceLogo(car: Car) {
    if (car.carUri.includes('rst.ua')) return '../../../../assets/rst1.png';
    else return '../../../../assets/ria.png';
  }

  showShortDesciption = true

 alterDescriptionText() {
    this.showShortDesciption = !this.showShortDesciption
 }
}
