using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Data.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}
