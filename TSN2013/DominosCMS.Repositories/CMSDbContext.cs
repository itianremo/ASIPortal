using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using DominosCMS.Models;

namespace DominosCMS.Repositories
{
    public class CMSDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<MenuItem> SiteMenu { get; set; }
        public DbSet<PhotoAlbum> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<NewsItem> News { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Config> Configurations { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<OptOutRequest> OptOutRequests { get; set; }
    }
}
