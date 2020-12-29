import { Component, OnInit, Inject } from '@angular/core';
import { SnackbarMessageDataComponent } from '../snackbar-message-data/snackbar-message-data.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { Routes, RouterModule } from '@angular/router';


import { DataService } from '../services/data.service';
import { JsonPipe } from '@angular/common';
import { searchPatient } from '../models/searchPatient';
import { cwd } from 'process';


@Component({
  selector: 'app-search-patient',
  templateUrl: './search-patient.component.html',
  styleUrls: ['./search-patient.component.css', '../bootstrap.css']
})
export class SearchPatientComponent implements OnInit {

  searchResults: Array<any>;

  enableButton: boolean = false;
  enableSearch: boolean = true;
  enableResults: boolean = false;
  searchValue: searchPatient;

  constructor(
    private snackBarTest: MatSnackBar,
    private dataService: DataService,
    private formBuilder: FormBuilder,
    private matInput: MatInputModule,
    private matIcon: MatIconModule,
    ) {     }

     

  ngOnInit(): void {
  }

  // Snackbar popup with the "Patient not found" text, commented out as alternative method using *ngIf conditions is now in place.
  /*openSnackBar(){
    this.snackBarTest.openFromComponent(SnackbarMessageDataComponent, {
      data:{
        message: ' Patient not found!',
        icon: 'error',
        color: 'red'
      },
      duration: 20000,
    });

    this.enableButton = true;
    this.enableSearch = false;
  } */

  //Sends a SearchPatient object to data.service.ts searchPatient()
  onSubmit(searchForm) {
    console.log(searchForm.value)
    this.searchValue = searchForm.value;
    this.dataService.searchPatient(searchForm.value).subscribe({
      next: data => {
        this.searchResults = data;
        console.log(data);
        this.enableResults = true;
        this.enableSearch = false;
      },
      error: error => {
        console.error(error);
        this.enableButton = true;
        this.enableSearch = false;
      }
      });
  }

  diffSearch(){
    this.enableButton=false;
    this.enableSearch=true;
  }

  //Clears the opposite search bar(s) on input
  inputSearch(i: number) {
    console.log("test")
    if(i == 1) {
      document.getElementById("hospitalNumber");
    }
  }

  //sets pass patient in the data service so view patient can retrieve it
  passPatient(item){
    this.dataService.passPatient = item; 
    console.log(item);
  }

}
