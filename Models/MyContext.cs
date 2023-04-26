#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace LetterCounter.Models;

public class MyContext : DbContext
{

    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<TextInput> TextInputs { get; set; }
}