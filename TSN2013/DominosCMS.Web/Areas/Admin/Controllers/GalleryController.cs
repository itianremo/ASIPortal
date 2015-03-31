using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Repositories.Abstract;
using DominosCMS.Web.Helpers;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class GalleryController : AdminController<PhotoAlbum>
    {
        private IGalleryRepository repository = null;
        
        public GalleryController() : base() {
            repository = DB.GalleryRepository;
        }

        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            return View(repository.FindAllAlbums());
        }

        [HttpPost]
        public ActionResult UploadPhotos(HttpPostedFileBase filename)
        {
            string filepath = string.Format("/Content/gallery/{0}", Path.GetFileName(filename.FileName));
            StringBuilder sb = new StringBuilder();

            try
            {
                filename.SaveAs(Server.MapPath(filepath));

                sb.Append("<div id=\"status\">success</div>");
                sb.Append("<div id=\"message\"> Successfully Uploaded</div>");
                //sb.Append("<div id=\"message\"> Successfully Uploaded</div>");
                //return the upload file
                sb.Append(string.Format("<div id=\"uploadedfile\"><img src='{0}' /></div>", filepath));
            }
            catch (Exception ex)
            {
                sb.Append("<div id=\"status\">failed</div>");
                sb.Append("<div id=\"message\">" + ex.ToString() + "</div>");
            }

            return Content(sb.ToString());
        }

        [HttpPost]
        public ActionResult UploadPhotos2(HttpPostedFileBase Filedata)
        {

            string filepath = string.Format("{0}/{1}{2}", ConfigurationManager.AppSettings["gallery"], Guid.NewGuid(), Path.GetExtension(Filedata.FileName));
            string result = "";

            try
            {
                Filedata.SaveAs(Server.MapPath(filepath));

                //resize photo
                result = filepath;
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(filepath)))
                {

                    if (image.Width > 800)
                    {
                        int h = 600;
                        float w = Math.Abs(((float)image.Width / (float)image.Height) * h);

                        //image.Save(Server.MapPath(filepath));


                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error occured while uploading this file! " + ex.Message;
            }

            return Content(result);
        }


        public ActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAlbum(PhotoAlbum album, string[] photourl, string[] photodec)
        {
            if (ModelState.IsValid)
            {
                album.CreationDate = DateTime.Now;
                album.Photos = new List<Photo>();


                for (int i = 0; i < photourl.Length; i++)
                {
                    album.Photos.Add(new Photo { Filename = Path.GetFileName(photourl[i]), Description = photodec[i], Visible = true, Title = "" });
                }



                repository.CreateAlbum(album);

                repository.Save();

                //Create folder for the album using album id
                string album_folder = string.Format("{0}/{1}", ConfigurationManager.AppSettings["gallery"], album.ID);
                Directory.CreateDirectory(Server.MapPath(album_folder));

                //move the photos to this folder 
                foreach (var photo in album.Photos)
                {
                    string source = string.Format("{0}/{1}", ConfigurationManager.AppSettings["gallery"], photo.Filename);
                    string destination = string.Format("{0}/{1}", album_folder, photo.Filename);

                    System.IO.File.Move(Server.MapPath(source), Server.MapPath(destination));
                }

                cleanUp();

                return RedirectToAction("Index");
            }

            return View(album);
        }

        public ActionResult EditAlbum(int? id)
        {
            if (id == null) return null;
            
            return View(repository.FindAlbumById(id.Value));
        }

        [HttpPost]
        public ActionResult EditAlbum(PhotoAlbum album, int[] photoID, string[] photourl, string[] photodec)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateAlbum(album);

                repository.Save();

                var album2 = repository.FindAlbumById(album.ID);

                for (int i = 0; i < photoID.Length; i++)
                {
                    if (photoID[i] == 0)
                    {
                        //add new photo
                        album2.Photos.Add(new Photo { Description = photodec[i], Title = "", Visible = true, Filename = Path.GetFileName(photourl[i]) });
                        //move the photo to the album folder
                        string filename = Path.GetFileName(photourl[i]);
                        string album_folder = string.Format("{0}/{1}", ConfigurationManager.AppSettings["gallery"], album.ID);
                        string source = string.Format("{0}/{1}", ConfigurationManager.AppSettings["gallery"], filename);
                        string destination = string.Format("{0}/{1}", album_folder, filename);

                        System.IO.File.Move(Server.MapPath(source), Server.MapPath(destination));
                    }
                    else
                    {
                        //update photo
                        var photo = album2.Photos.Where(p => p.ID == photoID[i]).Single();
                        photo.Description = photodec[i];
                    }
                }

                repository.UpdateAlbum(album2);

                repository.Save();

                cleanUp();
                return RedirectToAction("Index");
            }


            return View(album);
        }


        public ActionResult DeletePhoto(int id = 0, string filename = "")
        {
            if (id == 0)
            {
                if (string.IsNullOrEmpty(filename))
                    return null;
                else
                {
                    string path = Server.MapPath(filename);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);

                        return Content("Success");
                    }

                    return null;
                }
            }
            else
            {
                Photo photo = repository.FindBy(id);


                //remove the photo file
                string path = string.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["gallery"], photo.Album.ID, photo.Filename);
                path = Server.MapPath(path);

                System.IO.File.Delete(path);
                //remove from the database
                repository.Remove(photo);

                repository.Save();

                return Content("Success");

            }
        }

        public ActionResult DeleteAlbum(int id)
        {
            //remove the album folder 

            string foldername = string.Format("{0}/{1}", ConfigurationManager.AppSettings["gallery"], id);
            string folderpath = Server.MapPath(foldername);
            if (Directory.Exists(folderpath))
                Util.DeleteDirectory(folderpath);


            repository.DeleteAlbum(repository.FindAlbumById(id));

            DB.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Thumb(string filename, int album = 0, int height = 90)
        {

            string url = string.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["gallery"], album, filename);
            if (album == 0)
            {
                url = string.Format("{0}/{1}", ConfigurationManager.AppSettings["gallery"], Path.GetFileName(filename));
            }


            FileStream fs = new FileStream(Server.MapPath(url), FileMode.Open, FileAccess.Read, FileShare.Read);
            // create an image object, using the filename we just retrieved
            System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
            Bitmap img = new Bitmap(fs);
            //get the thumbnailwidth and height
            int h = height;
            float w = Math.Abs(((float)h / (float)img.Height) * (float)img.Width);

            Bitmap newImage = new Bitmap((int)w, h);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(img, new Rectangle(0, 0, (int)w, h));
            }

            img.Dispose();
            image.Dispose();
            fs.Close();

            // make a memory stream to work with the image bytes
            MemoryStream imageStream = new MemoryStream();

            // put the image into the memory stream
            newImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            // make byte array the same size as the image
            byte[] imageContent = new Byte[imageStream.Length];

            // rewind the memory stream
            imageStream.Position = 0;

            // load the byte array with the image
            imageStream.Read(imageContent, 0, (int)imageStream.Length);

            // return byte array to caller with image type
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(imageContent);
            imageStream.Close();
            imageStream.Dispose();

            return null;
            //return File(imageContent, "image/jpeg");

            //return Content(url);


        }

        public bool ThumbnailCallback()
        {
            return true;
        }


        private void cleanUp()
        {
            string folder = Server.MapPath(ConfigurationManager.AppSettings["gallery"]);

            string[] files = Directory.GetFiles(folder);

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                DateTime creationTime = info.CreationTime;

                TimeSpan period = DateTime.Now - creationTime;

                if (period.Hours > 6)
                {
                    System.IO.File.Delete(file);
                }
            }
        }
    }
}
