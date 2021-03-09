using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    // We determine type as Product, because this class is gonna be about Product class
    {
        // We use Base Class' constructor  
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) 
            : base( x =>
                ( string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search) ) &&
                ( !productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                ( !productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            ) 
        {}
    }
}