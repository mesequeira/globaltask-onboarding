using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Users.Persistence;

public static class InMemoryDbContextFactory
{
    public static IApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}
