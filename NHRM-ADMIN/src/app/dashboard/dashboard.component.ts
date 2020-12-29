import { Component, OnInit } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css', '../bootstrap.css']
})
export class DashboardComponent implements OnInit {
  @ViewChild('alert', { static: true }) alert: ElementRef;
  constructor() { }

  ngOnInit(): void {
  }

  items = ['Patient 1 has reported a score of 2 on his recent ECOG status', 'Patient 2 has reported a low QoL score ', 'Patient 3 has recorded a low VAS score for 3 days in a row'];

  closeAlert() {
    this.alert.nativeElement.classList.remove('show');
  }
  

}
