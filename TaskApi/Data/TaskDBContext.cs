using Microsoft.EntityFrameworkCore;
using TaskApi.Models;

namespace TaskApi.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options): base(options) 
        {

        }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
