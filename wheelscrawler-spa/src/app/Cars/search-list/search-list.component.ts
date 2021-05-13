import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, take } from 'rxjs/operators';
import { Car } from 'src/app/models/Car';
import { CarBrand } from 'src/app/models/CarBrand';
import { CarFuel } from 'src/app/models/CarFuel';
import { CarGearbox } from 'src/app/models/CarGearbox';
import { CarModel } from 'src/app/models/CarModel';
import { CarType } from 'src/app/models/CarType';
import { Pagination } from 'src/app/models/pagination';
import { SearchRequestParams } from 'src/app/models/SearchRequestParams';
import { Url } from 'src/app/models/Url';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/_services/account.service';
import { SearchService } from 'src/app/_services/search.service';
@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.css'],
})
export class SearchListComponent implements OnInit {
  @Input() url: string;
  cars: Car[];
  pagination: Pagination;
  searchParams: SearchRequestParams;
  interestedUrls: Url[];

  brands: CarBrand[];
  models: CarModel[];
  fuels: CarFuel[];
  gearboxes: CarGearbox[];
  types: CarType[];

  constructor(
    private searchService: SearchService,
    private accService: AccountService,
    private route: ActivatedRoute
  ) {
    this.searchParams = this.searchService.getSearchParams();
    this.accService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.interestedUrls = user.interestedUrls;
    });
  }

  ngOnInit(): void {
    this.getSearchData();
    console.log(this.brands);
    console.log(this.models);
  }

  crawlCars() {
    this.searchService.setSearchParams(this.searchParams);
    this.searchService.getCrawledCars(this.searchParams).subscribe((cars) => {
      this.cars = cars.result;
      this.pagination = cars.pagination;
    });
  }

  pageChanged(event: any) {
    this.searchParams.pageNumber = event.page;
    this.searchService.setSearchParams(this.searchParams);
    this.crawlCars();
  }

  resetFilters() {
    this.searchParams = this.searchService.resetSearchParams();
    this.crawlCars();
  }

  getSearchData() {
    this.route.data.subscribe(data => {
      this.brands = data['brands'];
    });
    this.route.data.subscribe(data => {
      this.models = data['models'];
    });
    // this.route.data.subscribe(data => {
    //   this.fuels = data['fuels'];
    // });
    // this.route.data.subscribe(data => {
    //   this.gearboxes = data['gearboxes'];
    // });
    // this.route.data.subscribe(data => {
    //   this.types = data['types'];
    // });
  }

}
