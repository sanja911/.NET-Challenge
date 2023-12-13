using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Projects.Models;

namespace DotnetWebApiWithEFCodeFirst.Models
{
    public class StudentGradeDBContext : DbContext
    {
        public StudentGradeDBContext(DbContextOptions<StudentGradeDBContext> options)
            : base(options)
        {
        }
        public DbSet<Grades> Grades { get; set; }
    }

    public class TransactionDBContext : DbContext
    {
        public TransactionDBContext(DbContextOptions<TransactionDBContext> options)
            : base(options)
        {
        }
        public DbSet<TransactionHeader> TransactionsHeader { get; set; }
        public DbSet<TransactionDetail> TransactionDetail { get; set; }
    }

    public class AuthDBContext : DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options)
            : base(options)
        {
        }
    }
}