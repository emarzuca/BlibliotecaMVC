using BlibliotecaMVC.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BlibliotecaMVC
{
    public class AplicationDbContext: IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions options ) : base( options )
        
        { 
        
        }

        public DbSet<CatApartado> CatApartados   { get; set; }
    }
}
