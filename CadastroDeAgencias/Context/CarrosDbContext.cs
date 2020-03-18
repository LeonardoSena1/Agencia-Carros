using CadastroDeAgencias.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CadastroDeAgencias.Context
{
    public class CarrosDbContext : DbContext
    {
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Agencia> Agencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}