import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Component({
  selector: '...',

  template: `
  <mat-icon>{{ data?.icon }}</mat-icon>
    <span>{{ data?.message }}</span>
  `,
  
  // testing
})

export class SnackbarMessageDataComponent {
  
  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) { 

  }
}
