using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.DAO
{
    public class C_DataParam
    {
        private string _name;
        private string _value;
        private string _Type;
        private int _Size;


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }

        }

        public int Size
        {
            get { return _Size; }
            set { _Size = value; }

        }


        public C_DataParam(string name, string value, int Size, string Type = null)
        {
            this._name = "I_" + name;
            this._value = value;
            this._Type = Type;
            this._Size = Size;
        }
    }
}