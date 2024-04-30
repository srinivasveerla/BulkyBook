using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicatonDBContext :DbContext
    {
        public ApplicatonDBContext(DbContextOptions<ApplicatonDBContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}