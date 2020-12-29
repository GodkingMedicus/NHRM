import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { CommonModule } from '@angular/common';
import {Router} from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-view-patient-measurements',
  templateUrl: './view-patient-measurements.component.html',
  styleUrls: ['./view-patient-measurements.component.css', '../bootstrap.css']
})
export class ViewPatientMeasurementsComponent implements OnInit {

  //hopefully works with just one
  queryResults: Array<any>;


  patient = this.dataService.passPatient;

  constructor(private dataService: DataService, public router: Router, private _location: Location) { }

  ngOnInit(): void {
    this.dataService.getPatientMeasurementsRecord(this.patient.urnumber).subscribe({
      next: data => {
        console.log(data);
        this.queryResults = data;
      },
      error: error => {
        console.log(error);
      }
    })
  }
  goBack(){
    this._location.back();
  }

}
