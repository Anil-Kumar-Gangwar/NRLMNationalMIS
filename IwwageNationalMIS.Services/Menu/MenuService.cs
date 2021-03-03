using IwwageNationalMIS.Services.IServices;
using DTOModel = IwwageNationalMIS.Data.Model;
using IwwageNationalMIS.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using IwwageNationalMIS.Data.Repositories.IRepositories;
using AutoMapper;
using System.Text.RegularExpressions;

namespace IwwageNationalMIS.Services
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
            throw new NotImplementedException();
        }
    }
}
