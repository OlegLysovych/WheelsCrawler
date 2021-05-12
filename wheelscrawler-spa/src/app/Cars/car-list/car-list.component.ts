import { Component, Input, OnInit } from '@angular/core';
import { map, take } from 'rxjs/operators';
import { Url } from 'src/app/models/Url';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/_services/account.service';
@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css'],
})
export class CarListComponent implements OnInit {
  @Input() url: Url;

  constructor(private accService: AccountService) {
    this.accService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.user = user;
      this.interestedUrls = user.interestedUrls;
    });
  }
  interestedUrls: Url[];
  user: User;
  counter: number = 0;

  ngOnInit(): void {
  }

  removeTabHandler(tab: any): void {
    this.interestedUrls.splice(this.interestedUrls.indexOf(tab), 1);
    console.log('Remove Tab handler');
  }

  transferUrlWithTimeOut(url) {
    if (this.counter == 0) {
      this.counter++;
      console.log(this.counter);
      return url;
    } else {
      setTimeout(() => {
        this.counter++;
        console.log(this.counter);
        return url;
      }, 10000);
    }
  }
}

