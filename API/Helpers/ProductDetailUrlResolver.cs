/*using System.Collections.Generic;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    
    public class ProductDetailUrlResolver : IValueResolver<Product, ProductToReturnDto, List<string>>
    {
        private readonly IConfiguration _config;
        public ProductDetailUrlResolver(IConfiguration config)
        {
            this._config = config;
        }

       

        public List<string> Resolve(Product source, ProductToReturnDto destination, List<string> destMember, ResolutionContext context)
        {
           
            List<string> UrlsWithServerAdress = new List<string>();

            for(int i=0; i<source.DetailPictureUrl.Count; i++)
            {
                UrlsWithServerAdress.Add(_config["ApiUrl"] + source.DetailPictureUrl[i].PictureUrl);
            }

            return UrlsWithServerAdress;
        
        }
    }
}*/