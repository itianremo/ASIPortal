using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;
using DominosCMS.Repositories.Abstract;

namespace DominosCMS.Repositories
{
    public class ConfigurationRepository : RepositoryBase<Config>, IConfigurationRepository
    {

        private CMSDbContext context;

        public ConfigurationRepository(CMSDbContext context)
            : base(context)
        {
            this.context = context;
        }
        public string this[string key]
        {
            get
            {
                var config = this.List.Where(c => c.Key == key).SingleOrDefault();

                return (config == null) ? null : config.Value;
            }
            set
            {
                var config = this.List.Where(c => c.Key == key).SingleOrDefault();

                if (config != null)
                {
                    config.Value = value;
                    Update(config);
                }
                else
                {
                    config = new Config() { Key = key, Value = value };

                    Add(config);
                }
            }
        }
    }
}
