using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lenic.DI;
using Sago.Framework.WorkFlow.Entities;
using Sago.Framework.WorkFlow.Hibernate;
using Action = Sago.Framework.WorkFlow.Entities.Action;

namespace Sago.Framework.WorkFlow.Core
{
    public class WorkFlowEngineCore
    {
        public WorkFlowEngineCore(IHibernate hibernateClass)
        {
            this.HibernateClass = hibernateClass;
        }

        private IHibernate HibernateClass { get; set; }

        #region
        public object Start(Process process, User creator, string processInstanceName)
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
                    this.DoReturn(toDoWork, Actioner);
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

            //能够被指向的下个节点
            var canBeActiveNextNodes =
                allOfTheNextProcessNodeInstances.Where(
                    allOfTheNextNode => allOfTheNextNode.Expression.Compile().Invoke(allOfTheNextNode.BizContext));

            //判断当前提交的这个节点的可被激活的下一级节点是否是会签节点，如果是会签节点，就要判断它会签的那些节点是否是审批通过状态
            foreach (var canBeActiveNextNode in canBeActiveNextNodes)
            {
                //判断某一个节点是否是会签节点
                if (this.IsCounterSignature(canBeActiveNextNode))
                {

                }
                else
                {
                    //如果非会签节点，那么就得到Linkage
                    var linkages =
                        canBeActiveNextNode.Linkages.Where(
                            linkage => linkage.Expression.Compile().Invoke(canBeActiveNextNode.BizContext));

                    //生成待办
                    foreach (var linkage in linkages)
                    {
                        this.HibernateClass.BuildToDo(linkage);
                    }

                    //关闭当前待办
                    this.HibernateClass.CloseToDoWork(toDoWork);
                }
            }
        }

        /// <summary>
        /// 处理退回的方法
        /// </summary>
        /// <param name="toDoWork"></param>
        /// <param name="Actioner"></param>
        private void DoReturn(ToDoWork toDoWork, User Actioner)
        {
            var canBeActivePreProcessNodeInstances =
                toDoWork.ProcessNodeInstance.PreProcessNodeInstances.Where(
                    preProcessNodeInstance =>
                        preProcessNodeInstance.Expression.Compile().Invoke(preProcessNodeInstance.BizContext));

            foreach (var canBeActivePreProcessNodeInstance in canBeActivePreProcessNodeInstances)
            {
                var linkages = canBeActivePreProcessNodeInstance.Linkages.Where(linkage => linkage.Expression.Compile().Invoke(canBeActivePreProcessNodeInstance.BizContext));

                foreach (var linkage in linkages)
                {
                    this.HibernateClass.BuildToDo(linkage);
                }

                this.HibernateClass.CloseToDoWork(toDoWork);
            }
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
