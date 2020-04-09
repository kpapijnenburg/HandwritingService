﻿using HandwritingService.Domain;
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
                .HasForeignKey<Note>(n => n.HandwritingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Handwriting()
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                State = State.Pending,
                Image = GetImage(),
            });

        }

        public byte[] GetImage()
        {
            var image = Image.FromFile("./Assets/text.jpg");

            using var stream = new MemoryStream();

            image.Save(stream, image.RawFormat);
            return stream.ToArray();
        }
    }
}
