namespace Core.Entities
{
    public class ProductDetailPictures : BaseEntity
    {
        public string DetailClassPicturesUrl { get; set; } 

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}