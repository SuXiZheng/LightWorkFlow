using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sago.Framework.WorkFlow.Entities;
using Sago.Framework.WorkFlow.Hibernate;
using Action = Sago.Framework.WorkFlow.Entities.Action;

namespace Sago.Framework.WorkFlow.Core
{
	public class WorkFlowEngine
	{
		public WorkFlowEngine(IHibernate hibernateClass)
		{
			this.HibernateClass = hibernateClass;
		}

		private IHibernate HibernateClass { get; set; }

		#region
		public object Start(Process process, User creator, User Actioner, string processInstanceName)
		{
			this.HibernateClass.NewProcessInstance(this.BuildProcessInstance(process, processInstanceName));

			return new object();
		}

		public object Submit(ToDoWork toDoWork, User Actioner)
		{
			//得到该流程实例下所有的流程节点实例
			var processNodeInstances = this.HibernateClass.GetProcessNodeInstancesByProcessInstanceID(toDoWork.ProcessNodeInstance.ProcessInstance.ID);

			switch (toDoWork.Action)
			{
				case Action.Approve:
					this.DoApproval(toDoWork, Actioner);
					break;
				case Action.Return:
					break;
			}

			return new object();
		}

		#region 各种审批动作处理方法
		/// <summary>
		/// 处理审批通过的方法
		/// </summary>
		/// <param name="toDoWork"></param>
		/// <param name="Actioner"></param>
		private void DoApproval(ToDoWork toDoWork, User Actioner)
		{
			var allOfTheNextProcessNodeInstances = toDoWork.ProcessNodeInstance.NextProcessNodeInstances;



			////判断是否是多人审批
			//if (this.IsMultipleApproval(toDoWork.ProcessNodeInstance.ProcessNode) == false && this.IsCounterSignature(toDoWork.ProcessNodeInstance))
			//{

			//}
		}


		/// <summary>
		/// 判断是否是会签节点
		/// </summary>
		/// <param name="processNodeInstance"></param>
		/// <returns></returns>
		private bool IsCounterSignature(ProcessNodeInstance processNodeInstance)
		{
			return processNodeInstance.IsCounterSignature;
		}

		/// <summary>
		/// 判断是否是多人审批
		/// </summary>
		/// <param name="processNodeInstance"></param>
		/// <returns></returns>
		private bool IsMultipleApproval(ProcessNodeInstance processNodeInstance)
		{
			return processNodeInstance.IsMultipleApproval;
		}

		#endregion
		#endregion

		#region
		private ProcessInstance BuildProcessInstance(Process process, string processInstanceName)
		{
			var processNodeInstances = new List<ProcessNodeInstance>();

			var processInstance = new ProcessInstance()
			{
				ProcessInstanceName = processInstanceName
			};
			return processInstance;
		}

		private ICollection<ProcessNodeInstance> BuildProcessNodeInstance(Process process)
		{
			var processNodes = this.HibernateClass.GetProcessNodesByProcessID(process.ID);

			return processNodes.Select(processNode => new ProcessNodeInstance()
			{
				ID = Guid.NewGuid()
			}).ToList();
		}
		#endregion
	}
}
