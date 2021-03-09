import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { IBrand } from 'src/app/shared/models/productBrand';
import { IType } from 'src/app/shared/models/productType';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;

  constructor(private shopService: ShopService, 
              private activateRoute: ActivatedRoute, 
              private basketService: BasketService) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, 1);
  }

  loadProduct() {

    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.product = response;
      console.log("loadProduct product", this.product);
      //console.log("loadProduct brandId  ", this.brandID);
    },error => {
      console.log(error);
    });

  }

}
