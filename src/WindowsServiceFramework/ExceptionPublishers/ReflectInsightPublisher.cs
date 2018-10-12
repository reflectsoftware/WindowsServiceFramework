using Plato.ExceptionManagement.Interfaces;
using Plato.Core.Strings;
using ReflectSoftware.Insight;
using ReflectSoftware.Insight.Common;
using System;
using System.Collections.Specialized;

namespace WindowsServiceFramework.ExceptionPublishers
{
    public class ReflectInsightPublisher : IExceptionPublisher
    {
        private readonly string _category;
        private readonly ExceptionFormatterExtender _exceptionExtender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectInsightPublisher"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="args">The arguments.</param>
        public ReflectInsightPublisher(NameValueCollection parameters, params object[] args)
        {
            _category = StringHelper.IfNullOrEmptyUseDefault(parameters["category"], "RIExceptionPublisher");
            _exceptionExtender = new ExceptionFormatterExtender();
        }

        /// <summary>
        /// Publishes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="additionalParameters">The additional parameters.</param>
        public void Publish(Exception exception, NameValueCollection additionalParameters)
        {
            if (additionalParameters != null)
            {
                foreach (var key in additionalParameters.AllKeys)
                {
                    RIExtendedMessageProperty.AttachToSingleMessage("Additional Info", key, additionalParameters[key]);
                }
            }

            var details = ExceptionFormatter.ConstructIndentedMessage(exception, additionalParameters/*, _exceptionExtender.Extender*/);
            RILogManager.Get(_category).Send(MessageType.SendException, exception.Message, details);
        }
    }
}
