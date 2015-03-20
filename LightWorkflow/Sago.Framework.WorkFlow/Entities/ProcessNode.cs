using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
    public class ProcessNode
    {
        public Guid ID { get; set; }

        public bool IsCounterSignature { get; set; }

        public bool IsMultipleApproval { get; set; }

        public ICollection<Linkage> Linkages { get; set; }

        public Expression<Func<IDictionary<string, object>, bool>> Expression { get; set; }
    }
}
