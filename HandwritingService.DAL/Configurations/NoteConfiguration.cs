using HandwritingService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandwritingService.DAL.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder
                .HasOne(n => n.Handwriting)
                .WithOne(h => h.Note)
                .HasForeignKey<Handwriting>(h => h.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Note()
            {
                Id = 1,
                HandwritingId = 1
            });
        }
    }
}
