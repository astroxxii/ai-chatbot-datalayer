using Microsoft.EntityFrameworkCore;
using TugasData.MessageDao;

namespace TugasData.Context
{
    /// <summary>
    /// Entity Framework Core database context for TugasData.
    /// Manages chat sessions and conversation history.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet for chat conversation sessions.
        /// </summary>
        public DbSet<Messages> Messages { get; set; } = null!;

        /// <summary>
        /// DbSet for individual message details.
        /// </summary>
        public DbSet<MessageDetail> MessageDetail { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.HasMany(e => e.messages)
                      .WithOne()
                      .HasForeignKey("Messagesid")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MessageDetail>(entity =>
            {
                entity.HasKey(e => e.id);
            });
        }
    }
}
