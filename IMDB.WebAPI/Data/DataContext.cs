using Microsoft.EntityFrameworkCore;
using IMDB.WebAPI.Models.Entidades;

namespace IMDB.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Usuario> BD_Usuarios { get; set; }

    }
}