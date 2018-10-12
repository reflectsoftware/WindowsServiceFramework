using System;
using System.Text;
using System.Diagnostics;
using Plato.Threading.WorkManagement;
using Plato.Core.Logging.Enums;
using Plato.Core.Miscellaneous;
using Plato.Core.Strings;
using ReflectSoftware.Insight;
using ReflectSoftware.Insight.Common;

namespace WindowsServiceFramework.Notifications
{
    public class ServiceLogNotification : WorkManagerNotification
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="nType">Type of the n.</param>
        public override void SendMessage(string msg, NotificationType nType)
        {
            switch (nType)
            {
                case NotificationType.Message:
                    SendMessage(msg);
                    break;

                case NotificationType.Debug:
                    SendDebug(msg);
                    break;

                case NotificationType.Information:
                    SendInformation(msg);
                    break;

                case NotificationType.Warning:
                    SendWarning(msg);
                    break;

                case NotificationType.Error:
                    SendError(msg);
                    break;

                case NotificationType.Fatal:
                    SendFatal(msg);
                    break;
            }
        }

        /// <summary>
        /// Constructs the exception message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private string ConstructExceptionMessage(string msg, Exception ex, params object[] args)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(msg, args);
            sb.AppendLine();
            sb.Append(ExceptionFormatter.ConstructMessage(ex));

            return sb.ToString();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="args">The arguments.</param>
        public void SendMessage(string msg, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).SendMessage(msg, args);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public void SendMessage(string msg, Exception ex, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).Send(MessageType.SendMessage, msg, ExceptionFormatter.ConstructIndentedMessage(ex), args);
        }

        /// <summary>
        /// Sends the debug.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="args">The arguments.</param>
        public override void SendDebug(string msg, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).SendDebug(msg, args);
        }

        /// <summary>
        /// Sends the debug.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public override void SendDebug(string msg, Exception ex, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).Send(MessageType.SendDebug, msg, ExceptionFormatter.ConstructIndentedMessage(ex), args);
        }

        /// <summary>
        /// Sends the information.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="args">The arguments.</param>
        public override void SendInformation(string msg, params object[] args)
        {
            var str = string.Format(msg, args);
            RILogManager.Get(WorkManagerConfig.ApplicationName).SendInformation(str);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, str, EventLogEntryType.Information);
        }

        /// <summary>
        /// Sends the information.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public override void SendInformation(string msg, Exception ex, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).Send(MessageType.SendInformation, msg, ExceptionFormatter.ConstructIndentedMessage(ex), args);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, ConstructExceptionMessage(msg, ex, args), EventLogEntryType.Information);
        }

        /// <summary>
        /// Sends the warning.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="args">The arguments.</param>
        public override void SendWarning(string msg, params object[] args)
        {
            var str = string.Format(msg, args);
            RILogManager.Get(WorkManagerConfig.ApplicationName).SendWarning(str);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, str, EventLogEntryType.Warning);
        }

        /// <summary>
        /// Sends the warning.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public override void SendWarning(string msg, Exception ex, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).Send(MessageType.SendWarning, msg, ExceptionFormatter.ConstructIndentedMessage(ex), args);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, ConstructExceptionMessage(msg, ex, args), EventLogEntryType.Warning);
        }

        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="args">The arguments.</param>
        public override void SendError(string msg, params object[] args)
        {
            var str = string.Format(msg, args);
            RILogManager.Get(WorkManagerConfig.ApplicationName).SendError(str);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, str, EventLogEntryType.Error);
        }

        /// <summary>
        /// Sends the error.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public override void SendError(string msg, Exception ex, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).Send(MessageType.SendError, msg, ExceptionFormatter.ConstructIndentedMessage(ex), args);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, ConstructExceptionMessage(msg, ex, args), EventLogEntryType.Error);
        }

        /// <summary>
        /// Sends the fatal.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="args">The arguments.</param>
        public override void SendFatal(string msg, params object[] args)
        {
            var str = string.Format(msg, args);
            RILogManager.Get(WorkManagerConfig.ApplicationName).SendFatal(str);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, str, EventLogEntryType.Error);
        }

        /// <summary>
        /// Sends the fatal.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The arguments.</param>
        public override void SendFatal(string msg, Exception ex, params object[] args)
        {
            RILogManager.Get(WorkManagerConfig.ApplicationName).Send(MessageType.SendFatal, msg, ExceptionFormatter.ConstructIndentedMessage(ex), args);
            MiscHelper.WriteToEventLog(WorkManagerConfig.ApplicationName, ConstructExceptionMessage(msg, ex, args), EventLogEntryType.Error);
        }

        /// <summary>
        /// Sends the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="bIgnoreTracker">if set to <c>true</c> [b ignore tracker].</param>
        public override void SendException(Exception ex, bool bIgnoreTracker = false)
        {
            base.SendException(ex, true);
        }
    }
}
