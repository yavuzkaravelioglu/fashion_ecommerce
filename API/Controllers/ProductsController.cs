using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers //represent which folder is located in the project 
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        // Inject ProductRepository into Controller Class using constructor dependency injection
        public ProductsController(IGenericRepository<Product> productsRepo, 
        IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper) // IProductRepository: name of the class we want to inject
        {
            this._productTypeRepo = productTypeRepo;
            this._productBrandRepo = productBrandRepo;
            this._productRepo = productsRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams productParams) // ActionResult: allows return some kind of Http Respond Status
        {
            var spec = new ProductsWithTypesAnsBrandsSpecification(productParams);   
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            
            var products = await _productRepo.ListAsync(spec);
            var totalItems = await _productRepo.CountAsync(countSpec);

            var data = _mapper
                .Map< IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok( new Pagination<ProductToReturnDto>(
                    productParams.PageIndex, productParams.PageSize, totalItems, data
            ));
        }

        [HttpGet("{id}")] //used to distinguish between get methods
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) // int dısında bir parametre yollanırsa 400 error verir
        {
            var spec = new ProductsWithTypesAnsBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);   

            if (product == null) // check if the requested product is exist or not
                return NotFound( new ApiResponse(404) ); 

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var product = await _productBrandRepo.ListAllAsync();
            return Ok(product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var product = await _productTypeRepo.ListAllAsync();
            return Ok(product);
        }

        ////////// DENEME ////////////
        [HttpGet("productimages/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductImages()
        {
            var product = await _productTypeRepo.ListAllAsync();
            return Ok(product);
        }
        //////////////////////

    }
}