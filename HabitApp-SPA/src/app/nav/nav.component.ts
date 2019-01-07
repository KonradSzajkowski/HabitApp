import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  @Output() setAddHabitMode = new EventEmitter();
  @Output() setHabit = new EventEmitter();
  model: any = {};
  habitnames: any;
  constructor(private authService: AuthService, private http: HttpClient) { }

  ngOnInit() {
    this.getHabits();
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('logged in succesfuly');
    }, error => {
      console.log('Failed login');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
  }

  cancelAddHabitMode() {
    this.setAddHabitMode.emit(false);
    console.log('cancelled');
  }

  activateAddHabitMode() {
    this.setAddHabitMode.emit(true);
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
      this.http.get('http://localhost:5000/api/habits/gethabitnames' , httpOptions).subscribe(response => {
        this.habitnames = response;
      }, error => {
        console.log(error);
      });
    }

    cancelHabit() {
      this.setHabit.emit(false);
      console.log('cancelled');
    }
    activateHabit() {
      this.setHabit.emit(true);
      console.log('cancelled');
    }
  }
