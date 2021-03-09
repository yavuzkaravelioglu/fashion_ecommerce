using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity 
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
            ISpecification<TEntity> spec ) 
        {
            var query = inputQuery; // to store inputQuery in

            if (spec.Criteria != null) // evaluate what is inside in spec
            {
                query = query.Where(spec.Criteria); // criteria might be : p => p.ProductTypeId == id
            }

            if (spec.OrderBy != null) // check whether we have OrderBy in our spec
            {
                query = query.OrderBy(spec.OrderBy); // if it does not have, do that
            }

            if (spec.OrderByDescending != null) // check whether we have OrderByDescending in our spec
            {
                query = query.OrderByDescending(spec.OrderByDescending); // if it does not have, do that
            }

            if (spec.isPagingEnabled) // check to see, whether we want paging or not
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate( query, (current, include) => 
                current.Include(include) ); // evaluate includes
        
            return query;
        }   
        
    }
}