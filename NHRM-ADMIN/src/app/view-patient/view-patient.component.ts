import { Component, OnInit} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataService } from '../services/data.service';
import { ActivatedRoute } from '@angular/router';
import { SnackbarMessageDataComponent } from '../snackbar-message-data/snackbar-message-data.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Patient } from '../models/patient';
import { FormBuilder, FormControl, Form, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-view-patient',
  templateUrl: './view-patient.component.html',
  styleUrls: ['./view-patient.component.css', '../bootstrap.css']
})
export class ViewPatientComponent implements OnInit {

  failed: boolean;
  err: string;
  data: Patient;
  isDisplayed:boolean = true;
  passForm: FormGroup;
  

  
  viewForm: FormGroup;

  titles = ['Mr.', 'Mrs.','Miss.', 'Ms.', 'Dr.'];
  genders = ['Male', 'Female', 'Other'];

  url = "http://nhrmadminapi-env.eba-sezdcf9x.us-east-1.elasticbeanstalk.com/api/patients/123456789";
  

  enableSave: boolean = false;
  isEditable:boolean = false;

  


  constructor(private http: HttpClient, private dataService: DataService,
    private route: ActivatedRoute, private fb: FormBuilder, private snackBarTest: MatSnackBar) {

      };
     

  ngOnInit(): void {

    this.data = this.dataService.passPatient;
    


    this.viewForm = new FormGroup({
      'URNumber': new FormControl(this.data.urnumber),
      'title': new FormControl(this.data.title),
      'firstName': new FormControl(this.data.firstName),
      'surName': new FormControl(this.data.surName),
      'gender': new FormControl(this.data.gender),
      'dob': new FormControl(new Date(this.data.dob).toISOString()),
      'countryOfBirth': new FormControl(this.data.countryOfBirth),
      'preferredLanguage': new FormControl(this.data.preferredLanguage),
      'mobileNumber': new FormControl(this.data.mobileNumber),
      'homeNumber': new FormControl(this.data.homeNumber),
      'email': new FormControl(this.data.email),
      'address': new FormControl(this.data.address),
      'suburb': new FormControl(this.data.suburb),
      'postCode': new FormControl(this.data.postCode),
      'registeredBy': new FormControl(this.data.registeredBy),
      'livesAlone': new FormControl(this.data.livesAlone) })

      this.passForm = new FormGroup({
        'password': new FormControl(this.data.password)
      })

    this.viewForm.disable();

    console.log(this.viewForm);
  }

  openSnackBar(){
    this.snackBarTest.openFromComponent(SnackbarMessageDataComponent, {
      data: {
        message: ' Successfully updated patient.',
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
      this.viewForm.disable();
    }
    else{
      this.isEditable = true;
      this.viewForm.enable();
      this.viewForm.controls['URNumber'].disable();
      this.viewForm.controls['registeredBy'].disable();

    }
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


  editPatient(viewForm) {
    // this.dataService.apiURL = this.url;
    

    //sets date of birth to an ISO string to be parsed correctly
    //this.viewForm.value['dob'] = this.viewForm.value['dob'].toISOString();
    console.log(viewForm)
    this.dataService.editPatient(viewForm.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      })
      .finally(() => {
        this.openSnackBar();
        this.viewForm.disable();
        viewForm.disable();
        this.isEditable = false;
      });

  }



  onEditClick()
  {
    //enables the save button.
    this.enableSave = true;
    this.viewForm.enable();
    //Disables hospitalNumber form field when enabling the others for editing
    this.viewForm.controls['URNumber'].disable();
    this.viewForm.controls['registeredBy'].disable();

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
this.viewForm.value.password = retVal;

}


  updatePassword(viewForm) {
    console.log(viewForm.value);
    this.dataService.editPatientPassword(viewForm.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      })
      .finally(() => {
        this.openPWSnackBar();
        this.passForm.disable();
      });
  }

}
