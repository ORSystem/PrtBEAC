using EFCAO.BLL.Entities;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Collections
{
    public class C_ListConsoms : System.Collections.CollectionBase
    {
        #region ------------------Variables membres------------------
        private C_Consom _TheClientConsommation;



        #endregion

        #region ------------------Constructeur------------------
        public C_ListConsoms()
        {
        }
        #endregion

        #region ------------------Accesseurs------------------
        public C_Consom TheClientConsommation
        {
            get { return _TheClientConsommation; }
            set { _TheClientConsommation = value; }
        }

        #endregion

        #region ------------------collection------------------
        /// <summary>
        /// Méthode d'accés à un objet Consommation de la collection List Consommations.
        /// </summary>
        public C_Consom this[int index]
        {
            get
            {
                return ((C_Consom)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Ajout d'un Consommation dans la Collection List Consommations.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(C_Consom value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// n°de rang de l'objet Consommation dans la collection List Consommations.
        /// </summary>
        /// <param name="value">Objet Consommation</param>
        /// <returns>Le rang de l'objet Consommation dans la collection List Consommations.</returns>
        public int IndexOf(C_Consom value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Insertion d'un objet Consommation d'aprés son rang.
        /// </summary>
        /// <param name="index"> N°rang entier</param>
        /// <param name="value">Objet Consommation</param>
        public void Insert(int index, C_Consom value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Suppression d'un objet Consommation dans la collection List Consommations.
        /// </summary>
        /// <param name="value">Objet Consommation</param>
        public void Remove(C_Consom value)
        {
            List.Remove(value);
        }

        #endregion

        #region ------------------Find Consommation------------------
        /// <summary>
        /// Find a Consommation from the collection List Consommations by ID.
        /// <returns>TheClientConsommation</returns>
        /// </summary>  
        public C_Consom FindConsommation(int ConsommationID)
        {
            C_Consom nRetour = new C_Consom();

            foreach (C_Consom TheClientConsommation in this)
            {
                if (TheClientConsommation.Consumption_ID == ConsommationID)
                {
                    nRetour = TheClientConsommation;
                    break;
                }
            }
            return nRetour;
        }
        #endregion

        #region ------------------Get list consommation by user id------------------
        /// <summary>
        /// Get list consommation by user id.
        /// <returns>The list Consommations</returns>
        /// </summary>  
        public object GetListConsommation(int UserID)
        {
            try
            {
                C_ConsomDal TheConsommationDal = new C_ConsomDal();
                return TheConsommationDal.GetListConsommation(this, UserID);
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