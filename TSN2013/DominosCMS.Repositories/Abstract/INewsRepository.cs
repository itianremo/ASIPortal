using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DominosCMS.Models;
using DbCore;

namespace DominosCMS.Repositories.Abstract
{
    public interface INewsRepository : IRepository<NewsItem>
    {
        NewsItem FindRandom();
    }
}
