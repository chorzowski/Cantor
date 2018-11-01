using System.Data.Entity;

namespace ExchangeApplication.Models
{
    public interface IApplicationDbContext
    {
        DbSet<Info> Infoes { get; set; }
    }
}