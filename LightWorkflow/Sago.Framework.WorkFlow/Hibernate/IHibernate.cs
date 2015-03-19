using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sago.Framework.WorkFlow.Entities;

namespace Sago.Framework.WorkFlow.Hibernate
{
	/// <summary>
	/// 数据持久化接口
	/// </summary>
	public interface IHibernate
	{
		#region 关于Process的操作
		Process GetProcessByProcessID(Guid processID);
		#endregion

		#region 关于ProcessNode操作
		ProcessNode GetProcessNodeByProcessNodeID(Guid ProcessNodeID);

		ICollection<ProcessNode> GetProcessNodesByProcessID(Guid processID);

		#endregion

		#region 关于ProcessInstance的操作

		ProcessInstance GetProcessInstanceByProcessInstanceID(Guid ProcessInstanceID);

		object NewProcessInstance(ProcessInstance processInstance);

		#endregion

		#region 关于ProcessNodeInstance的操作

		ProcessNodeInstance GetProcessNodeInstanceByProcessNodeInstanceID(Guid processNodeInstanceID);

		ICollection<ProcessNodeInstance> GetProcessNodeInstancesByProcessInstanceID(Guid processInstanceID);

		object NewProcessNodeInstance(ProcessNodeInstance processNodeInstance);

		object UpdateProcessNodeInstance(ProcessNodeInstance processNodeInstance);

		#endregion

		User GetUserByUserID(Guid userID);
	}
}
