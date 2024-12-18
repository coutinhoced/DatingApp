import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { Observable, observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  memebers = signal<Member[]>([]);
  constructor() { }

  getMember(username : string){   
    var input = {name : username};
    const member = this.memebers().find(x => x.userName === username);
    if(member !== undefined){
      return new Observable<Member[]>((observer)=>{
        observer.next([member]);
        observer.complete();
      });
    }
    return this.http.post<Member[]>(this.baseUrl + 'User/GetUsers', input);
  }

  getMembers(){       
    return this.http.post<Member[]>(this.baseUrl + 'User/GetUsers', {}).subscribe({
         next: memebers => this.memebers.set(memebers)
    });

  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + 'User/UpdateUser', member);
    
  }

  
}
