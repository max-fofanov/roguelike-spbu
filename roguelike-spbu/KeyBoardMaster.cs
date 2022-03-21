using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike_spbu
{
    delegate void KeyboardDelegate();
    class KeyBoardMaster
    {
        public event KeyboardDelegate? downPressedEvent = null;
        public event KeyboardDelegate? upPressedEvent = null;
        public event KeyboardDelegate? leftPressedEvent = null;
        public event KeyboardDelegate? rightPressedEvent = null;

        public void UpPressedEvent()
        {
            if (upPressedEvent != null)
                upPressedEvent.Invoke();
        }

        public void DownPressedEvent()
        {
            if (downPressedEvent != null)
                downPressedEvent.Invoke();
        }

        public void LeftPressedEvent()
        {
            if (leftPressedEvent != null)
                leftPressedEvent.Invoke();
        }

        public void RightPressedEvent()
        {
            if (rightPressedEvent != null)
                rightPressedEvent.Invoke();
        }
    }
}
