using System.Collections.Generic;
using System.Linq;

namespace Reqnroll.Assist.Dynamic;

[Binding]
public class DynamicStepArgumentTransformations
{

    [StepArgumentTransformation]
    public IEnumerable<object> TransformToEnumerable(DataTable table)
    {
        return table.CreateDynamicSet();
    }

    [StepArgumentTransformation]
    public IList<object> TransformToList(DataTable table)
    {
        return table.CreateDynamicSet().ToList<object>();
    }

    [StepArgumentTransformation]
    public dynamic TransformToDynamicInstance(DataTable table)
    {
        return table.CreateDynamicInstance();
    }
}
