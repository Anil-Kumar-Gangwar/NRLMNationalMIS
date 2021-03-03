using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IwwageNationalMIS.Model;

namespace IwwageNationalMIS.Services.IServices
{
    public interface IMenuService
    {
        List<UserMenu> GetMenuByUser(int userId);
    }
}
