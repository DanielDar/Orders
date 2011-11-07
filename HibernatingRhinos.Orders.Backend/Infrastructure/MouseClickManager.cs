using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public class MouseClickManager
    {
        private event MouseButtonEventHandler click;
        private event MouseButtonEventHandler doubleClick;

        public MouseClickManager(int doubleClickTimeout)
        {
            this.Clicked = false;
            this.DoubleClickTimeout = doubleClickTimeout;
        }

        public event MouseButtonEventHandler Click
        {
            add { click += value; }
            remove { click -= value; }
        }

        public event MouseButtonEventHandler DoubleClick
        {
            add { doubleClick += value; }
            remove { doubleClick -= value; }
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (click != null)
            {
                Debug.Assert(sender is Control);
                (sender as Control).Dispatcher.BeginInvoke(click, sender, e);
            }
        }

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (doubleClick != null)
            {
                doubleClick(sender, e);
            }
        }

        public void HandleClick(object sender, MouseButtonEventArgs e)
        {
            lock (this)
            {
                if (this.Clicked)
                {
                    this.Clicked = false;
                    OnDoubleClick(sender, e);
                }
                else
                {
                    this.Clicked = true;
                    var threadStart = new ParameterizedThreadStart(ResetThread);
                    var thread = new Thread(threadStart);
                    thread.Start(e);
                }
            }
        }

        private bool Clicked { get; set; }

        public int DoubleClickTimeout { get; set; }

        private void ResetThread(object state)
        {
            Thread.Sleep(this.DoubleClickTimeout);

            lock (this)
            {
                if (this.Clicked)
                {
                    this.Clicked = false;
                    OnClick(this, (MouseButtonEventArgs)state);
                }
            }
        }
    }
}
