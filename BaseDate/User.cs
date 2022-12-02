using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDate
{
    class User
    {
       public static string _userName;
        public string userName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (userName != "")
                    _userName = userName;
            }
        }

        public User() { }
        public User(string user)
        {
            _userName = user;
        }
    }
}
