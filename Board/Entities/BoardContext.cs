using Board.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Board.Entities
{
    public class BoardContext : DbContext
    {
        public BoardContext(DbContextOptions<BoardContext> options) : base(options) { }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<WorkItemTag> WorkItemTag { get; set; }
        public DbSet<TopAuthor> ViewTopAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(entityBuilder =>
            {
                entityBuilder.Property(x => x.Area).HasColumnType("varchar(200)");
                entityBuilder.Property(x => x.IterationPath).HasColumnName("Iteration_Path");

                entityBuilder.Property(x => x.Priority).HasDefaultValue(1);

                entityBuilder.HasMany(x => x.Comments)
                    .WithOne(x => x.WorkItem)
                    .HasForeignKey(x => x.WorkItemId);

                entityBuilder.HasOne(x => x.Author)
                    .WithMany(x => x.WorkItems)
                    .HasForeignKey(x => x.AuthorId);

                entityBuilder.HasMany(x => x.Tags)
                    .WithMany(x => x.WorkItems)
                    .UsingEntity<WorkItemTag>(
                        w => w.HasOne(x => x.Tag)
                        .WithMany()
                        .HasForeignKey(x => x.TagId),

                        w => w.HasOne(x => x.WorkItem)
                        .WithMany()
                        .HasForeignKey(x => x.WorkItemId),

                        w =>
                        {
                            w.HasKey(x => new { x.TagId, x.WorkItemId });
                            w.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");

                        });

                entityBuilder.HasOne(x => x.State)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.StateId);
            });

            modelBuilder.Entity<Epic>()
                .Property(x => x.EndDate)
                .HasPrecision(3);
            

            modelBuilder.Entity<Issue>()
                .Property(x => x.Efford)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Task>()
                .Property(x => x.Activity)
                .HasMaxLength(200);
                
            modelBuilder.Entity<Task>()
                .Property(x => x.RemaningWork)
                .HasPrecision(14, 2);
            

            modelBuilder.Entity<Comment>(entityBuilder =>
            {
                entityBuilder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                entityBuilder.Property(x => x.ModifiedDate).ValueGeneratedOnUpdate();
                entityBuilder.HasOne(x => x.Author)
                    .WithMany(x => x.Comments)
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.ClientCascade);
            });

            modelBuilder.Entity<User>()
                .HasOne(x => x.Address)
                .WithOne(x => x.User)
                .HasForeignKey<Address>(x => x.UserId);

            modelBuilder.Entity<State>()
                .Property(x => x.Value)
                .HasMaxLength(60)
                .IsRequired();

            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Value = "To Do" }, 
                new State { Id = 2, Value = "Doing" }, 
                new State { Id = 3, Value = "Done" });

            modelBuilder.Entity<TopAuthor>(eb =>
            {
                eb.ToView("View_TopAuthors");
                eb.HasNoKey();
            });
        }
    }

}
