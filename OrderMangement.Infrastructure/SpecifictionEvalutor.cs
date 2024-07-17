using Microsoft.EntityFrameworkCore;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Infrastructure
{
    public static class SpecifictionEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifiction<T> spec)
        {
            var query = inputQuery;

            if (spec.Cretiria is not null)
            {
                query = query.Where(spec.Cretiria);
            }

            query = spec.Includes.Aggregate(query, (cuurentquery, includeexprssion) => query.Include(includeexprssion));
            query = spec.IncludeString.Aggregate(query, (cuurentquery, includeexprssion) => query.Include(includeexprssion));
            return query;
        }
    }
}
