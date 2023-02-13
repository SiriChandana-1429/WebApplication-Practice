using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace WebApplication2.Models;
public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options)
    : base(options) { }

    public DbSet<Bank> Banks => Set<Bank>();
}