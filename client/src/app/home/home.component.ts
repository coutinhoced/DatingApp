import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { AccountService } from '../_services/account.service';


@Component({
    selector: 'app-home',
    imports: [RegisterComponent],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  accountService = inject(AccountService);

  ngOnInit(): void { 
    
  }

  resisterMode : boolean = false;
  users: any;

  registerToggle(){
    debugger;
    this.resisterMode = !this.resisterMode;
  }  

  cancelRegisterMode(event : boolean){
     this.resisterMode = event;
  }

}
