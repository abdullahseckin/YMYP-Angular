import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userNameOrEmail: string = "";
  password:string = "";

  signIn(form:NgForm){
    if(form.valid){
      console.log(form);
      console.log(form.value);
    }    
  }

  checkValidation(el:HTMLInputElement){
    if(!el.validity.valid){
      el.classList.add("is-invalid"); //form-control is-invalid is-valid
      el.classList.remove("is-valid");//form-control is-inval
    }else{
      el.classList.remove("is-invalid");//form-control is-valid is-invalid
      el.classList.add("is-valid");//form-control is-valid
    }
  }
}
