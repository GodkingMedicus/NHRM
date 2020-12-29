import { Component, OnInit } from '@angular/core';
import { SnackbarMessageDataComponent } from '../snackbar-message-data/snackbar-message-data.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-patient',
  templateUrl: './add-patient.component.html',
  styleUrls: ['./add-patient.component.css', '../bootstrap.css']
})
export class AddPatientComponent implements OnInit {


  failed: boolean;
  err: string;
  password: string;

  titles = ['Mr.', 'Mrs.','Miss.', 'Dr.'];
  genders = ['Male', 'Female', 'Other'];

  dictionary: Array<String>;
  passwordLength: Number = 8;
  
  constructor(private snackBarTest: MatSnackBar, private router: Router, private dataService: DataService) { }

  ngOnInit(): void {
  }
  

  openSnackBar(){
    this.snackBarTest.openFromComponent(SnackbarMessageDataComponent, {
      data: {
        message: ' Successfully added patient.',
        icon: 'done',
      },
      duration: 2000,
    });
  }

  disableTest(){

  }

  generatePassword() {
    var length = 8,
        charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
        retVal = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
        retVal += charset.charAt(Math.floor(Math.random() * n));
    }
    var inputElement = <HTMLInputElement>document.getElementById('password');
    inputElement.value = (retVal);
    this.password = retVal;
}


  addPatient(form) {
    console.log(this.password)
    //form.value['dob'] = form.value['dob'].toISOString();
    form.value['password'] = this.password;
    console.log(form.value)
    this.dataService.addPatient(form.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      })
      .finally(() => {
        this.openSnackBar();
        //this.router.navigate(['/dashboard']);
        form.reset();
      });
    
  }
}

