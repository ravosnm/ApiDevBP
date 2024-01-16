using ApiDevBP.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDevBP
{
    public class ApiDevBPContext : DbContext
    {
        public ApiDevBPContext(DbContextOptions<ApiDevBPContext> options) : base(options) { }

        public DbSet<UserEntity> UserEntities { get; set; }
    }
}
