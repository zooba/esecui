using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace esecui
{
#if MONO
    [Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Docking(DockingBehavior.AutoDock)]
    [DefaultEvent("SplitterMoved")]
    [ComVisible(true)]
    public class CompatibleSplitContainer : SplitContainer, ISupportInitialize
    {

        void ISupportInitialize.BeginInit()
        { }

        void ISupportInitialize.EndInit()
        { }
    }
#else
    [Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Docking(DockingBehavior.AutoDock)]
    [DefaultEvent("SplitterMoved")]
    [ComVisible(true)]
    public class CompatibleSplitContainer : SplitContainer
    { }
#endif
}
