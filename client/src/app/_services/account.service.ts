import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseurl = "https://localhost:5001/api/";
  currentUser = signal <User | null> (null) // User|| null le type de signal, la deuxième nul est la valeur initial

  login(model: any)
  {
    return this.http.post<User>(this.baseurl + 'account/login' , model).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any)
  {
    return this.http.post<User>(this.baseurl + 'account/register' , model).pipe(
      map(user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }

  logout() {
    localStorage.removeItem('user'); // vider le local storage du token
    this.currentUser.set(null); // doner une valeur null au signal qui contient l'objet user 
  }
}
