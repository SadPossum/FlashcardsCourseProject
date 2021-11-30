using FlashcardsCourseProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsCourseProject.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<Card> Cards { get; set; }
        private string _databasePath;

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
