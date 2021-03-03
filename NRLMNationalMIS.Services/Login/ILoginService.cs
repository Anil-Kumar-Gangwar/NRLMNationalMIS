using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRLMNationalMIS.Model;

namespace NRLMNationalMIS.Services.IServices
{
    public interface ILoginService
    {
        bool AuthenticateUser(ValidateLogin userCredential, out UserDetail userDetail);
    }
}
