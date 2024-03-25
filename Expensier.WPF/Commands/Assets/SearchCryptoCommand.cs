using Expensier.Domain.Exceptions;
using Expensier.Domain.Models;
using Expensier.Domain.Services;
using Expensier.WPF.ViewModels;
using Expensier.WPF.ViewModels.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Commands.Assets
{
    public class SearchCryptoCommand : AsyncCommandBase
    {
        private CryptoModalViewModel _cryptoModalViewModel;
        private readonly ICryptoService _cryptoService;

        public SearchCryptoCommand(CryptoModalViewModel cryptoModalViewModel, ICryptoService cryptoService)
        {
            _cryptoModalViewModel = cryptoModalViewModel;
            _cryptoService = cryptoService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _cryptoModalViewModel.ErrorMessage = string.Empty;

            try
            {
                Asset crypto = await _cryptoService.GetCrypto(_cryptoModalViewModel.CryptoSymbol + "USD");
                _cryptoModalViewModel.Crypto = crypto;
                _cryptoModalViewModel.ValidSymbol = true;
            }
            catch (InvalidSymbolException)
            {
                _cryptoModalViewModel.ErrorMessage = _cryptoModalViewModel.CryptoSymbol + " does not belong to any asset!";
            }

        }
    }
}
