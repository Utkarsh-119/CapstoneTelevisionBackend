using CapstoneTelevision.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTelevision.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<MediaLibrary> MediaLibraries { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ViewerFeedback> ViewerFeedbacks { get; set; }
        public DbSet<Talent> Talents { get; set; }
        public DbSet<CostManagement> CostManagements { get; set; }
        public DbSet<ComplianceRecord> ComplianceRecords { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Table Relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Shows)
                .WithOne(s => s.Producer)
                .HasForeignKey(s => s.ProducerId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.MediaLibraries)
                .WithOne(m => m.Uploader)
                .HasForeignKey(m => m.UploaderId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);

            // Show Table Relationships
            modelBuilder.Entity<Show>()
                .HasMany(s => s.Contents)
                .WithOne(c => c.Show)
                .HasForeignKey(c => c.ShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.Advertisements)
                .WithOne(a => a.AssignedShow)
                .HasForeignKey(a => a.AssignedShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.MediaLibraries)
                .WithOne(m => m.Show)
                .HasForeignKey(m => m.ShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.Schedules)
                .WithOne(sc => sc.Show)
                .HasForeignKey(sc => sc.ShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.ViewerFeedbacks)
                .WithOne(vf => vf.Show)
                .HasForeignKey(vf => vf.ShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.ComplianceRecords)
                .WithOne(cr => cr.Show)
                .HasForeignKey(cr => cr.ShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.Talents)
                .WithOne(t => t.Show)
                .HasForeignKey(t => t.ShowId);

            modelBuilder.Entity<Show>()
                .HasMany(s => s.CostManagements)
                .WithOne(cm => cm.Show)
                .HasForeignKey(cm => cm.ShowId);

            // Content Table Relationships
            modelBuilder.Entity<Content>()
                .HasOne(c => c.Editor)
                .WithMany()
                .HasForeignKey(c => c.EditorId)
                .OnDelete(DeleteBehavior.Restrict); 
            // Advertisement Table Relationships
            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.AssignedShow)
                .WithMany(s => s.Advertisements)
                .HasForeignKey(a => a.AssignedShowId);

            // MediaLibrary Table Relationships
            modelBuilder.Entity<MediaLibrary>()
                .HasOne(m => m.Show)
                .WithMany(s => s.MediaLibraries)
                .HasForeignKey(m => m.ShowId);

            modelBuilder.Entity<MediaLibrary>()
                .HasOne(m => m.Uploader)
                .WithMany(u => u.MediaLibraries)
                .HasForeignKey(m => m.UploaderId);

            // Schedule Table Relationships
            modelBuilder.Entity<Schedule>()
                .HasOne(sc => sc.Show)
                .WithMany(s => s.Schedules)
                .HasForeignKey(sc => sc.ShowId);

            modelBuilder.Entity<Schedule>()
                .HasOne(sc => sc.AssignedEditor)
                .WithMany()
                .HasForeignKey(sc => sc.AssignedEditorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ViewerFeedback Table Relationships
            modelBuilder.Entity<ViewerFeedback>()
                .HasOne(vf => vf.Show)
                .WithMany(s => s.ViewerFeedbacks)
                .HasForeignKey(vf => vf.ShowId);

            // Talent Table Relationships
            modelBuilder.Entity<Talent>()
                .HasOne(t => t.Show)
                .WithMany(s => s.Talents)
                .HasForeignKey(t => t.ShowId);

            // CostManagement Table Relationships
            modelBuilder.Entity<CostManagement>()
                .HasOne(cm => cm.Show)
                .WithMany(s => s.CostManagements)
                .HasForeignKey(cm => cm.ShowId);

            // ComplianceRecord Table Relationships
            modelBuilder.Entity<ComplianceRecord>()
                .HasOne(cr => cr.Show)
                .WithMany(s => s.ComplianceRecords)
                .HasForeignKey(cr => cr.ShowId);

            // Notification Table Relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
        }
    }
}
