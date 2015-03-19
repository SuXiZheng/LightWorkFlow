using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
	public class ProcessInstance
	{
		public Guid ID { get; set; }

		public string ProcessInstanceName { get; set; }

		public ICollection<ProcessNodeInstance> ProcessNodeInstances { get; set; }
	}
}
