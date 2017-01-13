using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_AdesConfig
    {
        #region -----------------------------Variables membres-----------------------------

        private string _ADES_USER;
        private string _ADES_PASS;
        private string _IP_ADDRESS;
        private string _FOLDER_NAME;
        private string _SITE_URL;

        #endregion

        #region -----------------------------Accesseurs-----------------------------
        public string ADES_USER
        {
            get { return _ADES_USER; }
            set { _ADES_USER = value; }
        }
        public string ADES_PASS
        {
            get { return _ADES_PASS; }
            set { _ADES_PASS = value; }
        }
        public string IP_ADDRESS
        {
            get { return _IP_ADDRESS; }
            set { _IP_ADDRESS = value; }
        }
        public string FOLDER_NAME
        {
            get { return _FOLDER_NAME; }
            set { _FOLDER_NAME = value; }
        }

        public string SITE_URL
        {
            get { return _SITE_URL; }
            set { _SITE_URL = value; }
        }

        #endregion

        #region -----------------------------Constructor ---------------------------
        public C_AdesConfig()
        {
        }
        #endregion

        #region -----------------Get Beac contact-----------------
        /// <summary>
        /// Get the list of all users roles
        /// <param name="this"></param>
        /// <returns> this</returns>
        /// </summary>
        public object GetBeacConfig()
        {
            try
            {
                C_ConsommationDal TheConsommationDal = new C_ConsommationDal();

                //ConsultationDAL TheConsultationDAL = new ConsultationDAL();
                return TheConsommationDal.GetBeacConfig(this);

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