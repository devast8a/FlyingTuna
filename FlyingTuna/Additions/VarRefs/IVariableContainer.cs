using System.Collections.Generic;

namespace FlyingTuna.Additions.VarRefs
{
    public interface IVariableContainer
    {
        //TODO: abstract members from VariableContainer
        void Overwrite(VariableReference varRef);
        IEnumerable<VariableReference> GetVariables();
    }
}