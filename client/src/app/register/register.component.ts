import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService)
  model : any = {}
//  usersFromHomeComponent = input.required<any>(); // a signal tthat we can make it required so we need to pass it from parent to child
 cancelRegister = output<boolean>(); 
 
 register(){
    this.accountService.register(this.model).subscribe({
      next : response => {
        console.log(response);
        this.cancel();
      },

      complete() {
        console.log("user registred")
      }
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
