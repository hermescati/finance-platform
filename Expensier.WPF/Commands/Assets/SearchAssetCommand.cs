using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.ViewModels.Modals;
using System.Threading.Tasks;


namespace Expensier.WPF.Commands.Assets
{
    public class SearchAssetCommand : AsyncCommandBase
    {
        private CryptoModalViewModel _assetModalViewModel;
        private readonly IAssetService _assetService;


        public SearchAssetCommand( CryptoModalViewModel cryptoModalViewModel, IAssetService cryptoService )
        {
            _assetModalViewModel = cryptoModalViewModel;
            _assetService = cryptoService;
        }


        public override async Task ExecuteAsync( object parameter )
        {
            _assetModalViewModel.ErrorMessage = string.Empty;

            try
            {
                Asset crypto = await _assetService.FetchCryptoAsset( _assetModalViewModel.CryptoSymbol );
                _assetModalViewModel.Crypto = crypto;
                _assetModalViewModel.ValidSymbol = true;
            }
            catch ( InvalidSymbolException )
            {
                _assetModalViewModel.ErrorMessage = _assetModalViewModel.CryptoSymbol + " does not belong to any asset!";
            }

        }
    }
}
