using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Entities.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
                builder.Property(x => x.Area).HasColumnType("varchar(200)");
                builder.Property(x => x.IterationPath).HasColumnName("Iteration_Path");

                builder.Property(x => x.Priority).HasDefaultValue(1);

                builder.HasMany(x => x.Comments)
                    .WithOne(x => x.WorkItem)
                    .HasForeignKey(x => x.WorkItemId);

                builder.HasOne(x => x.Author)
                    .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.AuthorId);

                builder.HasMany(x => x.Tags)
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

                builder.HasOne(x => x.State)
                .WithMany(x => x.WorkItems)
                .HasForeignKey(x => x.StateId);
            
        }
    }
}
