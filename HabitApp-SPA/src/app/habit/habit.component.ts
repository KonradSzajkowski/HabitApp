import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-habit',
  templateUrl: './habit.component.html',
  styleUrls: ['./habit.component.css']
})
export class HabitComponent implements OnInit {
  @Output() setHabit = new EventEmitter();
  habitdata: any;
  constructor(private http: HttpClient, private authService: AuthService) { }

  ngOnInit() {
  }
  cancelHabit() {
    this.setHabit.emit(false);
    console.log('cancelled');
  }

  activateHabit() {
    this.setHabit.emit(true);
    console.log('cancelled');
  }
  getHabits() {
    const token = 'Bearer ' + this.authService.getToken();
    console.log(token);
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
          'Authorization': token
        })
      };
      this.http.post('http://localhost:5000/api/habits/gethabitnames' ,  httpOptions).subscribe(response => {
        this.habitdata = response;
      }, error => {
        console.log(error);
      });
    }
}
