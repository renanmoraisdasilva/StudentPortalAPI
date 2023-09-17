namespace StudentPortalAPI.Data;

public class DbContextFactory : IDbContextFactory
{
    private readonly IConfiguration _configuration;

    public DbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DataContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PortalNotasContext"));
        return new DataContext(optionsBuilder.Options);
    }

}

