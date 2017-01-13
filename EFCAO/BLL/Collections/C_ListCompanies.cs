using EFCAO.BLL.Entities;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Collections
{
    public class C_ListCompanies : System.Collections.CollectionBase
    {
        #region -------------------------Variables membres-------------------------
        private C_Company _TheCompany;

        #endregion

        #region -------------------------Constructeur-------------------------
        public C_ListCompanies()
        {
        }
        #endregion

        #region -------------------------Accesseurs-------------------------
        public C_Company TheCompany
        {
            get { return _TheCompany; }
            set { _TheCompany = value; }
        }

        #endregion

        #region -------------------------collection-------------------------
        /// <summary>
        /// Méthode d'accés à un objet Company de la collection List Company.
        /// </summary>
        public C_Company this[int index]
        {
            get
            {
                return ((C_Company)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Ajout d'un Company dans la Collection List Company.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(C_Company value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// n°de rang de l'objet Company dans la collection List Company.
        /// </summary>
        /// <param name="value">Objet Project</param>
        /// <returns>Le rang de l'objet Company dans la collection List Company.</returns>
        public int IndexOf(C_Company value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Insertion d'un objet Company d'aprés son rang.
        /// </summary>
        /// <param name="index"> N°rang entier</param>
        /// <param name="value">Objet Company</param>
        public void Insert(int index, C_Company value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Suppression d'un objet Company dans la collection List Company.
        /// </summary>
        /// <param name="value">Objet Company</param>
        public void Remove(C_Company value)
        {
            List.Remove(value);
        }

        #endregion

        #region -------------------------Find company from the lsit company by name-------------------------
        /// <summary>
        /// Find a Company from collection ListCompany by Company name.
        /// <returns> TheCompany</returns>
        /// </summary>  

        public C_Company FindCompany(string NomCompany)
        {
            // int nRetour;
            C_Company nRetour = new C_Company();

            foreach (C_Company TheCompany in this)
            {
                if (TheCompany.Nom == NomCompany)
                {
                    nRetour = TheCompany;
                    break;
                }
            }

            return nRetour;
        }
        #endregion

        #region -------------------------Find company from the lsit company by ID-------------------------
        /// <summary>
        /// Find a Company from the collection ListCompany by CompanyID.
        /// <returns> TheCompany</returns>
        /// </summary>  
        public C_Company FindCompany(UInt64 CompanyID)
        {
            // int nRetour;
            C_Company nRetour = new C_Company();

            foreach (C_Company TheCompany in this)
            {
                if (TheCompany.Key == CompanyID)
                {
                    nRetour = TheCompany;
                    break;
                }
            }

            return nRetour;
        }
        #endregion

        #region -------------------------Search for a company-------------------------
        /// <summary>
        /// Search for a company.
        /// <returns> TheCompany</returns>
        /// </summary>  
        public object SearcheCompany(string TxtSearch)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.SearcheCompany(TxtSearch, this);
                //return null;
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