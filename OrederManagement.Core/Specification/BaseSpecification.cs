using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Specification
{
    public class BaseSpecification<T> : ISpecifiction<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Cretiria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeString { get; set; } = new List<string>();

        public BaseSpecification(Expression<Func<T, bool>> expression)
        {
            Cretiria = expression;
        }
        public BaseSpecification()
        {
            
        }
    }
}
