import { DOCUMENT } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ElementRef, Inject, Renderer2, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { User } from './models/user';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  private _router: Subscription;
  @ViewChild(NavComponent) navbar: NavComponent;
  title = 'Wheels';
  cars: any;

  constructor(
    private accountService: AccountService,
    private renderer: Renderer2,
    private router: Router,
    @Inject(DOCUMENT) private document: any,
    private element: ElementRef
  ) {}

  ngOnInit() {
    var navbar: HTMLElement =
      this.element.nativeElement.children[0].children[0];

    if (localStorage.getItem('user')) this.setCurrentUser();

    this.renderer.listen('window', 'scroll', (event) => {
      const number = window.scrollY;
      if (
        (this.router.url !== '/' && number > 150) ||
        window.pageYOffset > 150
      ) {
        // add logic
        navbar.classList.remove('navbar-transparent');
      } else {
        // remove logic
        if (this.router.url === '/') navbar.classList.add('navbar-transparent');
      }
    });
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
    }
  }
}
