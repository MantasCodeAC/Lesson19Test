using Lesson19Test.DataBase.Model.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ImageAndNote> ImageAndNote { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageAndNote>().HasKey(x => new
            {
                x.NoteId,
                x.ImageId
            });
            modelBuilder.Entity<Note>().HasMany(x => x.ImageAndNote).WithOne(x => x.Note).HasForeignKey(x => x.NoteId);
            modelBuilder.Entity<Image>().HasMany(x => x.ImageAndNote).WithOne(x => x.Image).HasForeignKey(x => x.ImageId);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
