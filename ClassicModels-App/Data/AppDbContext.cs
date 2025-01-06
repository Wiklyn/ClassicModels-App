using ClassicModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClassicModels.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLine> ProductLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=ClassicModels;Username=user_name;Password=user_password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Properties/Attributes names Configuration ---------------------

            modelBuilder.Entity<ProductLine>()
                .Property(productLine => productLine.ProductLineName)
                .HasColumnName("productline");

            modelBuilder.Entity<Product>()
                .Property(productLine => productLine.ProductLineName)
                .HasColumnName("productline");

            // Primary Key Configurations ------------------------------------

            modelBuilder.Entity<Customer>()
                .HasKey(customer => customer.CustomerNumber);

            modelBuilder.Entity<Employee>()
                .HasKey(employee => employee.EmployeeNumber);

            modelBuilder.Entity<Office>()
                .HasKey(office => office.OfficeCode);

            modelBuilder.Entity<Order>()
                .HasKey(order => order.OrderNumber);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(orderDetail => new { orderDetail.OrderNumber, orderDetail.ProductCode });

            modelBuilder.Entity<Payment>()
                .HasKey(payment => new { payment.CustomerNumber, payment.CheckNumber });

            modelBuilder.Entity<Product>()
                .HasKey(product => product.ProductCode);

            modelBuilder.Entity<ProductLine>()
                .HasKey(productLine => productLine.ProductLineName);

            // Relationship Configurations -----------------------------------

            modelBuilder.Entity<OrderDetail>()
                .HasOne(orderDetail => orderDetail.Order)
                .WithMany(customer => customer.OrderDetail)
                .HasForeignKey(orderDetail => orderDetail.OrderNumber);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(orderDetail => orderDetail.Product)
                .WithMany(product => product.OrdersDetails)
                .HasForeignKey(orderDetail => orderDetail.ProductCode);

            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.Manager)
                .WithMany(manager => manager.Subordinates)
                .HasForeignKey(employee => employee.ReportsTo);

            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.Office)
                .WithMany(office => office.Employees)
                .HasForeignKey(employee => employee.OfficeCode);

            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.Employee)
                .WithMany(employee => employee.Customers)
                .HasForeignKey(customer => customer.SalesRepEmployeeNumber);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(o => o.CustomerNumber);

            modelBuilder.Entity<Payment>()
                .HasOne(payment => payment.Customer)
                .WithMany(customer => customer.Payments)
                .HasForeignKey(payment => payment.CustomerNumber);

            modelBuilder.Entity<Product>()
                .HasOne(product => product.ProductLineNavigation)
                .WithMany(productLine => productLine.Products)
                .HasForeignKey(product => product.ProductLineName);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.GetTableName().ToLower());

                foreach (var property in entityType.GetProperties())
                {
                    var configuredColumnName = property.GetColumnName(
                        StoreObjectIdentifier.Table(
                            entityType.GetTableName(),
                            entityType.GetSchema()
                        )
                    );

                    if (configuredColumnName == null || configuredColumnName == property.Name)
                    {
                        property.SetColumnName(property.Name.ToLower());
                    }
                }
            }
        }
    }
}
