import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { AlertifyService, ConfirmResult } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<MemberEditComponent> {
  x = false;
  constructor(private alertify: AlertifyService) { }

  async canDeactivate(component: MemberEditComponent) {

    if (component.editForm.dirty) {

      const confirm = await this.alertify.promisifyConfirm('إنتـبــه', 'تم تعديل البيانات الخاصة بك هل تود الإستمرار بدون حفظ البيانات');
      if (confirm == ConfirmResult.Ok) { this.x = true } else { this.x = false; }
      return this.x;
    }
    return true
  }
}
