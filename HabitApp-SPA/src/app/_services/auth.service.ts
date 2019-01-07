import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
constructor(private http: HttpClient) { }
login(model: any) {
  return this.http.post(this.baseUrl + 'login', model )
  .pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
      }
    })
  );
}
register(model: any) {
  return this.http.post(this.baseUrl + 'register', model );
}

loggedIn() {
  const token = localStorage.getItem('token');
  return !!token;
}
  public getToken(): string {
    return localStorage.getItem('token');
  }

  addHabit(model: any) {
    const token = 'Bearer ' + this.getToken();
    console.log(token);
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json',
          'Authorization': token
        })
      };
  return this.http.post('http://localhost:5000/api/habits/createhabit', model , httpOptions);
}

}


