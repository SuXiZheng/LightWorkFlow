using Sago.Framework.Workflow.Core.DefaultFeatures.Entities;

namespace Sago.Framework.Workflow.Core.Interfaces
{
	public interface IDataTransformer
	{
		ProcessNodeInstance TransformToProcessNodeInstance(object dataEntity);

		object TransformToProcessNodeInstanceObject();
	}
}