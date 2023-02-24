using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.API.Models
{
    public class APIKey
    {
        public string Key { get; }

        public APIKey(string key)
        {
            Key = key;
        }
    }
}
