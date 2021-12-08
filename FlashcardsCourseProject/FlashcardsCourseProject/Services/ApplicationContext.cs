using FlashcardsCourseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsCourseProject.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CardSet> CardSet { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<FileImage> FileImage { get; set; }
        private readonly string _databasePath;

        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
