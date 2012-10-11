using System.Collections.Generic;

namespace FlyingTuna.Additions.VarRefs
{
    public interface IVariableContainer
    {
        void Overwrite(VariableReference varRef);
        IEnumerable<VariableReference> GetVariables();
    }
}