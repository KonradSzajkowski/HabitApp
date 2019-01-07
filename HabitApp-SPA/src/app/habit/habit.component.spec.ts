/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HabitComponent } from './habit.component';

describe('HabitComponent', () => {
  let component: HabitComponent;
  let fixture: ComponentFixture<HabitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HabitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HabitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
