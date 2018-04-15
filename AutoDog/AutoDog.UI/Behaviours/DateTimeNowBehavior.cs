namespace AutoDog.UI.Behaviours
{
    using System;
    using System.Windows.Interactivity;
    using System.Windows.Threading;
    using AutoDog.UI.Controls;

    public class DateTimeNowBehavior : Behavior<DateTimePicker>
    {
        private DispatcherTimer _dispatcherTimer;

        protected override void OnAttached()
        {
            base.OnAttached();
            _dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.DataBind, (sender, args) => AssociatedObject.SelectedDate = DateTime.Now, Dispatcher.CurrentDispatcher);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _dispatcherTimer.Stop();
        }
    }
}
