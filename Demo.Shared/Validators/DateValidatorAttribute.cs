using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Shared.Validators
{
    /// <summary>
    /// Validate Date to be less than DateTime.now
    /// </summary>
    public class DateValidatorAttribute : ValidationAttribute
    {
        public DateValidatorAttribute()
        {

        }

        public DateValidatorAttribute(string errorMessage) : base (errorMessage)
        {
                
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }

            return false;
        }
    }
}
