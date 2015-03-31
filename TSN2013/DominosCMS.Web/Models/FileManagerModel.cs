using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace DominosCMS.Web.Models
{
    public class FileManagerModel
    {
        public string RootFolder { get; set; }
        public string CurrentFolder { get; set; }
        public IList<FileItem> Files { get; set; }
        public IList<FolderItem> Folders { get; set; }
        public IList<FolderItem> RootFolders { get; set; }

    }

    public class FileItem 
    {
        public string Name
        {
            get
            {
                return Path.GetFileName(FullPath);
            }

        }
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
        public string Extension
        {
            get
            {
                return Path.GetExtension(FullPath).Remove(0, 1);
            }

        }

    }

    public class FolderItem
    {

        public FolderItem()
        {
            SubFolders = new List<FolderItem>();
        }
        
        public string FullPath { get; set; }
        public string Name
        {
            get
            {
                return getDirectoryName(FullPath);
            }

        }

        public string RelativePath { get; set; }

        private string getDirectoryName(string path)
        {
            string[] folders = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            return folders[folders.Length - 1];

        }

        public IList<FolderItem> SubFolders { get; set; }

    }
}