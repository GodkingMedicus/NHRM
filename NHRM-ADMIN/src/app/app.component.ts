import { Component } from '@angular/core';
import {DataService} from './services/data.service';
import {Router} from '@angular/router';
import {Location} from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NgxSpinner } from 'ngx-spinner/lib/ngx-spinner.enum';
import { NgxSpinnerService } from 'ngx-spinner';
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'NHRM-ADMIN';

  loggedIn: boolean;
  isAdmin: boolean;

  constructor(private _location: Location, private router: Router, private jwtHelper: JwtHelperService, private authService: AuthService, private spinner: NgxSpinnerService, private dataService: DataService){
    this.authService.loggedIn.subscribe(data => {
      this.loggedIn = data;
    })
    this.authService.isAdmin.subscribe(data => {
      this.isAdmin = data;
    })
    
    this.loadingSpinner();
  }

  logout(){
    this.authService.logout();
  }

  goBack(){
    this._location.back();
  }

  loadingSpinner() {
    this.dataService.loading.subscribe((value) => {
      if (value) {
        this.spinner.show();
      } else {
        this.spinner.hide();
      }
    });
  }
}