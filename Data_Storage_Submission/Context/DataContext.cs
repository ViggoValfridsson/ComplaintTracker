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
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
