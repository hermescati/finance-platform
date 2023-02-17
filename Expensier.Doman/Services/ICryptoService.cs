using Expensier.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.Domain.Services
{
    public interface ICryptoService
    {
        Task<Crypto> GetCrypto(string symbol); 
    }
}
