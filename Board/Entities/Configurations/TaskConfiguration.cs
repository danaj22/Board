using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Board.Entities.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.Property(x => x.Activity)
            .HasMaxLength(200);

            builder
                .Property(x => x.RemaningWork)
                .HasPrecision(14, 2);
        }
    }
}
