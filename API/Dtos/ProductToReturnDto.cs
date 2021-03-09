using System.Collections.Generic;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string PictureUrl1 { get; set; }
        public string PictureUrl2 { get; set; }
        public string PictureUrl3 { get; set; }
        public string PictureUrl4 { get; set; }
        public string PictureUrl5 { get; set; }
        public string PictureUrl6 { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }

        /*/
        public List<string> DetailPictureUrl { get; set; }
        */
        
    }
}