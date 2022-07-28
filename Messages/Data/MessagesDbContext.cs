using Messages.Models;
using Microsoft.EntityFrameworkCore;

namespace Messages.Data
{
    public class MessagesDbContext: DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }
    }
}
