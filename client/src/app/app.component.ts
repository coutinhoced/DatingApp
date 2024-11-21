import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
 
  http = inject(HttpClient);
  private accountService = inject(AccountService);
  title = 'Love';
  users: any;

  ngOnInit(): void {
    this.setCurrentUser();
   /*  this.http.post("https://localhost:7032/api/User/GetUsers", {}).subscribe(
      {
        next:(response)=>{ 
          this.users = response
        },
        error:(error) =>{console.log(error)},
        complete:()=>{console.log(this.users)}
      }
    ) */
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;

    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);

  }

}
