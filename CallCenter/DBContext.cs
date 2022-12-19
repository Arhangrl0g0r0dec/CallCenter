using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter
{
    class DBContext
    {
        private static CallCenterEntities _context;

        public static CallCenterEntities GetContext() 
        {
            if (_context == null) 
            {
                _context = new CallCenterEntities();
            }
            return _context;
        }
    }
}
