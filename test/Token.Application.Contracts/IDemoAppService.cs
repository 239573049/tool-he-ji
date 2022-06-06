using System;
using System.Threading.Tasks;

namespace Token.Application.Contracts
{
    public interface IDemoAppService
    {
        Task<string> GetDemoDataAsync();
    }
}