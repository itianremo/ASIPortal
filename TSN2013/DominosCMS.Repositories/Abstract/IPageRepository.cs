using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;

namespace DominosCMS.Repositories.Abstract
{
    public interface IPageRepository : IRepository<Page>
    {
        Page FindBy(string url);

        bool ValidatePageUrl(string url);
        bool ValidatePageUrl(string url, bool isUpdated);

        bool ValidatePageUrl(string url, string oldUrl);

        void RemovePage(int id);
    }
}
