import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  constructor() { }

  getMember(username : string){
    var input = {name : username};
    return this.http.post<Member[]>(this.baseUrl + 'User/GetUsers', input);
  }

  getMembers(){   
    return this.http.post<Member[]>(this.baseUrl + 'User/GetUsers', {});
  }

  updateMember(member: Member){
    debugger;
    return this.http.put(this.baseUrl + 'User/UpdateUser', member);
  }

  
}
