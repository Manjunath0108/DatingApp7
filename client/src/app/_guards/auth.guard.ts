import { Injectable } from '@angular/core';
import {  CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService, private toastr:ToastrService){}
  canActivate(): Observable<boolean >  {
   return this.accountService.currentUser$.pipe(
      map(user =>{
        if(user)
         return true;

        else{
          this.toastr.error('You shall not pass!')
          return false;
        }
      })
    
   )
  }
  
}




/* This is an Angular service that implements the CanActivate guard. 
It is used to restrict access to certain routes in the application.
 The guard checks if a user is authenticated by subscribing to the 
 currentUser$ observable from the AccountService. If the user is authenticated,
  the guard returns true and the user can access the route. If the user is not authenticated, 
  the guard returns false and displays an error message using the toastr service.

*/