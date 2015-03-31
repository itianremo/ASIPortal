using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Drawing;
using DominosCMS.Repositories.Abstract;
using DominosCMS.Repositories;
using DominosCMS.Models;
using System.Text;
using System.Drawing.Drawing2D;
using DominosCMS.Web.Helpers;

namespace DominosCMS.Web.Controllers
{
    public class GalleryController : BaseController
    {
        private IGalleryRepository repository;

        public GalleryController()
        {
            repository = DB.GalleryRepository;
        }

        public ActionResult Index()
        {
            return View(repository.FindAllAlbums());
        }

        [HttpPost]
        public ActionResult UploadPhotos(HttpPostedFileBase filename)
        {
            string filepath = string.Format("/Content/gallery/{0}", Path.GetFileName(filename.FileName));
            StringBuilder sb = new StringBuilder();

            try {
                filename.SaveAs(Server.MapPath(filepath));

                sb.Append("<div id=\"status\">success</div>");
                sb.Append("<div id=\"message\"> Successfully Uploaded</div>");
                //sb.Append("<div id=\"message\"> Successfully Uploaded</div>");
                //return the upload file
                sb.Append(string.Format("<div id=\"uploadedfile\"><img src='{0}' /></div>", filepath));
            }
            catch(Exception ex)
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
            catch (Exception)
            {
                result = "Error occured while uploading this file!";
            }

            return Content(result);
        }
        
        public ActionResult ShowAlbum(int id, int index = 0)
        {
            var model = repository.FindAlbumById(id);
            if (model.Photos == null || model.Photos.Count == 0)
            {
                ViewBag.AlbumID = id;
                return View("NoPhotos");
            }

            ViewBag.StartIndex = index;

            return View(model);
        }

        public ActionResult ShowAlbum2(int id, int index = 0)
        {
            var model = repository.FindAlbumById(id);
            if (model.Photos == null || model.Photos.Count == 0)
            {
                ViewBag.AlbumID = id;
                return View("NoPhotos");
            }

            ViewBag.StartIndex = index;

            return View(model);
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


        public ActionResult Album(int id, int index = 0)
        {
            var model = repository.FindAlbumById(id);
            if (model.Photos == null || model.Photos.Count == 0)
            {
                ViewBag.AlbumID = id;
                return View("NoPhotos");
            }

            ViewBag.StartIndex = index;

            return View(model);
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
