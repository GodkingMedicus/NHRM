import { Component, OnInit } from '@angular/core';
import { SnackbarMessageDataComponent } from '../snackbar-message-data/snackbar-message-data.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { Routes, RouterModule } from '@angular/router';
import { DataService } from '../services/data.service';
import { Staff } from '../models/staff';

@Component({
  selector: 'app-search-staff',
  templateUrl: './search-staff.component.html',
  styleUrls: ['./search-staff.component.css', '../bootstrap.css']
})
export class SearchStaffComponent implements OnInit {

  searchResults: Array<any>;

  enableButton: boolean = false;
  enableSearch: boolean = true;
  enableResults: boolean = false;
  searchValue: Staff;


  constructor(
    private snackBarTest: MatSnackBar,
    private dataService: DataService,
    private formBuilder: FormBuilder,
    private matInput: MatInputModule,
    private matIcon: MatIconModule,
    ) {     }


  ngOnInit(): void {
  }

  onSubmit(searchForm) {
    console.log(searchForm.value)
    this.searchValue = searchForm.value;
    this.dataService.searchStaff(searchForm.value).subscribe({
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
      document.getElementById("staffID");
    }
  }

  //sets pass staff in the data service so accountDetails can retrieve it
  passStaff(item){
    this.dataService.passStaff = item; 
    console.log(item);
  }


}
