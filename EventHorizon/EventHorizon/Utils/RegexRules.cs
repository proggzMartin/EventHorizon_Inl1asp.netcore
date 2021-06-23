using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizon.Utils
{
    public class RegexRules
    {
        public const string NameRule = "^[a-zA-ZåäöÅÄÖ]{2,20}$";
        public const string NameErrorMessage = "First and Lastnames may be 2-20 chars long, only letters";

    }
}
