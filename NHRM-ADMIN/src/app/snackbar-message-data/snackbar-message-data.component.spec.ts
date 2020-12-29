import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SnackbarMessageDataComponent } from './snackbar-message-data.component';

describe('SnackbarMessageDataComponent', () => {
  let component: SnackbarMessageDataComponent;
  let fixture: ComponentFixture<SnackbarMessageDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SnackbarMessageDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SnackbarMessageDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
