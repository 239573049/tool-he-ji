using System;
using System.Threading.Tasks;
using Token.Application.Contracts;
using Token.Inject.tag;

namespace Token.Application
{
    public class DemoAppService:IDemoAppService,ITransientTag
    {
        public async Task<string> GetDemoDataAsync()
        {
            string date="asdasd";
            return await Task.FromResult(date);
        }
    }
}