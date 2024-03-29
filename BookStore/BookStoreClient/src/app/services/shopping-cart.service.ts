import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { PaymentModel } from '../models/payment.model';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthService } from './auth.service';
import { SetShoppingCartsModel } from '../models/set-shopping-carts.model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  shoppingCarts: any[] = [];
  prices: { value: number, currency: string }[] = [];
  count: number = 0;
  total: number = 0;
  isLoading: boolean = false;

  constructor(
    private swal: SwalService,
    private translate: TranslateService,
    private http: HttpClient,
    private auth: AuthService,
    private spinner: NgxSpinnerService
  ) {
    this.checkLocalStoreForShoppingCarts();
  }

  checkLocalStoreForShoppingCarts(){
    const shoppingCartsString = localStorage.getItem("shoppingCarts");
    if (shoppingCartsString) {
      const carts: string | null = localStorage.getItem("shoppingCarts")
      if (carts !== null) {
        this.shoppingCarts = JSON.parse(carts);
      }
    }else{
      this.shoppingCarts = [];
    }

    if(localStorage.getItem("response")){
      this.http.get<SetShoppingCartsModel[]>("https://localhost:7082/api/ShoppingCarts/GetAll/" + this.auth.userId,).subscribe(res=> {
        this.shoppingCarts =  res
        this.calcTotal();
      });
    }

    this.calcTotal();
  }

  calcTotal() {
    this.count = this.shoppingCarts.length;
    this.total = 0;

    const sumMap = new Map<string, number>();

    this.prices = [];
    for (let s of this.shoppingCarts) {
      this.prices.push({ ...s.price });
    }

    for (const item of this.prices) {
      const currentSum = sumMap.get(item.currency) || 0;
      sumMap.set(item.currency, currentSum + item.value);
    }

    this.prices = [];
    for (const [currency, sum] of sumMap) {
      this.prices.push({ value: sum, currency: currency });
    }

  }

  removeByIndex(index: number) {

    forkJoin({
      doYouWantToDeleted: this.translate.get("remove.doYouWantToDeleted"),
      cancelBtn: this.translate.get("remove.cancelBtn"),
      confirmBtn: this.translate.get("remove.confirmBtn")
    }).subscribe(res => {
      this.swal.callSwal(res.doYouWantToDeleted, res.cancelBtn, res.confirmBtn, () => {
        if(localStorage.getItem("response")){
          this.http.get("https://localhost:7082/api/ShoppingCarts/RemoveById/" + this.shoppingCarts[index]?.shoppingCartId).subscribe(res=> {

            this.checkLocalStoreForShoppingCarts();
          });
        }else{
          this.shoppingCarts.splice(index, 1);
          localStorage.setItem("shoppingCarts", JSON.stringify(this.shoppingCarts));
          this.count = this.shoppingCarts.length;
          this.calcTotal();
        }
       
      });
    })

  }

  payment(data:PaymentModel, callBack: (res: any)=> void){
    this.spinner.show();
    this.http.post("https://localhost:7082/api/ShoppingCarts/Payment", data)
    .subscribe({
      next: (res:any)=> { 
        callBack(res);
        this.spinner.hide();
      },
      error: (err: HttpErrorResponse)=> {
        console.log(err);
        this.spinner.hide();
      }
    })
  }
}
