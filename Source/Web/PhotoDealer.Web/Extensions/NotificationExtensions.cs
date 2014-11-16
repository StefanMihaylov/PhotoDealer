namespace PhotoDealer.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /*                                                                      *
     *      This extension was derive from Brad Christie's answer           *
     *      on StackOverflow.                                               *
     *                                                                      *
     *      The original code can be found at:                              *
     *      http://stackoverflow.com/a/18338264/998328                      *
     *                                                                      */

    public static class NotificationExtensions
    {
        private static IDictionary<string, string> notificationKey = new Dictionary<string, string>
        {
            { "Error",      "App.Notifications.Error" }, 
            { "Warning",    "App.Notifications.Warning" },
            { "Success",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };

        public static void AddNotification(this ControllerBase controller, string message, string notificationType)
        {
            string notificationKey = GetNotificationKeyByType(notificationType);
            ICollection<string> messages = controller.TempData[notificationKey] as ICollection<string>;

            if (messages == null)
            {
                messages = new HashSet<string>();
                controller.TempData[notificationKey] = messages;
            }

            messages.Add(message);
        }

        public static IEnumerable<string> GetNotifications(this HtmlHelper htmlHelper, string notificationType)
        {
            string notificationKey = GetNotificationKeyByType(notificationType);
            return htmlHelper.ViewContext.Controller.TempData[notificationKey] as ICollection<string> ?? null;
        }

        private static string GetNotificationKeyByType(string notificationType)
        {
            try
            {
                return notificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
    }
}