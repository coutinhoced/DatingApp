import { AfterViewInit, Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { SpinnerService } from '../_services/spinner.service';

@Component({
  selector: 'app-spinner',
  imports: [],
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.css'
})
export class SpinnerComponent implements AfterViewInit {

  @ViewChild('overlayspinner') overlayspinner : ElementRef | undefined;
  constructor(private renderer : Renderer2, 
              private spinnerService: SpinnerService){  }

  ngAfterViewInit(): void {    
    this.spinnerService.registerSpinner(this);
  }  
  
  show(){
    this.renderer.setStyle(this.overlayspinner?.nativeElement, 'visibility', 'visible');
  }

  hide(){
    this.renderer.setStyle(this.overlayspinner?.nativeElement, 'visibility', 'hidden');
  }

}
