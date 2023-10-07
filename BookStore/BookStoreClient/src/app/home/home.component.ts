import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RequestModel } from '../models/request.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  response: any;
  pageNumbers: number[] = [];
  request: RequestModel = new RequestModel();

  constructor(private http: HttpClient){
    this.getAll();
  }

  getAll(pageNumber = 1){
    this.request.pageNumber = pageNumber;
    this.http
    .post(`https://localhost:7082/api/Books/GetAll/`, this.request)
    .subscribe(res=> {
      this.response = res;
      this.setPageNumber();
    })
  }

  setPageNumber(){
    this.pageNumbers = [];
    for (let i = 0; i < this.response.totalPageCount; i++) {
      this.pageNumbers.push(i + 1)      
    }
  }
}
