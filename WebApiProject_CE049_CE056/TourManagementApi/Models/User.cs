using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourManagementApi.Models
{
    public class User
    {
        String _fname;
        String _lname;
        String _email;
        String _password;

        public String fname
        {
            get { return _fname; }
            set { this._fname = value;  }
        }

        public String lname
        {
            get { return _lname; }
            set { this._lname = value; }
        }

        public String email
        {
            get { return _email; }
            set { this._email = value; }
        }

        public String password
        {
            get { return _password; }
            set { this._password = value; }
        }

    }
}