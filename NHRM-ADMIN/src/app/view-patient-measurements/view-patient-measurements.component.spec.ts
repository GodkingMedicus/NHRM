import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPatientMeasurementsComponent } from './view-patient-measurements.component';

describe('ViewPatientMeasurementsComponent', () => {
  let component: ViewPatientMeasurementsComponent;
  let fixture: ComponentFixture<ViewPatientMeasurementsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewPatientMeasurementsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewPatientMeasurementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
