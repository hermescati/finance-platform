﻿using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly ExpensierDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccountDataService(ExpensierDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<Account> Update(Guid id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Account> GetByID(Guid id)
        {
            using (ExpensierDbContext context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Accounts
                    .Include(holder => holder.AccountHolder)
                    .Include(transaction => transaction.TransactionList)
                    .Include(subscription => subscription.SubscriptionList)
                    .Include(crypto => crypto.CryptoAssetList)
                    .Include(returns => returns.PortfolioReturn)
                    .FirstOrDefaultAsync((entity) => entity.ID == id);

                return entity;
            }
        }

        public async Task<Account> GetByEmail(string email)
        {
            using (ExpensierDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts
                    .Include(holder => holder.AccountHolder)
                    .Include(transaction => transaction.TransactionList)
                    .Include(subscription => subscription.SubscriptionList)
                    .Include(crypto => crypto.CryptoAssetList)
                    .Include(returns => returns.PortfolioReturn)
                    .FirstOrDefaultAsync((entity) => entity.AccountHolder.Email == email);
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (ExpensierDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts
                    .Include(holder => holder.AccountHolder)
                    .Include(transaction => transaction.TransactionList)
                    .Include(subscription => subscription.SubscriptionList)
                    .Include(crypto => crypto.CryptoAssetList)
                    .Include(returns => returns.PortfolioReturn)
                    .ToListAsync();

                return entities;
            }
        }
    }
}
