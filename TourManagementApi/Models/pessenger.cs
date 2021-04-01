using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourManagementApi.Models
{
    public class pessenger
    {
        String _fname;
        String _lname;
        String _email;
        String _age;
        String _trip_date;

        public String fname
        {
            get { return _fname; }
            set { this._fname = value; }
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

        public String age
        {
            get { return _age; }
            set { this._age = value; }
        }

        public String trip_date
        {
            get { return _trip_date; }
            set { this._trip_date = value; }
        }
    }
}