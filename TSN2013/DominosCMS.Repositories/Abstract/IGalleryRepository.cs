using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;

namespace DominosCMS.Repositories.Abstract
{
    public interface IGalleryRepository : IRepository<Photo>
    {
        void CreateAlbum(PhotoAlbum album);
        void UpdateAlbum(PhotoAlbum album);
        void DeleteAlbum(PhotoAlbum id);
        PhotoAlbum FindAlbumById(int id);
        IList<PhotoAlbum> FindAllAlbums();

        PhotoAlbum FindRandomAlbum();
    }
}
