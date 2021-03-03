using System.Collections.Generic;

namespace NRLMNationalMIS.Services.IServices
{
    public interface IBaseService
    {
        void Dispose();
        string GetPercentage(int outOfTotal, int Total);
    }
}
