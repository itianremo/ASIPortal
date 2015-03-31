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
    public class AccountsRepository : RepositoryBase<Account>, IAccountsRepository
    {
        private CMSDbContext context;

        public AccountsRepository(CMSDbContext context)
            : base(context)
        {
            this.context = context;
        }


        public int[] GetUserRuleID(string username)
        {
            var result = context.Database.SqlQuery<int>("EXEC GetUserRuleID @UserName", new SqlParameter("@UserName", username));
            return result.ToArray<int>();
        }

        public Dictionary<int, string> GetAllRules()
        {
            var list = new Dictionary<int, string>();
            
            var result = context.Database.SqlQuery<Rule1>("EXEC GetAllRules");
            foreach (var item in result)
            {
                list.Add(item.Key, item.Value);
            }

            return list;
            
        }

        private class Rule1
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }


    }
}
