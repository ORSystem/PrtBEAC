using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_Consom
    {
        #region -----------------------------Variables membres-----------------------------

        private int _Consumption_ID;
        private int _Link_User_ID;
        private int _Link_Prod_ID;
        private int _Quantity;
        private int _Unit_Price;
        private int _Vat_Unit;
        private int _Company_Key;
        private string _Article_Name;
        private string _Link_Prod_Name;
        private DateTime _Date_Consumption;
        private string _DOCPATH;
        #endregion

        #region -----------------------------Accesseurs-----------------------------
        public int Consumption_ID
        {
            get { return _Consumption_ID; }
            set { _Consumption_ID = value; }
        }
        public int Link_User_ID
        {
            get { return _Link_User_ID; }
            set { _Link_User_ID = value; }
        }
        public int Link_Prod_ID
        {
            get { return _Link_Prod_ID; }
            set { _Link_Prod_ID = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public int Unit_Price
        {
            get { return _Unit_Price; }
            set { _Unit_Price = value; }
        }
        public int Vat_Unit
        {
            get { return _Vat_Unit; }
            set { _Vat_Unit = value; }
        }
        public int Company_Key
        {
            get { return _Company_Key; }
            set { _Company_Key = value; }
        }

        public DateTime Date_Consumption
        {
            get { return _Date_Consumption; }
            set { _Date_Consumption = value; }
        }
        public string Article_Name
        {
            get { return _Article_Name; }
            set { _Article_Name = value; }
        }
        public string Link_Prod_Name
        {
            get { return _Link_Prod_Name; }
            set { _Link_Prod_Name = value; }
        }

        public string DOCPATH
        {
            get { return _DOCPATH; }
            set { _DOCPATH = value; }
        }

        #endregion

        #region ----------------------------Consumption----------------------------
        /// <summary>
        /// Insert Consumption
        /// </summary>
        public int InsertConsommation()
        {
            C_ConsomDal TheConsommationDal = new C_ConsomDal();
            try
            {
                return TheConsommationDal.InsertConsommation(this);
            }
            catch (C_EfcaoException)
            {
                throw;
            }
            catch (SystemException)
            {
                throw;
            }

        }
        #endregion
    }
}