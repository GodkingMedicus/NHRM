import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CategoriesComponent } from '../categories/categories.component';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css', '../bootstrap.css']
})
export class AddCategoryComponent implements OnInit {

  constructor(private dataService: DataService, private categoryList: CategoriesComponent) { }

  categoryValue: string;
  Categories: Array<any> = [];

  ngOnInit(): void {
  }


  onSubmit(categoryForm) {
    console.log(categoryForm.value)
    this.categoryValue = categoryForm.value;
    this.dataService.addCategory(categoryForm.value).subscribe({
      next: data => {
        this.Categories = data;
        console.log(data);
        this.categoryList.loadCategories();
        categoryForm.reset();
      },
      error: error => {
        console.error(error);
      }
      });
  }

}
