using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_ExercicesModel
    {
        #region -----------------------------Variables membres-----------------------------

        // Other variable are added to the class for further important use
        private int _ExercicesModelID;
        private string _CompanyModelSaisie;
        private string _CompanyModelAnalyse;

        #endregion

        #region -----------------------------Accesseurs-----------------------------

        public int ExercicesModelID
        {
            get { return _ExercicesModelID; }
            set { _ExercicesModelID = value; }
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


        #endregion

        #region -----------------------------Methods-----------------------------

        #endregion
    }
}