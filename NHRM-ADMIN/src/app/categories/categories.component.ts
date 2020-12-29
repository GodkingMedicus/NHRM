import { Component, OnInit } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MatMenuModule} from '@angular/material/menu';
import { DataService } from '../services/data.service';


@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss', '../bootstrap.css']
})
export class CategoriesComponent implements OnInit {

  Categories: Array<any> = [];

  constructor(private dataService: DataService) { 

  }

  

  ngOnInit(): void {
    this.loadCategories();
  }
  
  loadCategories() {
    this.dataService.getCategory().subscribe({
      next: data => {
        this.Categories = data;
        console.log(data);
      },
      error: error => {
        console.error(error);
      }
      });
  }
}
