import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";

@Component({
    selector: 'app-home',
    imports: [RegisterComponent],
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  ngOnInit(): void {  }

  resisterMode : boolean = false;
  users: any;

  registerToggle(){
    this.resisterMode = !this.resisterMode;
  }  

  cancelRegisterMode(event : boolean){
     this.resisterMode = event;
  }

}
