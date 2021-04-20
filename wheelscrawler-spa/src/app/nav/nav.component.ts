import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {}

  ngOnInit() {}

  logIn() {
    this.accountService.logIn(this.model).subscribe(
      (response) => {
        this.router.navigateByUrl('/cars');
      },
      (error) => {
        console.log(error);
        this.toastr.error(error.error);
      }
    );
  }

  logOut() {
    this.accountService.logOut();
    this.router.navigateByUrl('/');
  }
  navbarOpen = false;

  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }

  public displayMenu(event) {
    document.getElementById('navbarCollapse').classList.toggle('show');
  }
}
