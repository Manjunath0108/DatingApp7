import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate } from '@angular/router';

import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent> {
  canDeactivate(
    component: MemberEditComponent):
     boolean  {
    if(component.editForm?.dirty){
      return confirm('Are u sure u want to continue?');
    }
    return true;
  }
  
}



/* This is a guard implementation in Angular that prevents a user from navigating away 
from a form in MemberEditComponent if the form is dirty (unsaved changes).
 The guard implements the CanDeactivate interface and returns a boolean value 
 indicating whether the navigation should proceed or not. If the form is dirty,
  a confirmation message will be displayed to the user and the navigation will only
   proceed if the user confirms it.*/