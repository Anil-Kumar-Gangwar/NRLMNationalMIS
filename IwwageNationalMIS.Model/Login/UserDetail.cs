using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwwageNationalMIS.Model
{
    public class UserDetail
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userFullName { get; set; }
        public string userTypeId { get; set; }
        public string userTypeName { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        public string userEmail { get; set; }
    }
}
