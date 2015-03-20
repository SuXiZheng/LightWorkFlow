using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sago.Framework.WorkFlow.Entities;
using Sago.Framework.WorkFlow.Hibernate;

namespace Sago.Framework.WorkFlow.Core
{
    public class WorkFlowEngine
    {
        public WorkFlowEngine(IHibernate hibernateClass)
        {
            this.WorkFlowEngineCore = new WorkFlowEngineCore(this.HibernateClass = hibernateClass);
        }

        private IHibernate HibernateClass { get; set; }

        private WorkFlowEngineCore WorkFlowEngineCore { get; set; }

        public object Start(string processCode, Guid creatorID, string processInstanceName)
        {
            var process = this.HibernateClass.GetProcessByProcessCode(processCode);

            var creator = this.HibernateClass.GetUserByUserID(creatorID);

            return this.WorkFlowEngineCore.Start(process, creator, processInstanceName);
        }
    }
}
