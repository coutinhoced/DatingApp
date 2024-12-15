import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component, currentRoute, currentState, nextState) => {
  if(component.editform?.dirty){
    return confirm('Are you sure you want to continue, any unsaved changes will be lost');
    //if false, we stay on the component; otherwise we allow to move
  }
  return true;
};
