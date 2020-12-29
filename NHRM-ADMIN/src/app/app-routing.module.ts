import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddPatientComponent } from './add-patient/add-patient.component';
import { AlertsComponent } from './alerts/alerts.component';
import { CategoriesComponent } from './categories/categories.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LibraryComponent } from './library/library.component';
import { LoginComponent } from './login/login.component';
import { PatientFilesComponent } from './patient-files/patient-files.component';
import { ResourcesComponent } from './resources/resources.component';
import { SearchPatientComponent } from './search-patient/search-patient.component';
import { ViewPatientComponent } from './view-patient/view-patient.component';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { AddStaffComponent } from './add-staff/add-staff.component';
import { SearchStaffComponent } from './search-staff/search-staff.component';
import { StaffFilesComponent } from './staff-files/staff-files.component';
import { 
  RoleGuardService as RoleGuard 
} from './services/role-guard.service';
import { ViewPatientMeasurementsComponent } from './view-patient-measurements/view-patient-measurements.component'

const routes: Routes = [

{
  path: '',
  //component: DashboardComponent,
  component: PatientFilesComponent,
},
{
  path: 'dashboard',
  component: DashboardComponent,
},
{
  path: 'library',
  component: LibraryComponent,
},
{
  path: 'files',
  component: PatientFilesComponent,
},
{
  path: 'resources',
  component: ResourcesComponent,
},
{
  path: 'addPatient',
  component: AddPatientComponent,
},
{
  path: 'searchPatient',
  component: SearchPatientComponent,
},
{
  path: 'viewPatient',
  component: ViewPatientComponent,
},
{
  path: 'viewPatientMeasurements',
  component: ViewPatientMeasurementsComponent,
},
{
  path: 'categories',
  component: CategoriesComponent,
},
{
  path: 'accountDetails',
  component: AccountDetailsComponent,
},
{
  path: 'addStaff',
  component: AddStaffComponent,
  canActivate: [RoleGuard], 
    data: { 
      expectedRole: '1'
    } 
},
{
  path: 'searchStaff',
  component: SearchStaffComponent,
  canActivate: [RoleGuard], 
    data: { 
      expectedRole: '1'
    } 
},
{
  path: 'staffFiles',
  component: StaffFilesComponent,
  canActivate: [RoleGuard], 
    data: { 
      expectedRole: '1'
    } 
}



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
