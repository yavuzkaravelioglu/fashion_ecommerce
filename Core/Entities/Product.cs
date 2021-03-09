using System.Collections.Generic;

namespace Core.Entities
{
    public class Product : BaseEntity // main object product oldugu için bununla başlamak işleri kolaylastırıyor, derive baseentitity class
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string PictureUrl1 { get; set; }
        public string PictureUrl2 { get; set; }
        public string PictureUrl3 { get; set; }
        public string PictureUrl4 { get; set; }
        public string PictureUrl5{ get; set; }
        public string PictureUrl6 { get; set; }
        public ProductType ProductType { get; set; } // bunlar related entities 
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; } // bunlar related entities 
        public int ProductBrandId { get; set; }

        public IList<ProductDetailPictures> PicturesUrl { get; set; }
    

        /*
        public List<ProductDetailPictures> DetailPictureUrl { get; set; }
        **/
        
    }
}