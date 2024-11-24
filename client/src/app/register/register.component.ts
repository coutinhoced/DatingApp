import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  private toaster = inject(ToastrService);
  validationErrors : string[] = [];
  model:any ={}
  cancelRegister = output<boolean>();
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
    console.log(this.model);
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log('cancel');
  }
}
