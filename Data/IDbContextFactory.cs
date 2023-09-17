namespace StudentPortalAPI.Data;

public interface IDbContextFactory
{
    DataContext CreateDbContext();
}

