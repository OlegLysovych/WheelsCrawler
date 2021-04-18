import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'wheelscrawler-spa';
  cars: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getCars();
    console.log(this.cars);
  }
  getCars() {
    this.http.get('https://localhost:5001/cars').subscribe(
      (response) => {
        this.cars = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
