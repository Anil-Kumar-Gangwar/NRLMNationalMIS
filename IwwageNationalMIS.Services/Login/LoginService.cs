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
    public class LoginService : BaseService, ILoginService
    {
        private IGenericRepository genericRepository;
        public LoginService(IGenericRepository genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public bool AuthenticateUser(ValidateLogin userCredential, out UserDetail userDetail)
        {
            throw new NotImplementedException();
        }
    }
}
