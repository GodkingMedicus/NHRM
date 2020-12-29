import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-add-measurement',
  templateUrl: './add-measurement.component.html',
  styleUrls: ['./add-measurement.component.css', '../bootstrap.css']
})
export class AddMeasurementComponent implements OnInit {

  Measurements: Array<any> = [];
  
  Frequency = ['1','2','3'];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.loadMeasurements();
  }

  loadMeasurements() {
    this.dataService.getMeasurement().subscribe({
      next: data => {
        this.Measurements = data;
        console.log(data);
      },
      error: error => {
        console.error(error);
      }
      });
  }
  
  onSubmit(addMeasurementForm) {

  }
}
