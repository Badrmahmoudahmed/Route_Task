using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrederManagement.Core.Specification
{
    public interface ISpecifiction<T>  where T : BaseEntity
    {
        Expression<Func<T,bool>> Cretiria {  get; set; }
        List<Expression<Func<T,object>>> Includes { get; set; }
        List<string> IncludeString { get; set; }


    }
}
