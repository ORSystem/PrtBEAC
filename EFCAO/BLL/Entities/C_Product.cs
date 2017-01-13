using EFCAO.BLL.Collections;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_Product
    {
        #region -----------------------------Variables membres-----------------------------

        private int _Prod_ID;
        private string _Prod_Name;
        private string _Prod_Characteristics;
        private int _Prod_Price;
        private bool _Prod_State;
        private int _Link_Type_ID;
        private string _Type_Name;
        private string _Prod_Url;
        private string _Prod_Reference;
        private C_ListProdDocs _TheListProductsDocumentSaisie;
        private C_ListProdDocs _TheListProductsDocumentAnalyse;
        private C_ListProdDocs _TheListWorkDocSaisie;
        private C_ListProdDocs _TheListWorkDocAnalyse; 

        #endregion

        #region -----------------------------Accesseurs-----------------------------

        public int Prod_ID
        {
            get { return _Prod_ID; }
            set { _Prod_ID = value; }
        }
        public string Prod_Name
        {
            get { return _Prod_Name; }
            set { _Prod_Name = value; }
        }
        public string Prod_Characteristics
        {
            get { return _Prod_Characteristics; }
            set { _Prod_Characteristics = value; }
        }
        public int Prod_Price
        {
            get { return _Prod_Price; }
            set { _Prod_Price = value; }
        }
        public bool Prod_State
        {
            get { return _Prod_State; }
            set { _Prod_State = value; }
        }
        public int Link_Type_ID
        {
            get { return _Link_Type_ID; }
            set { _Link_Type_ID = value; }
        }

        public string Type_Name
        {
            get { return _Type_Name; }
            set { _Type_Name = value; }
        }
        public string Prod_Url
        {
            get { return _Prod_Url; }
            set { _Prod_Url = value; }
        }

        public string Prod_Reference
        {
            get { return _Prod_Reference; }
            set { _Prod_Reference = value; }
        }

        public C_ListProdDocs TheListProductsDocumentSaisie
        {
            get { return _TheListProductsDocumentSaisie; }
            set { _TheListProductsDocumentSaisie = value; }
        }
        public C_ListProdDocs TheListProductsDocumentAnalyse
        {
            get { return _TheListProductsDocumentAnalyse; }
            set { _TheListProductsDocumentAnalyse = value; }
        }
        public C_ListProdDocs TheListWorkDocSaisie
        {
            get { return _TheListWorkDocSaisie; }
            set { _TheListWorkDocSaisie = value; }
        }
        public C_ListProdDocs TheListWorkDocAnalyse
        {
            get { return _TheListWorkDocAnalyse; }
            set { _TheListWorkDocAnalyse = value; }
        }

        #endregion

        #region -----------------------------Constructor ---------------------------
        public C_Product()
        {
            TheListProductsDocumentSaisie = new C_ListProdDocs();
            TheListProductsDocumentAnalyse = new C_ListProdDocs();
            TheListWorkDocSaisie = new C_ListProdDocs();
            TheListWorkDocAnalyse = new C_ListProdDocs(); 
        }
        #endregion

        #region -----------------------------Region Isertion modification suppression of Product ---------------------------

        #region -------------------------Search for a company-------------------------
        /// <summary>
        /// Search for a company.
        /// <returns> TheCompany</returns>
        /// </summary>  
        public object GetProductByProductID(int ProductID)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.GetProductByProductID(this, ProductID);
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

        #endregion
    }
}