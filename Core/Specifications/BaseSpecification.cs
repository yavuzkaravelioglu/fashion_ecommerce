using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        
        public BaseSpecification( Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        
        // it is gonna has all includes statement as a list to parse to the ToListAsync() method
        public List<Expression<Func<T, object>>> Includes { get; } =
            new List <Expression <Func <T, object>>>(); // set list by default to an empty list.

        public Expression<Func<T, object>> OrderBy {get; private set;}
        public Expression<Func<T, object>> OrderByDescending {get; private set;}

        public int Take {get; private set;} // Paging için kullanılacak property 1

        public int Skip {get; private set;} // Paging için kullanılacak property 2

        public bool isPagingEnabled {get; private set;} // Paging için kullanılacak property 3

        // we do need to start initially with a list so that we can use it to add things into list

        // create a method that will allow us to adds include statements to our include list
        // which is a list of expressions
        protected void AddInclude(Expression <Func <T, object>> includeExpression) 
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression <Func <T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression <Func <T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            isPagingEnabled = true;
        }

    }
}