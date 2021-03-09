using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IMemberValueResolver<Product, ProductToReturnDto, string, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string sourceMember, string destMember, 
            ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return _config["ApiUrl"] + sourceMember;
            }
            return null;
        }
    }
}