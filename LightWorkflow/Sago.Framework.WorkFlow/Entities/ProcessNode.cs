using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
	public class ProcessNode
	{
		public bool IsCounterSignature { get; set; }

		public bool IsMultipleApproval { get; set; }

		public ICollection<Linkage> Linkages { get; set; }
	}
}
