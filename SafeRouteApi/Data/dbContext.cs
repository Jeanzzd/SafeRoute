using Microsoft.EntityFrameworkCore;
using SafeRoute.Models;
using System.Collections.Generic;

namespace SafeRouteApi.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }
        public DbSet<AreasComRiscos> AreasComRiscos { get; set; }
        public DbSet<RotasSegurancas> RotasSegurancas { get; set; }
    }
}
