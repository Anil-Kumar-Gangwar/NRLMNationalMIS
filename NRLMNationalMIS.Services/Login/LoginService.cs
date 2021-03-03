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
    public class LoginService : BaseService, ILoginService
    {
        private IGenericRepository genericRepository;
        public LoginService(IGenericRepository genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public bool AuthenticateUser(ValidateLogin userCredential, out UserDetail userDetail)
        {
            userDetail = null;
            return true;
        }
    }
}
