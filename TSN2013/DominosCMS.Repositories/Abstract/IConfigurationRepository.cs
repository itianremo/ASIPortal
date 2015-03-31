using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;

namespace DominosCMS.Repositories.Abstract
{
    public interface IConfigurationRepository : IRepository<Config>
    {
        string this[string key] { get; set; }
    }
}
