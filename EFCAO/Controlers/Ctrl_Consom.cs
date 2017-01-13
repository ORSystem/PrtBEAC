using EFCAO.BLL.Collections;
using EFCAO.BLL.Entities;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.Controlers
{
    public class Ctrl_Consom
    {
        #region -----------------------------Variable-----------------------------

        private C_ListConsoms _TheListConsommation;
        private C_Consom _TheConsommationToInsert;

        #endregion

        #region -----------------------------Constructeur-------------------------

        public Ctrl_Consom()
        {
            TheListConsommation = new C_ListConsoms();
            TheConsommationToInsert = new C_Consom();

        }

        #endregion

        #region -----------------------------Accesseurs---------------------------
        public C_ListConsoms TheListConsommation
        {
            get { return _TheListConsommation; }
            set { _TheListConsommation = value; }
        }
        public C_Consom TheConsommationToInsert
        {
            get { return _TheConsommationToInsert; }
            set { _TheConsommationToInsert = value; }
        }
        #endregion

        #region ----------------------------Get List Consommation--------------------------
        /// <summary>
        /// Get list groups
        /// </summary>
        /// <param name="TheListGroups"></param>
        /// <returns> TheListGroups</returns>
        public object GetListConsommation(int UserID)
        {
            try
            {
                return TheListConsommation.GetListConsommation(UserID);

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

        #region ----------------------------Get membership end life time--------------------------
        /// <summary>
        /// Get membership end life time
        /// </summary>
        /// <param name="TheListGroups"></param>
        /// <returns> TheListGroups</returns>
        public DateTime GetMspEndDate(int UserId)
        {
            C_ConsomDal TheConsommationDal = new C_ConsomDal();
            try
            {
                return TheConsommationDal.GetMspEndDate(UserId);
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

        #region ----------------------------Get membership end life time--------------------------
        /// <summary>
        /// Get membership end life time
        /// </summary>
        /// <param name="TheListGroups"></param>
        /// <returns> TheListGroups</returns>
        public int GetMspRemainigTokens(int UserId)
        {
            C_ConsomDal TheConsommationDal = new C_ConsomDal();
            try
            {
                return TheConsommationDal.GetMspRemainigTokens(UserId);
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