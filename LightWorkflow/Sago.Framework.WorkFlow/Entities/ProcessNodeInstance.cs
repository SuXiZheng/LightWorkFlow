using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
	public class ProcessNodeInstance
	{
		public Guid ID { get; set; }

		public bool IsCounterSignature { get; set; }

		public bool IsMultipleApproval { get; set; }

		public ProcessNode ProcessNode { get; set; }

		public ProcessInstance ProcessInstance { get; set; }

		public NodeStatus NodeStatus { get; set; }

		public ICollection<ProcessNodeInstance> NextProcessNodeInstances { get; set; }

		//public ICollection<> Type { get; set; }
	}
}
