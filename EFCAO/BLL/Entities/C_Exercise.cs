using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_Exercise
    {
        #region -----------------------------Variables membres-----------------------------


        private UInt64 _CleUnik;
        private int _TypeExercice;
        private int _Verrouille;
        private string _DateExer;
        private string _Unite;
        private string _Currency;
        private int _DureeExer;
        private string _Model;
        private bool _RowChecked;


        #endregion

        #region -----------------------------Accesseurs-----------------------------

        public UInt64 CleUnik
        {
            get { return _CleUnik; }
            set { _CleUnik = value; }
        }

        public int TypeExercice
        {
            get { return _TypeExercice; }
            set { _TypeExercice = value; }
        }

        public int Verrouille
        {
            get { return _Verrouille; }
            set { _Verrouille = value; }
        }

        public string DateExer
        {
            get { return _DateExer; }
            set { _DateExer = value; }
        }

        public string Unite
        {
            get { return _Unite; }
            set { _Unite = value; }
        }



        public int DureeExer
        {
            get { return _DureeExer; }
            set { _DureeExer = value; }
        }

        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }

        public string Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public bool RowChecked
        {
            get { return _RowChecked; }
            set { _RowChecked = value; }
        }

        #endregion

        #region -----------------------------Region Search-----------------------------

        #endregion
    }
}