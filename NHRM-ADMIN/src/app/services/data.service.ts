import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Staff } from '../models/staff';
import { Patient } from '../models/patient';
import { updatePatient } from '../models/updatePatient';
import { searchPatient } from '../models/searchPatient';
import { Category } from '../models/category';
import { searchStaff } from '../models/searchStaff';
import { updateStaff } from '../models/updateStaff';
import { Measurement } from '../models/measurement';
import { PatientMeasurementsRecord} from '../models/patientMeasurementRecord';
import { NgxSpinnerService } from 'ngx-spinner';
import { JwtHelperService } from '@auth0/angular-jwt';



const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json", "Authorization": "c31z" })
};


@Injectable({
  providedIn: 'root'
})
export class DataService {

  patient: BehaviorSubject<Patient>;
  //used to pass patient between search and view
  passPatient: Patient;
  passStaff: Staff;
  staff: BehaviorSubject<Staff>;
  //apiURL = "https://localhost:5001/api";
  apiURL = "http://nhrmadminapi-env.eba-sezdcf9x.us-east-1.elasticbeanstalk.com/api";
  staffID: string;
  loading: BehaviorSubject<boolean>;

   constructor(private _http: HttpClient, private jwtHelper: JwtHelperService, private spinner: NgxSpinnerService) {
    this.loading = new BehaviorSubject(false);
  }

  


  addPatient(patient: Patient){
    var tokenS = this.jwtHelper.decodeToken(localStorage.getItem('Authorization'));
    patient.registeredBy = parseInt(tokenS.StaffID);
    patient.Active = true;
    console.log(patient);
    return new Promise((resolve, reject) => {
      this._http.post(this.apiURL + "/patients", patient).subscribe(
        () => {
          console.log("Patient added")
          resolve();
        },
        err => {
          console.error("Error adding patient")
          reject(err);
        });
    })

  }

  // Placeholder for updating a patients details via the view patient page.
  editPatient(patient: updatePatient){
    console.log(patient);
    patient.livesAlone = true;
    // patient.registeredBy = "STAFFID";
    patient.Active = true;
    return new Promise((resolve, reject) => {
      this._http.put(this.apiURL + "/patients/" + patient.URNumber, patient).subscribe(
        () => {
          console.log("Patient updated")
          resolve();
        },
        err => {
          console.error("Error updating patient")
          reject(err);
        });
    });
  }

  //Returns a List of Patients as a search result
  searchPatient(sP: searchPatient): Observable<Patient[]>{
    console.log(sP);
    return this._http.post<Patient[]>(this.apiURL + "/patients/search", sP);
  }

  //Staff
  addStaff(staff: Staff){
    console.log(staff);
    return new Promise((resolve, reject) => {
      this._http.post(this.apiURL + "/staffs", staff).subscribe(
        () => {
          console.log("Staff added");
          resolve();
        },
        err => {
          console.error("Error adding staff");
          reject(err);
        });
    })
  }

  editStaff(staff: Staff){
    console.log(staff);
    return new Promise((resolve, reject) => {
      this._http.put(this.apiURL + "/staffs/" + staff.email, staff).subscribe(
        () => {
          console.log("Staff updated")
          resolve();
        },
        err => {
          console.error("Error updating Staff")
          reject(err);
        });
    });
  }

  editPatientPassword(patient: Patient){
    console.log(patient);
    return new Promise((resolve, reject) => {
      this._http.put(this.apiURL + "/patients/password/", patient).subscribe(
        () => {
          console.log("Password updated")
          resolve();
        },
        err => {
          console.error("Error updating password")
          reject(err);
        });
    });
  }

  editStaffPassword(staff: Staff){
    console.log(staff);
    return new Promise((resolve, reject) => {
      this._http.put(this.apiURL + "/staffs/password/", staff).subscribe(
        () => {
          console.log("Password updated")
          resolve();
        },
        err => {
          console.error("Error updating password")
          reject(err);
        });
    });
  }

  searchStaff(sStaff: searchStaff): Observable<Staff[]>{
    console.log(sStaff);
    return this._http.post<Staff[]>(this.apiURL + "/staffs/search", sStaff);
  }


  getCategory(): Observable<Category[]> {
    return this._http.get<Category[]>(this.apiURL + "/TemplateCategories")
  }

  addCategory(cat: Category): Observable<Patient[]>{
    console.log(cat);
    return this._http.post<Patient[]>(this.apiURL + "/templatecategories", cat);
  }

  getMeasurement(): Observable<Measurement[]>{
    return this._http.get<Measurement[]>(this.apiURL + "/measurements")

  }

  getPatientMeasurementsRecord(ur): Observable<PatientMeasurementsRecord[]>{
    return this._http.get<PatientMeasurementsRecord[]>(this.apiURL + "/patientmeasurementsrecord" + "/" + ur);
  }

}