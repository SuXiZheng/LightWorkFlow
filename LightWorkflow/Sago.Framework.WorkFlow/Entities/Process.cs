using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
	public class Process
	{
		public Guid ID { get; set; }

		public string ProcessName { get; set; }

		public string Description { get; set; }

		public IDictionary<string, object> BizContext { get; set; }

		public ICollection<ProcessNode> ProcessNodes { get; set; }
	}
}
