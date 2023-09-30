import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  isShowPassword: boolean = false;
  isPasswordFocus: boolean = false;
  
  signIn(form:NgForm){
    if(form.valid){
      console.log(form);
      console.log(form.value);
    }    
  }  

  showOrHidePassword(password: HTMLInputElement){
    if(this.isShowPassword){
      this.isShowPassword = false;
      password.type = "password"
    }else{
      this.isShowPassword = true;
      password.type = "text"
    }
  }
}
