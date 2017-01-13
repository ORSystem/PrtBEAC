using EFCAO.EfcaoException;
using EFCAO.BLL.Entities;
using EFCAO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Collections
{
    public class C_ListProdDocs : System.Collections.CollectionBase
    {
        #region ------------------Variables membres------------------
        private C_ProductDocuments _TheProductDocuments;

        #endregion

        #region ------------------Constructeur------------------
        public C_ListProdDocs()
        {
        }
        #endregion

        #region ------------------Accesseurs------------------

        public C_ProductDocuments TheProductDocuments
        {
            get { return _TheProductDocuments; }
            set { _TheProductDocuments = value; }
        }

        #endregion

        #region ------------------collection methods------------------

            #region ------------------Méthode d'accés à un objet documents de Produit de la collection Liste documents de Produits------------------
            /// <summary>
            /// Méthode d'accés à un objet documents de Produit de la collection Liste documents de Produits.
            /// </summary>
            public C_ProductDocuments this[int index]
            {
                get
                {
                    return ((C_ProductDocuments)List[index]);
                }
                set
                {
                    List[index] = value;
                }
            }
            #endregion

            #region ------------------Ajout d'un document de Produit dans la Collection Liste documents de Produits-----------------
            /// <summary>
            /// Ajout d'un document de Produit dans la Collection Liste documents de Produits.
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public int Add(C_ProductDocuments value)
            {
                return (List.Add(value));
            }
            #endregion

            #region ------------------n°de rang de l'objet document de Produit dans la collection Liste documents de Produits.------------------
            /// <summary>
            /// n°de rang de l'objet document de Produit dans la collection Liste documents de Produits.
            /// </summary>
            /// <param name="value">Objet document de Produit</param>
            /// <returns>Le rang de l'objet document de Produit dans la collection Liste documents de Produits.</returns>
            public int IndexOf(C_ProductDocuments value)
            {
                return (List.IndexOf(value));
            }
            #endregion

            #region ------------------Insertion d'un objet document de Produit d'aprés son rang------------------
            /// <summary>
            /// Insertion d'un objet document de Produit d'aprés son rang.
            /// </summary>
            /// <param name="index"> N°rang entier</param>
            /// <param name="value">Objet document de Produit</param>
            public void Insert(int index, C_ProductDocuments value)
            {
                List.Insert(index, value);
            }
            #endregion

            #region ------------------Suppression d'un objet document de Produit dans la collection Liste documents de Produits.------------------
            /// <summary>
            /// Suppression d'un objet document de Produit dans la collection Liste documents de Produits.
            /// </summary>
            /// <param name="value">Objet document de Produit</param>
            public void Remove(C_ProductDocuments value)
            {
                List.Remove(value);
            }
            #endregion

            #region ------------------Find a document de Produit from collection List Produits by Produit ID.------------------
            /// <summary>
            /// Find a Product document from collection List documents of a product By Product document ID.
            /// <returns>The Product document</returns>
            /// </summary>  
            public C_ProductDocuments FindProductDocumentByProductDocID(int ProductDocID)
            {
                try
                {
                    C_ProductDocuments nRetour = new C_ProductDocuments();

                    foreach (C_ProductDocuments TheProductDocuments in this)
                    {
                        if (TheProductDocuments.Ades_Doc_ID == ProductDocID)
                        {
                            nRetour = TheProductDocuments;
                            break;
                        }
                    }
                    return nRetour;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            #endregion

        #endregion

        #region ------------------List products Documents------------------

        //#region ----------------------------Get list Documents of a product-----------------------
        ///// <summary>
        ///// Get list Documents of a product
        ///// </summary>
        ///// <param name="this"></param>
        ///// <returns>this</returns>
        //public object GetListDocumentsByProductID(int ProductID)
        //{
        //    try
        //    {
        //        ConsultationDAL TheConsultationDAL = new ConsultationDAL();
        //        return TheConsultationDAL.GetListDocumentsByProductID(this, ProductID);

        //    }

        //    catch (BeacExceptions ex)
        //    {
        //        throw ex;
        //    }

        //    catch (SystemException ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        /// <summary>
        /// Get List Document Saisie By ProductID
        /// </summary>
        /// <param name="this"></param>
        /// <returns>this</returns>
        public object GetListDocumentSaisieByProductID(int ProductID, string model)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.GetListDocumentSaisieByProductID(this, ProductID, model);
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

        /// <summary>
        /// Get List Document Analyse By Product ID
        /// </summary>
        /// <param name="this"></param>
        /// <returns>this</returns>
        public object GetListDocumentAnalyseByProductID(int ProductID, string model)
        {
            try
            {
                C_EfcaoDal TheEfcaoDal = new C_EfcaoDal();
                return TheEfcaoDal.GetListDocumentAnalyseByProductID(this, ProductID, model);
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