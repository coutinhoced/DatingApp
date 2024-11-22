import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  ngOnInit(): void {
  }

  resisterMode : boolean = false;
  http = inject(HttpClient);
  users: any;

  registerToggle(){
    this.resisterMode = !this.resisterMode;
  }

  getallUsers(){
     this.http.post("https://localhost:7032/api/User/GetUsers", {}).subscribe(
      {
        next:(response)=>{ 
          this.users = response
        },
        error:(error) =>{console.log(error)},
        complete:()=>{console.log(this.users)}
      }
    ) 
  }

  cancelRegisterMode(event : boolean){
     this.resisterMode = event;
  }

}
