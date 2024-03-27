using Expensier.Domain.Models;
using Expensier.WPF.State.Accounts;
using System;
using System.Collections.Generic;


namespace Expensier.WPF.State.Assets
{
    public class AssetStore
    {
        private readonly AccountStore _accountStore;

        public IEnumerable<AssetTransaction> AssetsList => _accountStore.CurrentAccount?.AssetList ?? new List<AssetTransaction>();

        public event Action StateChanged;

        public AssetStore(AccountStore accountStore)
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
