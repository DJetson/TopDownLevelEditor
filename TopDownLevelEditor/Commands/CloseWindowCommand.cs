using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TopDownLevelEditor.Commands
{
    public class CloseWindowCommand : RequeryBase
    {
        public override bool CanExecute(object parameter)
        {
            var Parameter = parameter as Window;
            if (parameter == null)
                return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            var Parameter = parameter as Window;

            Parameter.Close();
        }
    }
}
