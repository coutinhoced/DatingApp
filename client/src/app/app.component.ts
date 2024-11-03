import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
 
  http = inject(HttpClient);
  title = 'Dating Application';
  users: any;

  ngOnInit(): void {
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

}
