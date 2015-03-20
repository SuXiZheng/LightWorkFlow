using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
    public class Linkage
    {
        public Guid ID { get; set; }

        public Expression<Func<IDictionary<string, object>, bool>> Expression { get; set; }
    }
}
