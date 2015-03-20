using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sago.Framework.WorkFlow.Core;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var workFlowEngine = new WorkFlowEngine(new MSSQLIHibernate());

            workFlowEngine.Start("Payment", new Guid("d407cc11-fd06-4d8c-8cb8-9eeb2561bd07"), "Sago的申请");


        }
    }
}
