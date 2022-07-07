import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegistration = new EventEmitter<boolean>();

  model: any = {}
  constructor(public accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).
      subscribe(
        response => {
          console.log(response);
          this.cancel();
        },
        error => {
          this.toastr.error(error.error);
        }
      );
  }

  cancel() {
    this.cancelRegistration.emit(false);
  }
}
