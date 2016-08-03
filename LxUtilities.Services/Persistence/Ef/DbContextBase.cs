using System.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxUtilities.Services.Persistence.Ef
{
    public abstract class DbContextBase:DbContext
    {
        protected DbContextBase(string connectionString):base(connectionString)
        {
        }

    }
}
