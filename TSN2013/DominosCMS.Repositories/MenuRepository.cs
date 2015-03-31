using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;
using DominosCMS.Repositories.Abstract;

namespace DominosCMS.Repositories
{
    public class MenuRepository : RepositoryBase<MenuItem>, IMenuRepository
    {
        private CMSDbContext context;

        public MenuRepository(CMSDbContext context)
            : base(context)
        {
            this.context = context;
        }

        #region IMenuRepository Members

        public IList<MenuItem> FindRootItems()
        {
            throw new NotImplementedException();
        } 

        public void DeleteMenuItem(int id)
        {
            //throw new NotImplementedException(); 
            var item = this.FindBy(id);
            context.SiteMenu.Remove(item);
        }


        #endregion


    }
}
