using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using HibernatingRhinos.Orders.Backend.Infrastructure;

namespace HibernatingRhinos.Orders.Backend.Behaviors
{
    public class DataGridDoubleClickBehavior : Behavior<DataGrid>
    {

        private readonly MouseClickManager gridClickManager;
        public event EventHandler<MouseButtonEventArgs> DoubleClick;



        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(DataGridDoubleClickBehavior), new PropertyMetadata(CommandParameterChanged));

        private static void CommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }
        public ICommand DoubleClickCommand
        {
            get { return (ICommand)GetValue(DoubleClickCommandProperty); }
            set { SetValue(DoubleClickCommandProperty, value); }
        }

        public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register("DoubleClickCommand", typeof(ICommand), typeof(DataGridDoubleClickBehavior), new PropertyMetadata(DoubleClickCommandChanged));

        private static void DoubleClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Code for dealing with your property changes
        }

        public DataGridDoubleClickBehavior()
        {
            gridClickManager = new MouseClickManager(300);
            gridClickManager.DoubleClick += new MouseButtonEventHandler(gridClickManager_DoubleClick);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.LoadingRow += OnLoadingRow;
            AssociatedObject.UnloadingRow += OnUnloadingRow;
        }

        void OnUnloadingRow(object sender, DataGridRowEventArgs e)
        {
            //row is no longer visible so remove double click event otherwise
            //row events will miss fire
            e.Row.MouseLeftButtonUp -= gridClickManager.HandleClick;
        }

        void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            //row is visible in grid, wire up double click event
            e.Row.MouseLeftButtonUp += gridClickManager.HandleClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.LoadingRow -= OnLoadingRow;
            AssociatedObject.UnloadingRow -= OnUnloadingRow;
        }

        void gridClickManager_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(sender, e);

            DoubleClickCommand.Execute(CommandParameter);
        }

    }
}
