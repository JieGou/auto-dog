using System.Windows.Controls.Primitives;

namespace AutoDog.UI.Controls
{
    public class MetroThumbContentControlDragCompletedEventArgs : DragCompletedEventArgs
    {
        public MetroThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
            : base(horizontalOffset, verticalOffset, canceled)
        {
            this.RoutedEvent = MetroThumbContentControl.DragCompletedEvent;
        }
    }
}