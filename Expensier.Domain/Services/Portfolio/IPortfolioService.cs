using Expensier.Domain.Models;


namespace Expensier.Domain.Services.Portfolio
{
    public interface IPortfolioService
    {
        double FindTotalValue( IEnumerable<AssetTransaction> assets );

        double FindInitialInvestment( IEnumerable<AssetTransaction> assets );

        Task<double> FindROI( IEnumerable<AssetTransaction> assets, double value );

        //Task<Dictionary<string, double>> GetPorfolioDetails( Account currentAccount );

        //Task<Account> StoreReturns( Account currentAccount );
    }
}
