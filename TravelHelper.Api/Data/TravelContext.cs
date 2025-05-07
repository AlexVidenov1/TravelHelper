using Microsoft.EntityFrameworkCore;
using TravelHelper.Api.Models;

namespace TravelHelper.Api.Data;
public class TravelContext : DbContext
{
    public TravelContext(DbContextOptions<TravelContext> o) : base(o) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
}