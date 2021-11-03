using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{


    //TODO: Revisar UsersSecrets
    //https://www.youtube.com/watch?v=MIJJCR3ndQQ

    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Familia> Familias { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }
        public DbSet<CategoriaTransaccion> CategoriasTransacciones { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Donante>Donantes { get; set; }

    }
}
