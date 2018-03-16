using System.Windows.Controls.Primitives;

namespace TestExerciserPro.UI.Controls
{
    public class MetroThumbContentControlDragStartedEventArgs : DragStartedEventArgs
    {
        public MetroThumbContentControlDragStartedEventArgs(double horizontalOffset, double verticalOffset)
            : base(horizontalOffset, verticalOffset)
        {
            this.RoutedEvent = MetroThumbContentControl.DragStartedEvent;
        }
    }
}