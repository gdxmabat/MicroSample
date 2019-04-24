using MicroSample.Domain.Configuration;
using MicroSample.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;


namespace MicroSample.Domain
{
  public class DataContext : DbContext
  {
    protected DataContext()
    {
    }

    public DataContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      this.LoadConfigurations(modelBuilder);
    }

    private void LoadConfigurations(ModelBuilder builder)
    {
      var configurations = this.GetType()
                                  .GetTypeInfo()
                                  .Assembly.ExportedTypes
                                  .Where(x => typeof(IEntityTypeConfiguration).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract)
                                  .ToArray();
      foreach (var c in configurations)
      {
        var config = Activator.CreateInstance(c) as IEntityTypeConfiguration;
        config?.Configure(builder);
      }

     
    }

    public void Seed()
    {
      var testUser1 = new User
      {
        Id = 1,
        FirstName = "Luke",
        LastName = "Skywalker"
      };

      this.Add(testUser1);

      var testPost1 = new Post
      {
        Id = 1,
        UserId = testUser1.Id,
        Content = "What a piece of junk!"
      };

      this.Add(testPost1);

      this.SaveChanges();
    }
  }
}
