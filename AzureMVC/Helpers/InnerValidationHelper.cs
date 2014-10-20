using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace AzureMVC.Helpers
{
    public class InnerValidationHelper
    {
        /// <summary>
        /// Get entity validation errors from exception object
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEntityInnerValidationErrors(DbEntityValidationException e)
        {
            var sb = new StringBuilder();
            foreach (var failure in e.EntityValidationErrors)
            {
                sb.AppendFormat("{0} klaida", failure.Entry.Entity.GetType());
                foreach (var error in failure.ValidationErrors)
                {
                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        public static string GetInnerValidationErrors(Exception e)
        {
            var sb = new StringBuilder();

            sb.Append(e.Message);
            if (e.InnerException != null)
                sb.Append(" " + e.InnerException.Message);

            return sb.ToString();
        }
    }
}