import { Component, inject, input, OnInit, output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";

@Component({
    selector: 'app-register',
    imports: [ReactiveFormsModule, TextInputComponent],
    templateUrl: './register.component.html',
    styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  
  private accountService = inject(AccountService);
  private toaster = inject(ToastrService);
  validationErrors : string[] = [];
  cancelRegister = output<boolean>();
  model:any ={}

  //Form elements //Tracks and validity the form control instances
  registerForm : FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = new FormGroup({
        username : new FormControl('', Validators.required),
        password : new FormControl('', [Validators.required,
                                        Validators.minLength(4),
                                        Validators.maxLength(8)
                                       ]), 
        confirmPassword : new FormControl('', [Validators.required, this.matchValues('password')]),
    })
    
    this.registerForm.controls['password'].valueChanges.subscribe({
      next:()=> this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string):ValidatorFn{
    return (control: AbstractControl)=>{
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true} 
    }
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error =>{        
        //this.toaster.error(error);
        this.validationErrors = error;
      }      
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
