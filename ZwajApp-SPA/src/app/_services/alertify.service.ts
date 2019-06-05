import { Injectable } from '@angular/core';

declare let alertify : any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

confirm(message: string, okCallback: () => any) {
  alertify.confirm(message, function (e) {
    if (e) { okCallback() } else { }
  });
}
// confirm(message: string, okCallback: () => any, cancelCallback: () => any) {
//   alertify.confirm(message, function (e) {
//     if (e) { okCallback() } else { }
//   }, function (e) {
//     if (e) { cancelCallback() } else { }
//   });
// }
success(message: string) {
  alertify.success(message);
}

error(message: string) {
  alertify.error(message);
}
warning(message: string) {
  alertify.warning(message);
}
message(message: string) {
  alertify.message(message);
}

promisifyConfirm(title: string, message: string, options = {}): Promise<ConfirmResult> {

  return new Promise<ConfirmResult>((resolve) => {
    alertify.confirm(
      title,
      message,
      () => resolve(ConfirmResult.Ok),
      () => resolve(ConfirmResult.Cancel)).set(Object.assign({}, {
        closableByDimmer: false,
        defaultFocus: 'cancel',
        frameless: false,
        closable: false
      }, options));
  });
}


// defaults
defaults = alertify.defaults = {
  // dialogs defaults
  autoReset: true,
  basic: false,
  closable: true,
  closableByDimmer: true,
  frameless: false,
  maintainFocus: true, // <== global default not per instance, applies to all dialogs
  maximizable: true,
  modal: true,
  movable: true,
  moveBounded: false,
  overflow: true,
  padding: true,
  pinnable: true,
  pinned: true,
  preventBodyShift: false, // <== global default not per instance, applies to all dialogs
  resizable: true,
  startMaximized: false,
  transition: 'pulse',

  // notifier defaults
  notifier: { 
    delay: 3,
    position: 'top-right',
    closeButton: false
  },

  // language resources 
  glossary: {
    title: 'موقع زواج',
    ok: 'نعم',
    cancel: 'لا',

  },

  // theme settings
  theme: {
    // class name attached to prompt dialog input textbox.
    input: 'ajs-input',
    // class name attached to ok button
    ok: 'ajs-ok',
    // class name attached to cancel button 
    cancel: 'ajs-cancel'
  }
}  


}



//////////////////////////
export enum ConfirmResult {
  Ok = 1,
  Cancel
  }


