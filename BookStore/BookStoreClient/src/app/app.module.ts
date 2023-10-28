import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/layouts/navbar/navbar.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FormsModule } from '@angular/forms';
import { CategoryPipe } from './pipes/category.pipe';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { IconControlDirective } from './directives/icon-control.directive';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { TrCurrencyPipe } from 'tr-currency';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ZoomDirective } from './directives/zoom.directive';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
@NgModule({
  declarations: [
    AppComponent,
    LayoutsComponent,
    HomeComponent,
    NavbarComponent,
    CategoryPipe,
    IconControlDirective,
    ShoppingCartComponent,
    ZoomDirective,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    TrCurrencyPipe,
    SweetAlert2Module,
    InfiniteScrollModule,
    NgxSpinnerModule,
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
