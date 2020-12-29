import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { FormControl, NgForm } from '@angular/forms';
import { SnackbarMessageDataComponent } from '../snackbar-message-data/snackbar-message-data.component';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-add-staff',
  templateUrl: './add-staff.component.html',
  styleUrls: ['./add-staff.component.css', '../bootstrap.css']
})
export class AddStaffComponent implements OnInit {

  failed: boolean;
  err: string;


  //presets the icon to an arrow up https://material.io/resources/icons/
  icon = 'keyboard_arrow_up';
  //defines isDisplayed variable used for showing/hiding parts of the form
  isDisplayed:boolean = true;

  titles = ['Mr.', 'Mrs.','Miss.', 'Dr.'];
  roles = ['Admin', 'Clinician'];

  date = new FormControl(new Date());
  

  constructor(private dataService: DataService, private snackBarTest: MatSnackBar) { }


  ngOnInit(): void {
    
    console.log(this.date);
  }

  openSnackBar(){
    this.snackBarTest.openFromComponent(SnackbarMessageDataComponent, {
      data: {
        message: ' Successfully added staff.',
        icon: 'done',
      },
      duration: 2000,
    });
  }



  showMe()
  {
    if(this.isDisplayed)
    {
      //Hides form and sets the icon to arrow down
      this.isDisplayed = false;
      this.icon = 'keyboard_arrow_down';
    }
    else
    {
      //Shows the form and sets the icon to arrow up
      this.isDisplayed = true;
      this.icon = 'keyboard_arrow_up';
    }
  }

  //placeholder for api
  addStaff(form) {
    console.log(form.value)
    this.dataService.addStaff(form.value).then()
      .catch((err) => {
        this.failed = true;
        this.err = err['status'];
      })
      .finally(() => {
        this.openSnackBar();
       // this.dataService.loading.next(false);
       // this.router.navigate(['/dashboard']);
       form.reset();
      });
    
  }



  dateTest(){
    console.log(this.date.value);
  }


}
