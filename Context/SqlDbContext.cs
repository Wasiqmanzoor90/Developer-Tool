using Microsoft.EntityFrameworkCore;
using MyApiProject.Model.Entites;

namespace MyApiProject.Context;

public class SqlDbContext: DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {

    }
    
    public DbSet<Collection> Collections { get; set; }
    public DbSet<HttpModel> HttpModels { get; set; }

    public DbSet<RequestHistory> Requests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Collection ↔ HttpModel (1-to-many)
        modelBuilder.Entity<HttpModel>()
            .HasOne(h => h.Collection)
            .WithMany(c => c.Request)
            .HasForeignKey(h => h.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<RequestHistory>()
        .HasOne(r => r.HttpModel)
        .WithMany(h => h.RequestHistories)
        .HasForeignKey(r => r.HttpModelId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}