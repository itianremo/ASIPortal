using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;
using DominosCMS.Repositories;
using DominosCMS.Repositories.Abstract;

namespace DominosCMS.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        private CMSDbContext context;

        public PageRepository(CMSDbContext context)
            : base(context)
        {
            this.context = context;
        }

        #region IPageRepository Members

        public Page FindBy(string url)
        {
            return context.Pages.Where(p => p.Url == url).SingleOrDefault();
        }

        public bool ValidatePageUrl(string url)
        {
            int count = context.Pages.Count(p => p.Url == url);

            return (count == 0);
        }

        public bool ValidatePageUrl(string url, string oldUrl)
        {
            int count = context.Pages.Count(p => p.Url == url && p.Url != oldUrl);

            return (count == 0);
        }
        public bool ValidatePageUrl(string url, bool isUpdated)
        {
            throw new NotImplementedException();
        }
        public void RemovePage(int id)
        {
            context.Pages.Remove(context.Pages.Find(id));
        }

       

        #endregion





    }
}
