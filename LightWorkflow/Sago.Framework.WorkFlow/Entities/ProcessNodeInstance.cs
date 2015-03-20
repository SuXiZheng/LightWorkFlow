using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
    public class ProcessNodeInstance : ProcessNode
    {
        public ProcessNode ProcessNode { get; set; }

        public ProcessInstance ProcessInstance { get; set; }

        public NodeStatus NodeStatus { get; set; }

        public IDictionary<string, object> BizContext { get; set; }

        public ICollection<ProcessNodeInstance> NextProcessNodeInstances { get; set; }

        public ICollection<ProcessNodeInstance> PreProcessNodeInstances { get; set; }
    }
}
