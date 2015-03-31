using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DominosCMS.Repositories.Abstract;
using DbCore;
using DominosCMS.Models;

namespace DominosCMS.Repositories
{
    public class NewsRepository : RepositoryBase<NewsItem>, INewsRepository
    {
        private CMSDbContext context;

        public NewsRepository(CMSDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public NewsItem FindRandom()
        {
            var news = context.News.Where(c => c.ShowInHome);
            int count = news.Count();
            if (count == 0) return null;
            else if (count == 1) return news.Single();

            var rndm = new Random().Next(count);

            return news.OrderBy(c => c.ID).Skip(rndm).FirstOrDefault();
        }


    }
}
