using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DominosCMS.Repositories.Abstract;
using DominosCMS.Models;
using DbCore;

namespace DominosCMS.Repositories
{
    public class GalleryRepository : RepositoryBase<Photo>, IGalleryRepository
    {
        private CMSDbContext context;

        public GalleryRepository(CMSDbContext context)
            : base(context)
        {
            this.context = context;
        }

        #region IGalleryRepository Members

        public void CreateAlbum(PhotoAlbum album)
        {
            context.Albums.Add(album);
        }

        public void UpdateAlbum(PhotoAlbum album)
        {
            var original = FindAlbumById(album.ID);

            context.Entry(original).CurrentValues.SetValues(album);
        }

        public void DeleteAlbum(PhotoAlbum album)
        {
            if (context.Entry(album).State == System.Data.Entity.EntityState.Detached)
            {
                context.Albums.Attach(album);
            }

            foreach (var photo in album.Photos.ToList())
            {
                context.Photos.Remove(photo);
            }
            context.Albums.Remove(album);
        }

        public PhotoAlbum FindAlbumById(int id)
        {
            return context.Albums.Include("Photos").Where(a => a.ID == id).SingleOrDefault();
        }

        public IList<PhotoAlbum> FindAllAlbums()
        {
            return context.Albums.OrderByDescending(album => album.ID).ToList();
        }

        public PhotoAlbum FindRandomAlbum()
        {
            return context.Albums.Where(r => r.Visible).OrderBy(r => Guid.NewGuid()).Take(1).Single();
        }


        #endregion
    }
}
