using Microsoft.EntityFrameworkCore;
using _8_Calculator.DB.Entities;
using _8_Calculator.Enums;
using static _8_Calculator.Enums.OperationEnum;

public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Calculation> Calculations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculation>().HasData(
            new Calculation
            {
                Id = 1,
                Operand1 = 12,
                Operand2 = 2,
                Operation = OperationType.Add.ToString(),
                Result = 14
            });
    }
}

