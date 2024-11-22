﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Entities.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder
                .Property(x => x.Value)
                .HasMaxLength(60)
                .IsRequired();

            builder.HasData(
                new State { Id = 1, Value = "To Do" },
                new State { Id = 2, Value = "Doing" },
                new State { Id = 3, Value = "Done" });
        }
    }
}
