using EFCAO.EfcaoException;
using EFCAO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_ProductDocuments
    {
        #region -----------------------------Variables membres-----------------------------

        private int _Doc_ID;
        private int _Link_Prod_ID;
        private string _Doc_Name;
        private string _Doc_Characteristics;
        private int _Ades_Doc_ID;
        private string _Doc_EDW;
        private string _Ades_Model;
        private string _Doc_Type;
        private bool _Doc_Checked;

        #endregion

        #region -----------------------------Accesseurs-----------------------------
        public int Doc_ID
        {
            get { return _Doc_ID; }
            set { _Doc_ID = value; }
        }

        public int Link_Prod_ID
        {
            get { return _Link_Prod_ID; }
            set { _Link_Prod_ID = value; }
        }

        public string Doc_Name
        {
            get { return _Doc_Name; }
            set { _Doc_Name = value; }
        }

        public string Doc_Characteristics
        {
            get { return _Doc_Characteristics; }
            set { _Doc_Characteristics = value; }
        }

        public int Ades_Doc_ID
        {
            get { return _Ades_Doc_ID; }
            set { _Ades_Doc_ID = value; }
        }

        public string Doc_EDW
        {
            get { return _Doc_EDW; }
            set { _Doc_EDW = value; }
        }

        public string Ades_Model
        {
            get { return _Ades_Model; }
            set { _Ades_Model = value; }
        }

        public string Doc_Type
        {
            get { return _Doc_Type; }
            set { _Doc_Type = value; }
        }
        public bool Doc_Checked
        {
            get { return _Doc_Checked; }
            set { _Doc_Checked = value; }
        }
        #endregion
    }
}