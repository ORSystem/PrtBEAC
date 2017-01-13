using EFCAO.BLL.Entities;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Collections
{
    public class C_ListExercicesModels : System.Collections.CollectionBase
    {
        #region -----------------------Variables membres----------------------
        private C_ExercicesModel _TheExercicesModel;



        #endregion

        #region ----------------------Constructeur----------------------
        public C_ListExercicesModels()
        {
        }
        #endregion

        #region ----------------------Accesseurs----------------------
        public C_ExercicesModel TheExercicesModel
        {
            get { return _TheExercicesModel; }
            set { _TheExercicesModel = value; }
        }

        #endregion

        #region ----------------------collection----------------------
        /// <summary>
        /// Méthode d'accés à un objet Project de la collection Projects.
        /// </summary>
        public C_ExercicesModel this[int index]
        {
            get
            {
                return ((C_ExercicesModel)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }
        /// <summary>
        /// Ajout d'un Project dans la Collection Projects.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(C_ExercicesModel value)
        {
            return (List.Add(value));
        }
        /// <summary>
        /// n°de rang de l'objet Project dans la collection Project.
        /// </summary>
        /// <param name="value">Objet Project</param>
        /// <returns>Le rang de l'objet Project dans la collection Project.</returns>
        public int IndexOf(C_ExercicesModel value)
        {
            return (List.IndexOf(value));
        }
        /// <summary>
        /// Insertion d'un objet Project d'aprés son rang.
        /// </summary>
        /// <param name="index"> N°rang entier</param>
        /// <param name="value">Objet Project</param>
        public void Insert(int index, C_ExercicesModel value)
        {
            List.Insert(index, value);
        }
        /// <summary>
        /// Suppression d'un objet Project dans la collection Project.
        /// </summary>
        /// <param name="value">Objet Project</param>
        public void Remove(C_ExercicesModel value)
        {
            List.Remove(value);
        }
        #endregion

        #region ----------------------Method----------------------
        /// <summary>
        /// Find a customer from collection ListCustomer by ID.
        /// Une exception de classe PortailWebAnaDefi Exeception est renvoyé en cas d'erreur.
        /// <param>IdentifiantPrincipal</param>
        /// <returns> TheCustomer</returns>
        /// </summary>  

        public C_ExercicesModel FindExercicesModel(int ExercicesModelID)
        {
            // int nRetour;
            C_ExercicesModel nRetour = new C_ExercicesModel();

            foreach (C_ExercicesModel TheExercicesModel in this)
            {
                if (TheExercicesModel.ExercicesModelID == ExercicesModelID)
                {
                    nRetour = TheExercicesModel;
                    break;
                }
            }

            return nRetour;
        }


        public object GetExercicesModelList(UInt64 CompanyKey)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.GetExercicesModelList(CompanyKey, this);
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