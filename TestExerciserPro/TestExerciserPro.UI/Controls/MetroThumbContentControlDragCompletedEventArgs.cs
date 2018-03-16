using System.Windows.Controls.Primitives;

namespace TestExerciserPro.UI.Controls
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