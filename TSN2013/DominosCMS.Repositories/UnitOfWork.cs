using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DominosCMS.Repositories.Abstract;
using DbCore;
using DominosCMS.Models;

namespace DominosCMS.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private CMSDbContext context = null;
        
        public  CMSDbContext Context 
        {
            get 
            {
                return context;
            }
        
        }

        private IGalleryRepository galleryRepository = null;
        private IPageRepository pageRepository = null;
        private IMenuRepository menuRepository = null;
        private RepositoryBase<Banner> bannerRepository = null;
        private INewsRepository newsRepository = null;
        private RepositoryBase<Inquiry> inquiryRepository = null;
        private IAccountsRepository accountsRepository = null;
        private RepositoryBase<Group> groupsRepository = null;
        private RepositoryBase<Download> downloadsRepository = null;
        private RepositoryBase<Contact> contactsRepository = null;
        private RepositoryBase<OptOutRequest> optOutRepository = null;
        private IConfigurationRepository configRepository = null;

        private bool disposed = false;

        public UnitOfWork()
        {
            this.context = new CMSDbContext();
        }

        public IGalleryRepository GalleryRepository
        {
            get {

                return (galleryRepository ?? new GalleryRepository(context));
            }
        }

        public IPageRepository PageRepository
        {
            get
            {

                return (pageRepository ?? new PageRepository(context));
            }
        }
        public IMenuRepository MenuRepository
        {
            get
            {

                return (menuRepository ?? new MenuRepository(context));
            }
        }

        public INewsRepository NewsRepository
        {
            get
            {

                return (newsRepository ?? new NewsRepository(context));
            }
        }


        public RepositoryBase<Download> DownloadsRepository
        {
            get
            {

                return (downloadsRepository ?? new RepositoryBase<Download>(context));
            }
        }

        public RepositoryBase<Group> GroupsRepository
        {
            get
            {

                return (groupsRepository ?? new RepositoryBase<Group>(context));
            }
        }
        
        public RepositoryBase<OptOutRequest> OptOutRepository
        {
            get
            {

                return (optOutRepository ?? new RepositoryBase<OptOutRequest>(context));
            }
        }

        
        public RepositoryBase<Contact> ContactsRepository
        {
            get
            {

                return (contactsRepository ?? new RepositoryBase<Contact>(context));
            }
        }
        public RepositoryBase<Banner> BannerRepository
        {
            get
            {

                return (bannerRepository ?? new RepositoryBase<Banner>(context));
            }
        }

        public RepositoryBase<Inquiry> InquiryRepository
        {
            get
            {

                return (inquiryRepository ?? new RepositoryBase<Inquiry>(context));
            }
        }

        public IAccountsRepository AccountsRepository
        {
            get
            {

                return (accountsRepository ?? new AccountsRepository(context));
            }
        }

        public IConfigurationRepository ConfigRepository
        {
            get
            {
                return (configRepository ?? new ConfigurationRepository(context));
            }
        }
       

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        
        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public void Refresh(System.Data.Entity.Core.Objects.RefreshMode refreshMode,  object entity)
        {
            var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
            objectContext.Refresh(refreshMode, entity);
        }


    }
}
