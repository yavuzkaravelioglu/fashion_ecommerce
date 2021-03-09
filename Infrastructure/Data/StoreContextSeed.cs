using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory) // static keyword : allows us to use a method inside a class wthout actually needing to create a new instance of class before we can use it's methods
        {
            try
            {
                if (!context.ProductBrand.Any()) // check we have got ProductBrands (! ProductBrands !)
                {
                    var brandsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json"); // file path, .. means go for upper folder first because we in API right now

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); // serialize the json file into productBrand object

                    foreach (var item in brands) // iterate over for each brands Data in brands List
                    {
                        context.ProductBrand.Add(item);
                    }

                    await context.SaveChangesAsync(); // save all new productBrands into our DB.

                }

                if (!context.ProductType.Any()) // check we have got ProductBrands (! ProductBrands !)
                {
                    var typesData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json"); // file path, .. means go for upper folder first because we in API right now

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData); // serialize the json file into productBrand object

                    foreach (var item in types) // iterate over for each brands Data in brands List
                    {
                        context.ProductType.Add(item);
                    }

                    await context.SaveChangesAsync(); // save all new productBrands into our DB.

                }

                if (!context.Products.Any()) // check we have got ProductBrands (! ProductBrands !)
                {
                    var productsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json"); // file path, .. means go for upper folder first because we in API right now

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData); // serialize the json file into productBrand object

                    foreach (var item in products) // iterate over for each brands Data in brands List
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync(); // save all new productBrands into our DB.

                }

/*
                if (!context.ProductDetailPictures.Any()) // check we have got ProductBrands (! ProductBrands !)
                {
                    var productPictures = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/productDetailPictures.json"); // file path, .. means go for upper folder first because we in API right now

                    var productDetailPictures = JsonSerializer.Deserialize<List<ProductDetailPictures>>(productPictures); // serialize the json file into productBrand object

                    foreach (var item in productDetailPictures) // iterate over for each brands Data in brands List
                    {
                        context.ProductDetailPictures.Add(item);
                    }

                    await context.SaveChangesAsync(); // save all new productBrands into our DB.

                }*/
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    
    }
}