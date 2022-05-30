import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};
  title = "Dating app";
  currentUser$: Observable<User> = new Observable<User>();

  constructor(public accountServices: AccountService) { }

  ngOnInit(): void {
  }

  login() {
    this.accountServices.login(this.model)
      .subscribe(
        response => {
          console.log(response);
        },
        error => {
          console.log(error)
        });
  }

  logout() {
    this.accountServices.logout();
  }
}
