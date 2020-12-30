using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Validators
{
    public class DateValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if( value is DateTime date)
            {
               return date <= DateTime.Now;
            }

            return false;
        }
    }
}
