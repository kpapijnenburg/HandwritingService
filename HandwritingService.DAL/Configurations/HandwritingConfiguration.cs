using HandwritingService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace HandwritingService.DAL.Configurations
{
    class HandwritingConfiguration : IEntityTypeConfiguration<Handwriting>
    {
        public void Configure(EntityTypeBuilder<Handwriting> builder)
        {
            builder
                .HasOne(h => h.Note)
                .WithOne(n => n.Handwriting)
                .HasForeignKey<Note>(n => n.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Handwriting()
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Image = GetImage(),
                NoteId = 1,
            }); ;

        }

        public byte[] GetImage()
        {
            var image = Image.FromFile("./Assets/text.jpeg");

            using var stream = new MemoryStream();

            image.Save(stream, image.RawFormat);
            return stream.ToArray();

        }
    }
}
