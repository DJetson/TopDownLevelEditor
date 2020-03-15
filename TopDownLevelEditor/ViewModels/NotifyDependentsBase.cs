using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public abstract class NotifyDependentsBase : NotifyBase
    {
        protected abstract void NotifyDependentProperties([CallerMemberName] string property = "");
    }
}
