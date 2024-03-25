using Expensier.Domain.Models;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.State.Crypto
{
    public class CryptoStore
    {
        private readonly AccountStore _accountStore;

        public IEnumerable<AssetTransaction> CryptoAssetList => _accountStore.CurrentAccount?.AssetList ?? new List<AssetTransaction>();

        public event Action StateChanged;

        public CryptoStore(AccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
