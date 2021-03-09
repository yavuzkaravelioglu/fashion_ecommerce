import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/productBrand';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  
  @ViewChild( 'search', {static: true} ) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected = "name";
  sortOptions = [
    {name : 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];
  pageSize = 6; // bu manuel, değişmesi lazım 
  pageIndex = 0;
  totalProductCount: number;
  search: string;
  isCollapsed = true;
  isCollapsedFilter = true;

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts( this.brandIdSelected, this.typeIdSelected, 
        this.sortSelected, this.pageSize, this.pageIndex, this.search ).subscribe( response => {
            this.products = response.data;
            this.pageSize = response.pageSize; // bu
            this.pageIndex = response.pageIndex; // bu 
            this.totalProductCount = response.count; // ve bu nun olayını anlaman lazım
        }, error => {
            console.log(error);
        });
  }

  getBrands() {
    this.shopService.getBrands().subscribe( response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }
  
  getTypes() {
    this.shopService.getTypes().subscribe( response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    
    if (this.brandIdSelected !== brandId){
      this.brandIdSelected = brandId;
      this.pageIndex = 1;
      this.getProducts();
    }
      
  }

  onTypeSelected(typeId: number) {

    if (this.typeIdSelected !== typeId){
      this.typeIdSelected = typeId;
      this.pageIndex = 1;
      this.getProducts();
    }

  }

  onSortSelected (e: Event): void{
    this.sortSelected = (e.target as HTMLSelectElement).value;
    this.getProducts();
  }

  onPageChanged(event: any){
      if (this.pageIndex !== event.page){
        this.pageIndex = event.page;
        this.getProducts();
      }
  }

  onSearch() {
    this.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.search = this.searchTerm.nativeElement.value;
    this.getProducts(); 
  }

  onResetFilter() {
    this.brandIdSelected = 0;
    this.typeIdSelected = 0;
    this.getProducts();
  }

}




