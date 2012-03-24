using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class ConstraintResult
    {
        readonly bool fulfilled;
        readonly string errorMessage;
        public bool ConstraintFulfilled { get { return fulfilled; } }
        public string ErrorMessage { get { return errorMessage; } }
        public ConstraintResult(bool aFulfilled, string aErrorMessage)
        {
            if (!aFulfilled && aErrorMessage.Length == 0)
            {
                throw new ArgumentException("Message can't be empty when not fulfilled.", "aErrorMessage");
            }
            fulfilled = aFulfilled;
            errorMessage = aErrorMessage;
        }
        public override string ToString()
        {
            return string.Format("Fullfilled: {0}, {1}", fulfilled, errorMessage);
        }
    }
}
