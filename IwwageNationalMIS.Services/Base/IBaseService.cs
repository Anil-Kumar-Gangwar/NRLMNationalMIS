using System.Collections.Generic;

namespace IwwageNationalMIS.Services.IServices
{
    public interface IBaseService
    {
        void Dispose();
        string GetPercentage(int outOfTotal, int Total);
    }
}
