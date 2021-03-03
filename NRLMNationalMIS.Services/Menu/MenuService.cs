using NRLMNationalMIS.Services.IServices;
using DTOModel = NRLMNationalMIS.Data.Model;
using NRLMNationalMIS.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using NRLMNationalMIS.Data.Repositories.IRepositories;
using AutoMapper;
using System.Text.RegularExpressions;

namespace NRLMNationalMIS.Services
{
    public class MenuService : BaseService, IMenuService
    {
        private IGenericRepository genericRepository;
        public MenuService(IGenericRepository genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public List<UserMenu> GetMenuByUser(int userId)
        {
            List<UserMenu> userMenu = new List<UserMenu>();
            return userMenu;
        }
    }
}
