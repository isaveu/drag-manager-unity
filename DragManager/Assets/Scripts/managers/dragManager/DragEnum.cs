using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace managers.dragManager {
    
    public enum DragItemState {
        Idle = 1,
        Ready = 2,
        Dragging = 3
    }

    public enum DragManagerState {
        Idle = 1,
        Ready = 2
    }

}