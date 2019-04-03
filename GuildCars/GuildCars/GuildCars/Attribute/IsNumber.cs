using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.Attribute
{
    public class IsNumber : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            if (value is string)
            {
                int number = 0;

                bool res = int.TryParse((string)value, out number);

                if (res == false)
                {
                    return false;
                }

                if(number < 0)
                {
                    return false;
                }

                return true;

            }

            return false;

        }
    }
}