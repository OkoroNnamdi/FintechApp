using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Interfaces
{
    public interface ICurrencyAPI
    {
        Task<double> GetApiAsync(string currency);
    }
}
