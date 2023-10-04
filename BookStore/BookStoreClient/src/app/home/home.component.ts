import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  response: any;
  pageNumbers: number[] = [];

  constructor(private http: HttpClient){
    this.getAll();
  }

  getAll(pageNumber = 1){
    this.http.get(`https://localhost:7082/api/Books/GetAll/${pageNumber}/10`)
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
