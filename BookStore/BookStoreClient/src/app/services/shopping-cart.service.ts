import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';
import { TranslateService } from '@ngx-translate/core';
import { forkJoin } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  shoppingCarts: any[] = [];
  prices: { value: number, currency: string }[] = [];
  count: number = 0;
  total: number = 0;
  constructor(
    private swal: SwalService,
    private translate: TranslateService,
    private http: HttpClient
  ) {
    if (localStorage.getItem("shoppingCarts")) {
      const carts: string | null = localStorage.getItem("shoppingCarts")
      if (carts !== null) {
        this.shoppingCarts = JSON.parse(carts);
        this.count = this.shoppingCarts.length;
      }
    }
  }

  calcTotal() {
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
      console.log(this.prices);

    }

  }

  removeByIndex(index: number) {

    forkJoin({
      doYouWantToDeleted: this.translate.get("remove.doYouWantToDeleted"),
      cancelBtn: this.translate.get("remove.cancelBtn"),
      confirmBtn: this.translate.get("remove.confirmBtn")
    }).subscribe(res => {
      this.swal.callSwal(res.doYouWantToDeleted, res.cancelBtn, res.confirmBtn, () => {
        this.shoppingCarts.splice(index, 1);
        localStorage.setItem("shoppingCarts", JSON.stringify(this.shoppingCarts));
        this.count = this.shoppingCarts.length;
        this.calcTotal();
      });
    })

  }

  payment(currency: string){
    const newList = this.shoppingCarts.filter(p=> p.price.currency === currency);
   this.http.post("https://localhost:7082/api/ShoppingCarts/Payment", {books: newList})
   .subscribe(res=> {
    //burası sonra doldurulacak.
   })
  }
}
