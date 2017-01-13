using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.DAO
{
    public class C_DateDataParam
    {
        private string _name;
        private DateTime _valueDate;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime valueDate
        {
            get { return _valueDate; }
            set { _valueDate = value; }
        }

        public C_DateDataParam(string name, DateTime valueDate)
        {
            this._name = "@" + name;
            this._valueDate = valueDate;
        }
    }
}