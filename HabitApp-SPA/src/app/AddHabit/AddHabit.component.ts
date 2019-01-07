import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-addhabit',
  templateUrl: './AddHabit.component.html',
  styleUrls: ['./AddHabit.component.css']
})
export class AddHabitComponent implements OnInit {
  @Output() setAddHabitMode = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  cancelAddHabitMode() {
    this.setAddHabitMode.emit(false);
    console.log('cancelled');
  }

  activateAddHabitMode() {
    this.setAddHabitMode.emit(true);
    console.log('cancelled');
  }

  addHabit() {
    this.authService.addHabit(this.model).subscribe(() => {
      console.log('registration successful');
    }, error => {
      console.log(error);
    });
  }
}
