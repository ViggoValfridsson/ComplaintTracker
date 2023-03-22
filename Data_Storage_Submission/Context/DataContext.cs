using Data_Storage_Submission.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data_Storage_Submission.Context;

internal class DataContext : DbContext
{
    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string workingDirectory = Environment.CurrentDirectory;
        string currentDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
        string dbDirectory = $"{currentDirectory}\\Context";

        SqlConnectionStringBuilder builder =
            new()
            {
                ["Data Source"] = @"(LocalDB)\MSSQLLocalDB",
                ["AttachDbFilename"] = @$"{dbDirectory}\submisson_db.mdf",
                ["integrated Security"] = true,
                ["Connect Timeout"] = 30
            };

        optionsBuilder.UseSqlServer(builder.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DepartmentEntity> Departments { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ComplaintEntity> Complaints { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
