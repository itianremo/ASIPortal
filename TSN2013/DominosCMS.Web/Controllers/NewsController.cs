using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Repositories;
using System.Configuration;
using System.IO;
using System.Text;
using DbCore;
using DominosCMS.Repositories.Abstract;

namespace DominosCMS.Web.Controllers
{ 
    public class NewsController : BaseController
    {
        private readonly INewsRepository newsRepository;

        public NewsController()
        {
            this.newsRepository = DB.NewsRepository;
        }

        //
        // GET: /News/

        public ViewResult Index()
        {
            return View(newsRepository.List.Where(n=>n.ShowInHome).OrderByDescending(n => n.LastUpdate).ToList());
        }

        //
        // GET: /News/Details/5

        public ViewResult Details(int id)
        {
            NewsItem newsitem = newsRepository.FindBy(id);
            return View(newsitem);
        }
    }
}