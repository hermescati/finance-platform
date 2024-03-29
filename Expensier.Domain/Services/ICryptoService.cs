﻿using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services
{
    public interface ICryptoService
    {
        Task<Asset> GetCrypto(string symbol);

        Task<IEnumerable<PriceData>> GetHistoricalPrices(string symbol);

        public double GetMarketValue(double? price, double coins);

        Task<double> GetCryptoReturns(AssetTransaction currentCrypto);

        Task<Account> AddCrypto(
            Account currentAccount,
            Asset currentCrypto,
            double purchasePrice,
            double amount);

        Task<Account> DeleteCrypto(
            Account currentAccount,
            Guid cryptoID);
    }
}
