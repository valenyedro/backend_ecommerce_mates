using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<CarritoProducto> CarritoProducto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Orden> Orden{ get; set; }
        public DbSet<Producto> Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.HasKey(e => e.CarritoId);
                entity.Property(c => c.CarritoId);

                entity.HasOne(c => c.Orden)
                    .WithOne(c => c.Carrito)
                    .HasForeignKey<Orden>(c => c.CarritoId);

                entity.HasOne(c => c.Cliente)
                    .WithMany(c => c.Carritos)
                    .HasForeignKey(c => c.ClienteId);
            });
            modelBuilder.Entity<CarritoProducto>(entity =>
            {
                entity.HasKey(e => new { e.CarritoId, e.ProductoId });

                entity.HasOne(cp => cp.Carrito)
                    .WithMany(cp => cp.CarritoProductos)
                    .HasForeignKey(cp => cp.CarritoId);

                entity.HasOne(cp => cp.Producto)
                    .WithMany(cp => cp.CarritoProductos)
                    .HasForeignKey(cp => cp.ProductoId);
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);
                entity.Property(c => c.ClienteId).ValueGeneratedOnAdd();
                entity.Property(c => c.DNI).HasColumnType("nvarchar(10)");
                entity.Property(c => c.Nombre).HasColumnType("nvarchar(25)");
                entity.Property(c => c.Apellido).HasColumnType("nvarchar(25)");
                entity.Property(c => c.Direccion).HasColumnType("nvarchar(max)");
                entity.Property(c => c.Telefono).HasColumnType("nvarchar(13)");
                entity.HasData(new Cliente
                {
                    ClienteId = 1,
                    DNI = "43655405",
                    Nombre = "Valentin",
                    Apellido = "Yedro",
                    Direccion = "Calle 17 1234",
                    Telefono = "1125908161",
                });
            });
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.OrdenId);
                entity.Property(o => o.Total).HasColumnType("decimal(15,2)");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProductoId);
                entity.Property(p => p.ProductoId).ValueGeneratedOnAdd();
                entity.Property(p => p.Nombre).HasColumnType("nvarchar(max)");
                entity.Property(p => p.Descripcion).HasColumnType("nvarchar(max)");
                entity.Property(p => p.Marca).HasColumnType("nvarchar(25)");
                entity.Property(p => p.Codigo).HasColumnType("nvarchar(25)");
                entity.Property(p => p.Precio).HasColumnType("decimal(15,2)");
                entity.Property(p => p.Image).HasColumnType("nvarchar(max)");
                entity.HasData(new Producto
                {
                    ProductoId = 1,
                    Nombre = "Mate camionero ALUMINIO",
                    Descripcion = "Un mate estilo camionero con virola de aluminio y recubierto de cuero.\r\n• Mate elaborado con calabaza brasilera seleccionada.\r\n• Virola de aluminio lisa.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.",
                    Marca = "Mates Berazategui",
                    Codigo = "#1",
                    Precio = 2000,
                    Image = "https://i.imgur.com/kBpL61h.jpg",
                }, new Producto
                {
                    ProductoId = 2,
                    Nombre = "Mate torpedo ALUMINIO",
                    Descripcion = "Un mate estilo torpedo con virola de aluminio y recubierto de cuero.\r\n• Mate elaborado con calabaza brasilera seleccionada.\r\n• Virola de aluminio lisa.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.",
                    Marca = "Mates Berazategui",
                    Codigo = "#2",
                    Precio = 2000,
                    Image = "https://i.imgur.com/OBgi9Rq.jpg",
                }, new Producto
                {
                    ProductoId = 3,
                    Nombre = "Mate camionero ALUMINIO CINCELADO",
                    Descripcion = "Un mate estilo camionero con virola de aluminio cincelada y recubierto de cuero.\r\n• Mate elaborado con calabaza brasilera seleccionada.\r\n• Virola de aluminio cincelada.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.\r\n• Los mejores detalles para un mate mucho más estético.",
                    Marca = "Mates Berazategui",
                    Codigo = "#3",
                    Precio = 2300,
                    Image = "https://i.imgur.com/rTMC88Q.jpg",
                }, new Producto
                {
                    ProductoId = 4,
                    Nombre = "Mate torpedo ALUMINIO CINCELADO",
                    Descripcion = "Un mate estilo torpedo con virola de aluminio cincelada y recubierto de cuero.\r\n• Mate elaborado con calabaza brasilera seleccionada.\r\n• Virola de aluminio cincelada.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.\r\n• Los mejores detalles para un mate mucho más estético.",
                    Marca = "Mates Berazategui",
                    Codigo = "#4",
                    Precio = 2300,
                    Image = "https://i.imgur.com/lnaf51K.jpg",
                }, new Producto
                {
                    ProductoId = 5,
                    Nombre = "Mate camionero ALPACA LISA",
                    Descripcion = "Un mate estilo camionero con virola de alpaca lisa y recubierto de cuero.\r\n• La máxima calidad en la virola tu mate.\r\n• Elaborado con calabaza brasilera seleccionada para una calidad óptima.\r\n• Virola de alpaca lisa.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.",
                    Marca = "Mates Berazategui",
                    Codigo = "#5",
                    Precio = 3000,
                    Image = "https://i.imgur.com/QChC7FG.jpg",
                }, new Producto
                {
                    ProductoId = 6,
                    Nombre = "Mate torpedo ALPACA LISA",
                    Descripcion = "Un mate estilo torpedo con virola de alpaca lisa y recubierto de cuero.\r\n• La máxima calidad en la virola tu mate.\r\n• Elaborado con calabaza brasilera seleccionada para una calidad óptima.\r\n• Virola de alpaca lisa.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.",
                    Marca = "Mates Berazategui",
                    Codigo = "#6",
                    Precio = 3000,
                    Image = "https://i.imgur.com/gRMsfCx.jpg",
                }, new Producto
                {
                    ProductoId = 7,
                    Nombre = "Mate camionero ALPACA CINCELADA",
                    Descripcion = "Un mate estilo camionero con virola de alpaca cincelada y recubierto de cuero.\r\n• La máxima calidad y detalle en la virola tu mate.\r\n• Elaborado con calabaza brasilera seleccionada para una calidad óptima.\r\n• Virola de alpaca cincelada.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.",
                    Marca = "Mates Berazategui",
                    Codigo = "#7",
                    Precio = 3300,
                    Image = "https://i.imgur.com/gt1RiK4.jpg",
                }, new Producto
                {
                    ProductoId = 8,
                    Nombre = "Mate torpedo ALPACA CINCELADA",
                    Descripcion = "Un mate estilo torpedo con virola de alpaca cincelada y recubierto de cuero.\r\n• La máxima calidad y detalle en la virola tu mate.\r\n• Elaborado con calabaza brasilera seleccionada para una calidad óptima.\r\n• Virola de alpaca cincelada.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.",
                    Marca = "Mates Berazategui",
                    Codigo = "#8",
                    Precio = 3300,
                    Image = "https://i.imgur.com/DNDhDf6.jpg",
                }, new Producto
                {
                    ProductoId = 9,
                    Nombre = "Mate camionero ALPACA GRABADA",
                    Descripcion = "Un mate estilo camionero con virola de alpaca grabada totalmente personalizada y recubierto de cuero.\r\n• Elaborado con calabaza brasilera seleccionada para una calidad óptima.\r\n• Virola de alpaca.\r\n• Grabado láser.\r\n• Forrado en cuero.\r\n• Con refuerzo en las patas para máxima estabilidad.\r\n• ¡Pedinos tu diseño y te lo hacemos!",
                    Marca = "Mates Berazategui",
                    Codigo = "#9",
                    Precio = 4200,
                    Image = "https://i.imgur.com/Slb5Ojf.jpg",
                }, new Producto
                {
                    ProductoId = 10,
                    Nombre = "Bombilla pico loro ACERO INOXIDABLE",
                    Descripcion = "Una bombilla ideal para un mate ideal. Con estilo pico loro para máximo confort.\r\n• Acero inoxidable.\r\n• Medida aproximada: 19cm.\r\n• Bombilla estilo pico loro para mayor confort.",
                    Marca = "Mates Berazategui",
                    Codigo = "#10",
                    Precio = 800,
                    Image = "https://i.imgur.com/3UPfdzA.jpg",
                });
            });
        }
    }
}
