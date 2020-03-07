using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TopDownLevelEditor.Views.Gestures
{
    public class MouseWheelUpGesture : MouseGesture
    {

        public MouseWheelUpGesture() : base(MouseAction.WheelClick)
        {
        }

        public MouseWheelUpGesture(ModifierKeys modifiers) : base(MouseAction.WheelClick, modifiers)
        {
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!base.Matches(targetElement, inputEventArgs))
                return false;

            if (!(inputEventArgs is MouseWheelEventArgs args))
                return false;

            if (args.Delta <= 0)
                return false;

            return true;
        }
    }

    public class MouseWheelGesture
    {
        public static MouseWheelUpGesture WheelUp => new MouseWheelUpGesture();
        public static MouseWheelDownGesture WheelDown => new MouseWheelDownGesture();
    }


    public class MouseWheelDownGesture : MouseGesture
    {
        public MouseWheelDownGesture() : base(MouseAction.WheelClick)
        {
        }

        public MouseWheelDownGesture(ModifierKeys modifiers) : base(MouseAction.WheelClick, modifiers)
        {
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!base.Matches(targetElement, inputEventArgs))
                return false;

            if (!(inputEventArgs is MouseWheelEventArgs args))
                return false;

            if (args.Delta >= 0)
                return false;

            return true;
        }
    }
}
