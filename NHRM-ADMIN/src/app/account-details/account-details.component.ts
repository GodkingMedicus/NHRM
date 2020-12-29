import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackbarMessageDataComponent } from '../snackbar-message-data/snackbar-message-data.component';
import { FormBuilder, FormControl, Form, FormGroup, NgForm } from '@angular/forms';
import { Staff } from '../models/staff';


@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css', '../bootstrap.css']
})
export class AccountDetailsComponent implements OnInit {

  failed: boolean;
  err: string;
  form: FormGroup;
  passForm: FormGroup;

  data: Staff;


  //presets the icon to an arrow up https://material.io/resources/icons/
  icon = 'keyboard_arrow_up';
  //defines isDisplayed variable used for showing/hiding parts of the form
  isDisplayed:boolean = true;
  isEditable:boolean = false;

  titles = ['Mr.', 'Mrs.','Miss.', 'Dr.'];
  roles = ['Admin', 'Clinician', 'Other'];

  

  constructor(private dataService: DataService, private snackBarTest: MatSnackBar) { }

  //Form is based off the example we were shown by Nick, image is attached to the Account Details jira item

  ngOnInit(): void {

    this.data = this.dataService.passStaff;
    console.log(this.data)


    this.form = new FormGroup({
      'email': new FormControl(this.data.email),
      'firstName': new FormControl(this.data.firstName),
      'surname': new FormControl(this.data.surname),
      'roleId': new FormControl(this.data.roleId)
    })

    this.passForm = new FormGroup({
      'password': new FormControl(this.data.password)
    })

    this.form.disable();

  }



  showMe()
  {
    if(this.isDisplayed)
    {
      //Hides form and sets the icon to arrow down
      this.isDisplayed = false;
      this.icon = 'keyboard_arrow_down';
    }
    else
    {
      //Shows the form and sets the icon to arrow up
      this.isDisplayed = true;
      this.icon = 'keyboard_arrow_up';
    }
  }

  //placeholder for api
  addStaff(form) {
    console.log(form.value)
    this.dataService.addStaff(form.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      });
  }

  openSnackBar(){
    this.snackBarTest.openFromComponent(SnackbarMessageDataComponent, {
      data: {
        message: ' Successfully updated Staff.',
        icon: 'done',
      },
      duration: 2000,
    });
  }

  onClick(){
    // this.isEditable = true;
    // this.form.enable();

    if(this.isEditable){
      this.isEditable = false;
      this.form.disable();
    }
    else{
      this.isEditable = true;
      this.form.enable();
    }
  }

  editStaff(form) {
    console.log("test");
    this.data.email = form.value.email;
    this.data.firstName = form.value.firstName;
    this.data.surname = form.value.surname;
    this.data.roleId = form.value.roleId;
    console.log(form.value.roleId);
    
    console.log(this.data)
    this.dataService.editStaff(form.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      })
      .finally(() => {
        this.openSnackBar();
        form.disable();
        this.isEditable = false;
      });

  }

  updatePassword(form) {
    console.log(form.value);
    this.dataService.editStaffPassword(form.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      })
      .finally(() => {
        this.openPWSnackBar();
        this.passForm.disable();
      });
  }

  openPWSnackBar(){
    this.snackBarTest.openFromComponent(SnackbarMessageDataComponent, {
      data: {
        message: ' Successfully updated password.',
        icon: 'done',
      },
      duration: 2000,
    });
  }

}

