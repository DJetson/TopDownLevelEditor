using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.Commands
{
    public class AddTileCommand : RequeryBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
