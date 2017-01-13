using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_Company
    {

        #region -----------------------------Variables membres-----------------------------
        private string _IdExterne;   // IdentifiantPrincipal
        private string _Nom;
        private string _Modele;
        private string _ModeleLabel;
        private UInt64 _key;
        private int _lockType;

        // Other variable are added to the class for further important use
        private string _CompanyModelSaisie;
        private string _CompanyModelAnalyse;
        private int _NumberOFSlectedExercises;

        #endregion

        #region -----------------------------Accesseurs-----------------------------
        public string IdExterne
        {
            get { return _IdExterne; }
            set { _IdExterne = value; }
        }

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public string Modele
        {
            get { return _Modele; }
            set { _Modele = value; }
        }

        public string ModeleLabel
        {
            get { return _ModeleLabel; }
            set { _ModeleLabel = value; }
        }

        public UInt64 Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public int LockType
        {
            get { return _lockType; }
            set { _lockType = value; }
        }

        public string CompanyModelSaisie
        {
            get { return _CompanyModelSaisie; }
            set { _CompanyModelSaisie = value; }
        }

        public string CompanyModelAnalyse
        {
            get { return _CompanyModelAnalyse; }
            set { _CompanyModelAnalyse = value; }
        }

        public int NumberOFSlectedExercises
        {
            get { return _NumberOFSlectedExercises; }
            set { _NumberOFSlectedExercises = value; }
        }
        #endregion

        #region ------------------------Methods------------------------

        #region ------------------------Model Get Links-----------------------
        /// <summary>
        /// Model Get Links
        /// </summary>
        /// <param name="CompanyModel"></param>
        /// <return>Model Links</return>
        public string ModelGetLinks(string CompanyModel)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();

                return TheEfcaoDal.ParamsModelGetLinks(CompanyModel);
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

        #region ------------------------Get Company Property in a dictonary string-----------------------
        /// <summary>
        /// Get Company Property in a dictonary string
        /// </summary>
        /// <param name="CompanyKey"></param>
        /// <return>Dictonary CompanyProperty</return>
        public Dictionary<string, string> GetCompanyProperty(UInt64 CompanyKey, Dictionary<string, string> CompanyProperty)
        {

            // Fill the company property with data
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.GetCompanyProperty(CompanyKey, CompanyProperty);

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