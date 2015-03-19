using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sago.Framework.WorkFlow.Entities
{
	public class ToDoWork
	{
		public Action Action { get; set; }

		public ProcessNodeInstance ProcessNodeInstance { get; set; }
	}
}
