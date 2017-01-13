using EFCAO.BLL.Entities;
using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Collections
{
    public class C_ListAdesDocsPdf : System.Collections.CollectionBase
    {
        #region --------------------Variables membres--------------------
        private C_AdesDocPdf _TheDocPdf;

        #endregion

        #region --------------------Constructeur--------------------
        public C_ListAdesDocsPdf()
        {
        }
        #endregion

        #region --------------------Accesseurs--------------------
        public C_AdesDocPdf TheDocPdf1
        {
            get { return _TheDocPdf; }
            set { _TheDocPdf = value; }
        }

        #endregion

        #region --------------------collection--------------------
        /// <summary>
        /// Méthode d'accés à un objet Project de la collection Projects.
        /// </summary>
        public C_AdesDocPdf this[int index]
        {
            get
            {
                return ((C_AdesDocPdf)List[index]);
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
        public int Add(C_AdesDocPdf value)
        {
            return (List.Add(value));
        }
        /// <summary>
        /// n°de rang de l'objet Project dans la collection Project.
        /// </summary>
        /// <param name="value">Objet Project</param>
        /// <returns>Le rang de l'objet Project dans la collection Project.</returns>
        public int IndexOf(C_AdesDocPdf value)
        {
            return (List.IndexOf(value));
        }
        /// <summary>
        /// Insertion d'un objet Project d'aprés son rang.
        /// </summary>
        /// <param name="index"> N°rang entier</param>
        /// <param name="value">Objet Project</param>
        public void Insert(int index, C_AdesDocPdf value)
        {
            List.Insert(index, value);
        }
        /// <summary>
        /// Suppression d'un objet Project dans la collection Project.
        /// </summary>
        /// <param name="value">Objet Project</param>
        public void Remove(C_AdesDocPdf value)
        {
            List.Remove(value);
        }
        #endregion

        #region --------------------Search Method--------------------
        /// <summary>
        /// Find a customer from collection ListCustomer by ID.
        /// Une exception de classe PortailWebAnaDefi Exeception est renvoyé en cas d'erreur.
        /// <param>IdentifiantPrincipal</param>
        /// <returns> TheCustomer</returns>
        /// </summary>  

        public C_AdesDocPdf FindAPEC(int ID)
        {
            // int nRetour;
            C_AdesDocPdf nRetour = new C_AdesDocPdf();

            foreach (C_AdesDocPdf TheDocPdf in this)
            {
                if (TheDocPdf.PdfID == ID)
                {
                    nRetour = TheDocPdf;
                    break;
                }
            }

            return nRetour;
        }



        public object GetListDocPdf(string ModelName)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();

                return TheEfcaoDal.GetListDocPdf(ModelName, this);
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