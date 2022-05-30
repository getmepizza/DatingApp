import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;
  baseUrl = 'https://localhost:5001/';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers()
  }

  registerToggle() {
    this.registerMode = true;
  }

  getUsers() {
    this.http.get(this.baseUrl + 'api/user').subscribe(resposne => this.users = resposne)
  }

}
