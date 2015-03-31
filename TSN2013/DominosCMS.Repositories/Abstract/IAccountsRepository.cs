using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using DominosCMS.Models;

namespace DominosCMS.Repositories.Abstract
{
    public interface IAccountsRepository : IRepository<Account>
    {
        int[] GetUserRuleID(string username);
        Dictionary<int, string> GetAllRules();

    }
}
