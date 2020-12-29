import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PatientFilesComponent } from './patient-files/patient-files.component';
import { LibraryComponent } from './library/library.component';
import { ResourcesComponent } from './resources/resources.component';
import { AlertsComponent } from './alerts/alerts.component';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AddPatientComponent } from './add-patient/add-patient.component';
import { SearchPatientComponent } from './search-patient/search-patient.component';
import { NoopAnimationsModule, BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SnackbarMessageDataComponent } from './snackbar-message-data/snackbar-message-data.component';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ViewPatientComponent } from './view-patient/view-patient.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { CategoriesComponent } from './categories/categories.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter, MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import {CdkTableModule} from '@angular/cdk/table';  
import {CdkTreeModule} from '@angular/cdk/tree';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { MatTabsModule } from '@angular/material/tabs';
import { DataService } from './services/data.service';
import { SearchStaffComponent } from './search-staff/search-staff.component';
import { AddStaffComponent } from './add-staff/add-staff.component';
import { AddMeasurementComponent } from './add-measurement/add-measurement.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { StaffFilesComponent } from './staff-files/staff-files.component';
import { ViewPatientMeasurementsComponent } from './view-patient-measurements/view-patient-measurements.component';

export function tokenGetter() {
  return JSON.parse(localStorage.getItem('Authorization'));
}

@NgModule({
  declarations: [
    AppComponent,
    PatientFilesComponent,
    LibraryComponent,
    ResourcesComponent,
    AlertsComponent,
    LoginComponent,
    DashboardComponent,
    AddPatientComponent,
    SearchPatientComponent,
    SnackbarMessageDataComponent,
    ViewPatientComponent,
    CategoriesComponent,
    AddCategoryComponent,
    AccountDetailsComponent,
    SearchStaffComponent,
    AddStaffComponent,
    AddMeasurementComponent,
    StaffFilesComponent,
    ViewPatientMeasurementsComponent
  ],
  imports: [
    RouterModule,
    AppRoutingModule,
    BrowserModule,
    NoopAnimationsModule,
    MatSnackBarModule,
    MatIconModule,
    ReactiveFormsModule,
    MatInputModule,
    FormsModule,
    HttpClientModule,
    MatMenuModule,
    MatButtonModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatNativeDateModule,
    MatTabsModule,
    MatDatepickerModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5001', 'nhrmadminapi-env.eba-sezdcf9x.us-east-1.elasticbeanstalk.com']
      }
    }),
    BrowserAnimationsModule,
    NgxSpinnerModule
  ],
  providers: [{provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: {useUtc: true}}],
  bootstrap: [AppComponent]
})
export class AppModule { }
