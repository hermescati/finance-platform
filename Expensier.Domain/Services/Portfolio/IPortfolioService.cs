using Expensier.Domain.Models;


namespace Expensier.Domain.Services.Portfolio
{
    public interface IPortfolioService
    {
        double FindTotalValue( IEnumerable<AssetTransaction> assets );

        double FindInitialInvestment( IEnumerable<AssetTransaction> assets );

        //Task<Account> StoreReturns( Account currentAccount );
    }
}
