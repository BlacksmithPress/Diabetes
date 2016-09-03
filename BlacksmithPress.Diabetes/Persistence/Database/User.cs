using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Persistence.Database
{
    public class User : Person
    {
        public string Password { get; set; }
    }
}
