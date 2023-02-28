using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public enum ResultStatus
    {
        Ok = 200, WithError = 400, Warning = 300, Information = 100, Allert = 500, Bare = 600
    }
}
