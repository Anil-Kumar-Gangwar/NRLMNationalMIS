using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRLMNationalMIS.Model
{
    public class UserMenu
    {
        public int menuId { get; set; }
        public Nullable<int> parentMenuId { get; set; }
        public string menuName { get; set; }
        public Nullable<int> sequenceNo { get; set; }
        public string url { get; set; }
        public string iconClass { get; set; }
        public bool help { get; set; }
        public bool createRight { get; set; }
        public bool editRight { get; set; }
        public bool viewRight { get; set; }
        public bool deleteRight { get; set; }
    }
}
