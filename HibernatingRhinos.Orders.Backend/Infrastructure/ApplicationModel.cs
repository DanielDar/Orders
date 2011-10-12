using System.Windows;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public class ApplicationModel
    {
        private static string threadSafeNavigationState;

        static ApplicationModel()
        {
            threadSafeNavigationState = Application.Current.Host.NavigationState;
            Application.Current.Host.NavigationStateChanged +=
                (sender, args) =>
                {
                    threadSafeNavigationState = args.NewNavigationState;
                };
        }

        public static string NavigationState { get { return threadSafeNavigationState; } }
    }
}