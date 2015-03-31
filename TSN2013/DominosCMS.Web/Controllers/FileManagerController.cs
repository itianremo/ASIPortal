using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Web.Models;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using DominosCMS.Web.Helpers;
using System.Drawing;

namespace DominosCMS.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class FileManagerController : Controller
    {
        //
        // GET: /FileManager/

        public ActionResult Index()
        {
            string path = ((Request.Cookies["lastFolderVisited"] == null)) ? ConfigurationManager.AppSettings["filesdir"] : Request.Cookies["lastFolderVisited"].Value;


            FileManagerModel model = GetModel(path);

            HttpCookie c = new HttpCookie("lastFolderVisited");
            c.Value = path;

            Response.Cookies.Add(c);
            return View(model);
        }

        public ActionResult Folders()
        {
            FileManagerModel model = GetModel(ConfigurationManager.AppSettings["filesdir"]);

            return PartialView(model);
        }

        private FileManagerModel GetModel(string path)
        {
            FileManagerModel model = new FileManagerModel();

            model.RootFolder = ConfigurationManager.AppSettings["filesdir"];
            model.CurrentFolder = path;
            string[] files = Directory.GetFiles(Server.MapPath(model.CurrentFolder));

            IList<FileItem> items = new List<FileItem>();

            foreach (var f in files)
            {
                var item = new FileItem { FullPath = f };
                item.RelativePath = string.Format("{0}/{1}", model.CurrentFolder, item.Name);
                items.Add(item);

            }

            model.Files = items;
            IList<FolderItem> folders = new List<FolderItem>();
            string[] sfolders = Directory.GetDirectories(Server.MapPath(model.CurrentFolder));

            foreach (var d in sfolders)
            {
                var item = new FolderItem { FullPath = d };
                item.RelativePath = string.Format("{0}/{1}", model.CurrentFolder, item.Name);

                folders.Add(item);
            }

            model.Folders = folders;

            model.RootFolders = GetSubFolders(model.RootFolder);

            return model;


            //IList<FolderItem> roots = new List<FolderItem>();
            //string[] rootfolders = Directory.GetDirectories(Server.MapPath(model.RootFolder));

            //foreach (var d in rootfolders)
            //{
            //    var item = new FolderItem { FullPath = d };
            //    item.RelativePath = string.Format("{0}/{1}", model.RootFolder, item.Name);

            //    roots.Add(item);
            //}

        }

        private IList<FolderItem> GetSubFolders(string dir)
        {
            string[] folders = Directory.GetDirectories(Server.MapPath(dir));
            IList<FolderItem> dirs = new List<FolderItem>();

            foreach (var d in folders)
            {
                var item = new FolderItem { FullPath = d };
                item.RelativePath = string.Format("{0}/{1}", dir, item.Name);

                item.SubFolders = GetSubFolders(item.RelativePath);
                dirs.Add(item);
            }

            return dirs;
        }


        public ActionResult UploadFiles(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                dir = ConfigurationManager.AppSettings["filesdir"];
            ViewBag.UploadDir = dir;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        public ActionResult CreateFolder(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                dir = ConfigurationManager.AppSettings["filesdir"];
            ViewBag.UploadDir = dir;
            if (Request.IsAjaxRequest())
                return PartialView();
            else
                return View();
        }

        [HttpPost]
        public ActionResult CreateFolder(string dir, string foldername)
        {
            Directory.CreateDirectory(Server.MapPath(string.Format("{0}/{1}", dir, foldername)));
            return Content("SUCCESS");
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase filename, string dir)
        {
            StringBuilder sb = new StringBuilder();


            string filepath = string.Format("{0}/{1}", dir, Path.GetFileName(filename.FileName));

            try
            {
                filename.SaveAs(Server.MapPath(filepath));

                sb.Append("<div id=\"status\">success</div>");
                sb.Append("<div id=\"message\"> Successfully Uploaded</div>");
                //return the upload file
                sb.Append("<div id=\"uploadedfile\">" + Path.GetFileName(filename.FileName) + "</div>");
            }
            catch (Exception ex)
            {
                sb.Append("<div id=\"status\">failed</div>");
                sb.Append("<div id=\"message\">" + ex.ToString() + "</div>");
            }

            return Content(sb.ToString());
        }

        public ActionResult List(string path)
        {
            //Thread.Sleep(2000);

            FileManagerModel model = GetModel(path);

            return PartialView(model);
        }



        public ActionResult DeleteFile(string filename)
        {
            System.IO.File.Delete(Server.MapPath(filename));
            return Content("SUCCESS");
        }

        public ActionResult DeleteFolder(string filename)
        {
            Util.DeleteDirectory(Server.MapPath(filename));
            return Content("SUCCESS");
        }

        public ActionResult DownloadFile(string filename)
        {
            //Response.Headers.Clear();
            Response.AppendHeader("content-disposition", "attachment; filename=\"" + Path.GetFileName(filename) + "\"");
            return File(Server.MapPath(filename), Util.GetContentType(filename));

        }

        public ActionResult Thumb(string filename, int height = 90)
        {
            string url = filename;
            FileStream fs = new FileStream(Server.MapPath(url), FileMode.Open, FileAccess.Read, FileShare.Read);
            // create an image object, using the filename we just retrieved
            System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
            Bitmap img = new Bitmap(fs);
            //get the thumbnailwidth and height
            int h = height;
            float w = Math.Abs(((float)img.Width / (float)img.Height) * h);
            //if (w > 60)
            //    w = 220;
            // create the actual thumbnail image
            Image thumbnailImage = img.GetThumbnailImage((int)w, h, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
            //System.Drawing.Image thumbnailImage = image.GetThumbnailImage((int)w, h, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            img.Dispose();
            fs.Dispose();
            // make a memory stream to work with the image bytes
            MemoryStream imageStream = new MemoryStream();

            // put the image into the memory stream
            thumbnailImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);

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


            return null;
            //return File(imageContent, "image/jpeg");

            //return Content(url);
        }

        public bool ThumbnailCallback()
        {
            return true;
        }

    }
}
