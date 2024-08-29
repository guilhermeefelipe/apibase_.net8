using Microsoft.EntityFrameworkCore;

namespace ApiBase.Repositories.DbContexts.Base;

public abstract class DbContextBase : DbContext
{
    protected DbContextBase(DbContextOptions options)
        : base(options)
    {
    }

    protected DbContextBase()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EnforceRestrictDeleteBehavior(modelBuilder);
        CreateIndexes(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void EnforceRestrictDeleteBehavior(ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(i => i.GetForeignKeys())
            .Where(i => i.DeleteBehavior != DeleteBehavior.Restrict);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    private static void CreateIndexes(ModelBuilder modelBuilder)
    {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity.ClrType is not null)
            {
                foreach (var att in GetAttributes<Domain.Entities.Base.IndexAttribute>(entity.ClrType))
                    modelBuilder.Entity(entity.ClrType).HasIndex(att.Fields);

                foreach (var att in GetAttributes<Domain.Entities.Base.UniqueIndexAttribute>(entity.ClrType))
                    modelBuilder.Entity(entity.ClrType).HasIndex(att.Fields).IsUnique();
            }
        }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    }

    private static IEnumerable<T> GetAttributes<T>(Type type) where T : Attribute
        => type.GetCustomAttributes(typeof(T), false).OfType<T>();
}
