using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services.Portfolio
{
    public interface IPortfolioService
    {
        Task<double> GetPortfolioReturn(Account currentAccount);

        public double GetPortfolioValue(Account currentAccount);

        public double GetCryptoWeight(Account currentAccount, AssetTransaction currentCrypto);

        Task<Account> StoreReturns(Account currentAccount);
    }
}
