import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user =>{
        if(user){
          request=request.clone({
            setHeaders:{
              Authorization: `Bearer ${user.token}`
            }
          })
        }
      }
    })
    
    
    return next.handle(request);
  }
}

/**  The JwtInterceptor takes the token from the current user, if it exists,
 *  and adds it to the Authorization header of the request as a bearer token. 
 * The request is then passed to the next handler in the HTTP chain, which is responsible
 *  for actually sending the request. */