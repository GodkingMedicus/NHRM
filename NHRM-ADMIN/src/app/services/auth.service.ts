import { Injectable } from '@angular/core';
import {Login} from '../models/login';
import {HttpClient} from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Staff } from '../models/staff';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  //apiURL = "https://localhost:5001/api";
  apiURL = "http://nhrmadminapi-env.eba-sezdcf9x.us-east-1.elasticbeanstalk.com/api";
  loggedIn: BehaviorSubject<boolean>;
  staff: Staff;
  isAdmin: BehaviorSubject<boolean>;

  constructor(private _http: HttpClient, private jwtHelper: JwtHelperService, private dataService: DataService) { 
    this.loggedIn = new BehaviorSubject(null);
    this.isAdmin = new BehaviorSubject(null);
    this.staff = new Staff;
    this.loggedIn.next(false);
    if(this.isLoggedIn()) {
      this.loggedIn.next(true);
      if(this.jwtHelper.decodeToken(localStorage.getItem('Authorization')).Roleid == "1")
            this.isAdmin.next(true);
          else
            this.isAdmin.next(false);
    }
    else 
      this.loggedIn.next(false);
      
      
  }

  login(credentials: Login){
    this.dataService.loading.next(true);

    return new Promise((resolve, reject) => {
      this._http.post(this.apiURL + "/Auth/admin", credentials).subscribe(
        (token) => {
          console.log("Logged in")
          localStorage.setItem('Authorization', JSON.stringify(token['token']))
          if(this.jwtHelper.decodeToken(token['token']).Roleid == "1")
            this.isAdmin.next(true);
          else
            this.isAdmin.next(false);
          this.loggedIn.next(true);
          resolve();
        },
        err => {
          console.error("Login Error")
          this.loggedIn.next(false);
          reject(err);
        });
    })
  }

  isLoggedIn(){
    return !this.jwtHelper.isTokenExpired();
  }

  logout(){
    localStorage.removeItem('Authorization');
    localStorage.removeItem('StaffID');
    this.loggedIn.next(false);
  }

/*   getStaffID() {
    return this.staff.staffID
  } */


}