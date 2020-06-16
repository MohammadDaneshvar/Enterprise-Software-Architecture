using Domain.Person;
using Framework.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.EF
{
    public class FRIQueryDbContext : DbContext
    {

        public FRIQueryDbContext()
        {

        }
        public FRIQueryDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
