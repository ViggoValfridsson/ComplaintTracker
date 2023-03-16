using Data_Storage_Submission.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Storage_Submission.Context;

internal class DataContext : DbContext
{
    protected DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\viggo\source\repos\Inlämningar\Data_Storage_Submission\Data_Storage_Submission\Context\submisson_db.mdf;Integrated Security=True;Connect Timeout=30");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DepartmentEntity> Departments { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes{ get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<CustomerEntity> Customers{ get; set; }
    public DbSet<ComplaintEntity> Complaints { get; set; }
    public DbSet<CommentEntity> Comments{ get; set; }
}
