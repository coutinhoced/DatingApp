import { Injectable } from '@angular/core';
import { SpinnerComponent } from '../spinner/spinner.component';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  private spinner : SpinnerComponent | null = null;

  constructor() { }

  registerSpinner(spinner : SpinnerComponent): void{   
    this.spinner = spinner;
  }

  show() : void{
     if(this.spinner){
      this.spinner.show();
     }
  }

  hide() : void{
    if(this.spinner){
      this.spinner.hide();
     }
  }
}
