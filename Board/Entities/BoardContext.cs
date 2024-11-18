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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(entityBuilder =>
            {
                entityBuilder.Property(x => x.State).IsRequired();
                entityBuilder.Property(x => x.Area).HasColumnType("varchar(200)");
                entityBuilder.Property(x => x.IterationPath).HasColumnName("Iteration_Path");
                entityBuilder.Property(x => x.Efford).HasColumnType("decimal(5,2)");
                entityBuilder.Property(x => x.Activity).HasMaxLength(200);
                entityBuilder.Property(x => x.RemaningWork).HasPrecision(14, 2);
                entityBuilder.Property(x => x.EndDate).HasPrecision(3);

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


            });

            modelBuilder.Entity<Comment>(entityBuilder =>
            {
                entityBuilder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                entityBuilder.Property(x => x.ModifiedDate).ValueGeneratedOnUpdate();

            });

            modelBuilder.Entity<User>()
                .HasOne(x => x.Address)
                .WithOne(x => x.User)
                .HasForeignKey<Address>(x => x.UserId);

            modelBuilder.Entity<WorkItem>();

        }
    }

}
