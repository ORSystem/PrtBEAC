using EFCAO.BLL.Entities;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Collections
{
    public class C_ListExercises : System.Collections.CollectionBase
    {
        #region ----------------------Variables membres---------------------
        private C_Exercise _TheExercise;

        #endregion

        #region ---------------------Constructeur---------------------
        public C_ListExercises()
        {
        }
        #endregion

        #region ---------------------Accesseurs---------------------
        public C_Exercise TheExercise
        {
            get { return _TheExercise; }
            set { _TheExercise = value; }
        }

        #endregion

        #region ---------------------collection---------------------
        /// <summary>
        /// Méthode d'accés à un objet Exercice de la collection Liste Exercices.
        /// </summary>
        public C_Exercise this[int index]
        {
            get
            {
                return ((C_Exercise)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Ajout d'un Exercise dans la Collection Liste Exercices.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(C_Exercise value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// n°de rang de l'objet Exercise dans la collection Liste Exercices.
        /// </summary>
        /// <param name="value">Objet Exercice</param>
        /// <returns>Le rang de l'objet Exercice dans la collection Liste Exercises.</returns>
        public int IndexOf(C_Exercise value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Insertion d'un objet Exercice d'aprés son rang.
        /// </summary>
        /// <param name="index"> N°rang entier</param>
        /// <param name="value">Objet Exercice</param>
        public void Insert(int index, C_Exercise value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Suppression d'un objet Exercice dans la collection Liste Exercices.
        /// </summary>
        /// <param name="value">Objet Exercice</param>
        public void Remove(C_Exercise value)
        {
            List.Remove(value);
        }

        #endregion

        #region ---------------------Method---------------------

        #region ---------------------Find Exercice---------------------
        /// <summary>
        /// Find a Exercice from collection List Exercices par Cle unique.
        /// <returns> The Exercice</returns>
        /// </summary>  

        public C_Exercise FindExercise(UInt64 CleUnik)
        {
            C_Exercise nRetour = new C_Exercise();

            foreach (C_Exercise TheExercise in this)
            {
                if (TheExercise.CleUnik == CleUnik)
                {
                    nRetour = TheExercise;
                    break;
                }
            }

            return nRetour;
        }
        #endregion

        #region --------------------Get Balance List---------------------
        /// <summary>
        /// Get Balance List.
        /// <returns>Exercise</returns>
        /// </summary>  
        public object GetBalanceList(UInt64 CompanyKey, string CompanyModelSaisie)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.GetBalanceList(CompanyKey, this, CompanyModelSaisie);
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