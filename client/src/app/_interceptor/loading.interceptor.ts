import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { BusyService } from '../_services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService:BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    this.busyService.busy();
    
    
    return next.handle(request).pipe(
      delay(1000),
      finalize(()=>{
        this.busyService.idle()
      }
      )
    )
    
  }
}



/*  This code is an Angular service that implements the HttpInterceptor interface. The purpose of this service
 is to add a delay of 1 second to all HTTP requests and show a loading indicator while the request is in progress.
The LoadingInterceptor uses the BusyService to handle the display of a loading indicator. 
The busyService.busy() method is called before the request is sent and the busyService.idle() method is called
 when the request is complete, which will indicate to the user that the request has finished.
 The delay of 1 second is added to the HTTP request using the delay operator from the RxJS library. */