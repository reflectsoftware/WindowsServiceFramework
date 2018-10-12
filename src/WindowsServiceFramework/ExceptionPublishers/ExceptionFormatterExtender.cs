using Plato.Core.Strings;
using System;
using System.Collections.Generic;

namespace WindowsServiceFramework.ExceptionPublishers
{
    public class ExceptionFormatterExtender
    {
        /// <summary>
        /// Extenders the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public IEnumerable<ExceptionFormatterExtension> Extender(Exception exception)
        {
            var extensions = new List<ExceptionFormatterExtension>();

            //if (exception is DbEntityValidationException)
            //{
            //    foreach (var errors in (exception as DbEntityValidationException).EntityValidationErrors)
            //    {
            //        var extension = new ExceptionFormatterExtension($"Error type: {errors.GetType().Name}");
            //        extensions.Add(extension);

            //        foreach (var error in errors.ValidationErrors)
            //        {
            //            extension.Properties[error.PropertyName] = error.ErrorMessage;
            //        }
            //    }
            //}
            //else if (exception is SqlException)
            //{
            //    var sqlException = (exception as SqlException);
            //    if (sqlException.Errors.Count > 0)
            //    {

            //        var extension = new ExceptionFormatterExtension("SqlErrors");
            //        extensions.Add(extension);

            //        var count = 1;
            //        foreach (var error in sqlException.Errors)
            //        {
            //            var sqlError = (error as SqlError);
            //            extension.Properties[$"Error({count++})"] = $"{sqlError.Message} at line number: {sqlError.LineNumber}";
            //        }
            //    }
            //}

            return extensions;
        }
    }
}
