using EFCAO.EfcaoException;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace EFCAO.CommonFunctions
{
    public class C_Functionglobal
    {
        #region -------------------------Get DataBase connectionstring Not used-------------------------
        /// <summary>
        ///  Function to get the connection SQL
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///  Function to get the connection SQL
        /// </summary>
        /// <returns></returns>
        public static string getOracleConnection()
        {
            try
            {
                bool Bool_SERVICE_NAME = Convert.ToBoolean(ConfigurationManager.AppSettings["Bool_SERVICE_NAME"].ToString());
                bool Bool_SID = Convert.ToBoolean(ConfigurationManager.AppSettings["Bool_SID"].ToString());

                string HOST = ConfigurationManager.AppSettings["HOST"].ToString();
                string PORT = ConfigurationManager.AppSettings["PORT"].ToString();
                string SERVICE_NAME = ConfigurationManager.AppSettings["SERVICE_NAME"].ToString();
                string User = ConfigurationManager.AppSettings["User"].ToString();
                string Pass = ConfigurationManager.AppSettings["Pass"].ToString();
                string SID = ConfigurationManager.AppSettings["SID"].ToString();

                string connString = "";

                if (Bool_SERVICE_NAME)
                {
                    connString = "Data Source=(DESCRIPTION="
                                       + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + HOST + ")(PORT=" + PORT + ")))"
                                       + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + SERVICE_NAME + ")));"
                                       + "User Id=" + User + ";Password=" + Pass + ";";
                }
                else if (Bool_SID)
                {
                    connString = "Data Source=(DESCRIPTION="
                                      + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + HOST + ")(PORT=" + PORT + ")))"
                                      + "(CONNECT_DATA=(SERVER=DEDICATED)(SID=" + SID + ")));"
                                      + "User Id=" + User + ";Password=" + Pass + ";";
                }

                //string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;


                //string connString = "Data Source=(DESCRIPTION="
                //                  + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.200)(PORT=1521)))"
                //                  + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=beac.orsystem.com)));"
                //                  + "User Id=beac;Password=Service34;";


                return connString;
            }
            catch (SystemException)
            {
                throw;
            }
        }
        #endregion

        //#region -------------------------getOracleConnectionFromIniParameter-------------------------
        ///// <summary>
        /////  getOracleConnectionFromIniParameter
        ///// </summary>
        ///// <returns></returns>
        //public static string getOracleConnectionFromIniParameter()
        //{
        //    string connString = "";

        //    try
        //    {
        //        string path = System.Web.HttpContext.Current.Server.MapPath("~/DbConfig/ParametreDb.ini");
        //        string Host = "";
        //        string Port = "";
        //        string Sid = "";
        //        string ServiceName = "";
        //        string User = "";
        //        string Pswd = "";
        //        bool TheSid = false;
        //        bool TheServiceName = false;

        //        if (File.Exists(path))
        //        {
        //            var Reader = new StreamReader(File.OpenRead(@"" + path));
        //            while (!Reader.EndOfStream)
        //            {
        //                var line = Reader.ReadLine();
        //                if (line != "")
        //                {
        //                    int eq = line.IndexOf("=");
        //                    if (eq != -1)
        //                    {
        //                        string LeftValue = GetTheModelLeftValueIniFile(line);
        //                        string RightValue = GetTheModelRightValueIniFile(line);

        //                        if (LeftValue == "Host")
        //                        {
        //                            Host = RightValue;
        //                        }
        //                        else if (LeftValue == "Port")
        //                        {
        //                            Port = RightValue;
        //                        }
        //                        else if (LeftValue == "Sid")
        //                        {
        //                            Sid = RightValue;
        //                        }
        //                        else if (LeftValue == "ServiceName")
        //                        {
        //                            ServiceName = RightValue;
        //                        }
        //                        else if (LeftValue == "User")
        //                        {
        //                            User = RightValue;
        //                        }
        //                        else if (LeftValue == "PsWd")
        //                        {
        //                            Pswd = RightValue;
        //                        }

        //                        else if (LeftValue == "TheSid")
        //                        {
        //                            TheSid = Convert.ToBoolean(RightValue);
        //                        }
        //                        else if (LeftValue == "TheServiceName")
        //                        {
        //                            TheServiceName = Convert.ToBoolean(RightValue);
        //                        }

        //                    }
        //                }
        //            }
        //            Reader.Close();
        //            Reader.Dispose();

        //            if (TheServiceName)
        //            {
        //                connString = "Data Source=(DESCRIPTION="
        //                              + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Host + ")(PORT=" + Port + ")))"
        //                              + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + ServiceName + ")));"
        //                              + "User Id=" + User + ";Password=" + Pswd + ";";
        //            }
        //            else if (TheSid)
        //            {
        //                connString = "Data Source=(DESCRIPTION="
        //                              + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + Host + ")(PORT=" + Port + ")))"
        //                              + "(CONNECT_DATA=(SERVER=DEDICATED)(SID=" + Sid + ")));"
        //                              + "User Id=" + User + ";Password=" + Pswd + ";";
        //            }

        //        }
        //        return connString;
        //    }
        //    catch (SystemException)
        //    {
        //        throw;
        //    }
        //}
        //#endregion

        #region -------------------------Language -------------------------
        /// <summary>
        /// Traduction languge
        /// </summary>
        public static string GetObjectLanguage(string IdTrtaduction)
        {
            string strCulture = Convert.ToString(HttpContext.Current.Session["SelectedCulture"]);
            //create the culture based upon session culuture
            CultureInfo objCI = new CultureInfo(strCulture);
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;

            //Read the rsources files
            String strResourcesPath = HttpContext.Current.Server.MapPath("~/bin");
            ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("resource", strResourcesPath, null);
            string Result = rm.GetString(IdTrtaduction);
            return Result;
        }

        #endregion

        #region   ----------------------Substring the model left value--------------
        /// <summary>
        /// Substring the model left value
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetTheModelLeftValueIniFile(string ModelValue)
        {
            int LengthBeforEqual = 0;
            int index = ModelValue.IndexOf("=");
            LengthBeforEqual = index;
            string ExpectedResult = ModelValue.Substring(0, LengthBeforEqual);
            return (ExpectedResult);
        }

        #endregion

        #region   ----------------------Substring the model rihgt value--------------
        /// <summary>
        /// Substring the model rihgt value
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetTheModelRightValueIniFile(string ModelValue)
        {
            int LengthBeforEqual = 0;
            int LengthAfterEqual = 0;
            int index = ModelValue.IndexOf("=");
            LengthBeforEqual = (index + 1);
            LengthAfterEqual = (ModelValue.Length - (index + 1));
            string ExpectedResult = ModelValue.Substring(LengthBeforEqual, LengthAfterEqual);
            return (ExpectedResult);
        }

        #endregion

        #region   ----------------------Substring the model rihgt value (T)-------------
        /// <summary>
        /// Substring the model rihgt value (T)
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetTheDateLeftValue(string DateValue)
        {
            int LengthBeforEqual = 0;
            int index = DateValue.IndexOf("T");
            LengthBeforEqual = index;
            string ExpectedResult = DateValue.Substring(0, LengthBeforEqual);
            return (ExpectedResult);
        }

        #endregion

        #region   ----------------------Create Pdf Font -------------
        /// <summary>
        /// 
        /// </summary>
        public static void CreatePdfFont()
        {
            try
            {
                int totalfonts = FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "GenerePdfDal.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Erreur lors de création de police pdf";
                Except.ErrorDetail = "Detail : " + ex.Message;
                Except.ErrorClass = "C_Functionglobal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_Functionglobal.CreatePdfFont()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;

            }
        }

        #endregion

        #region   ----------------------Function to Test if a string is Integer values --------------
        /// <summary>
        ///  Function to Test if the data is Integer values
        /// </summary>
        /// <returns></returns>
        public static bool isInt(string value)
        {
            bool bOk = true;

            try
            {
                int.Parse(value);
            }
            catch
            {
                bOk = false;
            }
            return bOk;
        }

        #endregion

        #region   ----------------------Function to Test if a string is Decimal values --------------
        /// <summary>
        ///  Function to Test if the data is Decimal values
        /// </summary>
        /// <returns></returns>
        public static bool isDecimal(string value)
        {
            bool bOk = true;
            try
            {
                decimal.Parse(value);
            }
            catch
            {
                bOk = false;
            }
            return bOk;
        }

        #endregion

        #region   ----------------------Function to Test if a string is Numeric values --------------
        /// <summary>
        ///  Function to Test if a string is Numeric values
        /// </summary>
        /// <returns></returns>
        public static bool isNumeric(string value)
        {
            bool bOk = true;
            try
            {
                int.Parse(value);
            }
            catch
            {
                bOk = false;
            }
            return bOk;
        }

        #endregion

        #region   ----------------------Function to Test if a string is Boolean values --------------
        /// <summary>
        ///  Function to Test if a string is Boolean values
        /// </summary>
        /// <returns></returns>
        public static bool isBoolean(string value)
        {
            bool bOk = true;

            try
            {
                bool.Parse(value);
            }
            catch
            {
                bOk = false;
            }

            return bOk;
        }

        #endregion

        #region   ----------------------Function to Test if a string is DateTime values --------------
        /// <summary>
        ///  Function to Test if a string is DateTime values
        /// </summary>
        /// <returns></returns>
        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate; return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }

        #endregion

        #region   ----------------------Function to convert a date to french date--------------
        /// <summary>
        ///  Function to convert a date to french date
        /// </summary>
        /// <returns></returns>
        public static string ConvertToFrenchDate(DateTime AnyDate)
        {
            string dateEnFrancais = "";
            try
            {
                CultureInfo francais = CultureInfo.GetCultureInfo("fr-FR");
                CultureInfo anglais = CultureInfo.GetCultureInfo("en-US");
                string dateEnUs = AnyDate.ToShortDateString();
                dateEnFrancais = DateTime.Parse(dateEnUs, anglais).ToString(francais);
                dateEnFrancais = dateEnFrancais.Substring(0, dateEnFrancais.Length - 8);
            }
            catch
            {
                dateEnFrancais = "Error";
            }
            return dateEnFrancais;
        }

        #endregion

        #region   ----------------------Function to convert a date to English date--------------
        /// <summary>
        ///  Function to convert a date to English date
        /// </summary>
        /// <returns></returns>
        public static string ConvertToEnglishDate(DateTime AnyDate)
        {
            //AnyDate = AnyDate.ToShortDateString();
            string dateEnanglais = "";
            DateTime dateEnanglais1;
            try
            {
                CultureInfo francais = CultureInfo.GetCultureInfo("fr-FR");
                CultureInfo anglais = CultureInfo.GetCultureInfo("en-GB");
                string dateEnFr = AnyDate.ToShortDateString();
                DateTimeStyles styles = DateTimeStyles.None;
                DateTime.TryParse(dateEnFr, anglais, styles, out dateEnanglais1);
            }
            catch
            {
                dateEnanglais = "Error";
            }
            return dateEnanglais;
        }
        #endregion

        #region   ----------------------Function to convert any date to number--------------
        /// <summary>
        ///  Function to convert any date to number
        /// </summary>
        /// <returns></returns>
        public static string AnyDateToNumber(string AnyDate)
        {
            string EnGB = String.Format("{0:u}", Convert.ToDateTime(AnyDate));
            int i = 0;

            try
            {
                if (i == 0)
                {
                    int index = EnGB.IndexOf("-");
                    EnGB = EnGB.Remove(index, 1);
                    i = i + 1;
                }

                if (i == 1)
                {
                    int index = EnGB.IndexOf("-");
                    EnGB = EnGB.Remove(index, 1);
                    int length = EnGB.Length;
                    EnGB = EnGB.Remove(8, length - 8);
                }
            }

            catch
            {
                EnGB = "false";
            }

            return EnGB;
        }
        #endregion

        #region   ----------------------Substring the model left value--------------
        /// <summary>
        /// Substring the model left value
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetTheModelLeftValue(string ModelValue)
        {
            int LengthBeforEqual = 0;
            int index = ModelValue.IndexOf("=");
            LengthBeforEqual = index;
            string ExpectedResult = ModelValue.Substring(0, LengthBeforEqual);
            return (ExpectedResult);
        }

        #endregion

        #region   ----------------------Substring the model rihgt value--------------
        /// <summary>
        /// Substring the model rihgt value
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetTheModelRightValue(string ModelValue)
        {
            int LengthBeforEqual = 0;
            int LengthAfterEqual = 0;
            int index = ModelValue.IndexOf("=");
            LengthBeforEqual = (index + 1);
            LengthAfterEqual = (ModelValue.Length - (index + 1));
            string ExpectedResult = ModelValue.Substring(LengthBeforEqual, LengthAfterEqual);
            return (ExpectedResult);
        }

        #endregion

        #region   ---------------------Convert a text to 2 decimal places--------------
        /// <summary>
        /// Convert a text to a given number of decimal places
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="numberOfDecimalPlaces"></param>
        /// <returns>result</returns>Formated number with the given decimale places
        public static string ConvertStringToDoubleRound2Decimal(string Text, int numberOfDecimalPlaces)
        {
            string result;
            if (numberOfDecimalPlaces == 1)
            {
                decimal numero = Convert.ToDecimal(Text);
                double Numero = (double)Math.Round(numero, 2);
                result = String.Format("{0:0.0}", Numero);
            }
            else if (numberOfDecimalPlaces == 2)
            {
                decimal numero = Convert.ToDecimal(Text);
                double Numero = (double)Math.Round(numero, 2);
                result = String.Format("{0:0.00}", Numero);
            }

            else if (numberOfDecimalPlaces == 3)
            {
                decimal numero = Convert.ToDecimal(Text);
                double Numero = (double)Math.Round(numero, 2);
                result = String.Format("{0:0.000}", Numero);
            }

            else if (numberOfDecimalPlaces == 4)
            {
                decimal numero = Convert.ToDecimal(Text);
                double Numero = (double)Math.Round(numero, 2);
                result = String.Format("{0:0.0000}", Numero);
            }
            else
            {
                decimal numero = Convert.ToDecimal(Text);
                double Numero = (double)Math.Round(numero, 2);
                result = String.Format("{0:0.00000}", Numero);
            }
            return result;
        }

        #endregion

        #region   ---------------------Convert a text to 3 decimal places--------------
        /// <summary>
        /// Convert a text to 3 decimal places
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="numberOfDecimalPlaces"></param>
        /// <returns>result</returns>Formated number with the given decimale places
        public static string ConvertStringTo3Decimal(string Text)
        {
            string result;
            decimal numero = Convert.ToDecimal(Text);
            result = String.Format("{0:0.000}", numero);
            return result;
        }

        #endregion

        #region   ---------------------Formate Color--------------
        /// <summary>
        /// Formate Color
        /// </summary>
        /// <param name="Color"></param>
        /// <returns>ExpectedResult</returns>
        public static string FormateColor(string Color)
        {
            string ExpectedResult = "";
            try
            {
                if (Color.Length == 6)
                {
                    ExpectedResult = Color;
                }
                else if (Color.Length == 7)
                {
                    ExpectedResult = Color.Substring(1);
                }
                else if (Color.Length == 8)
                {
                    ExpectedResult = Color.Substring(2);
                }
                else if (Color.Length == 9)
                {
                    ExpectedResult = Color.Substring(3);
                }
                else
                {
                    ExpectedResult = Color.Substring(4);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return (ExpectedResult);
        }

        #endregion

        #region -------------------------------Pdf function----------------------------
        /// <summary>
        /// Convert To Point
        /// </summary>
        /// <param name="SomeValue"></param>
        /// <return>point</return>
        public static float ConvertToPoint(float SomeValue)
        {
            float PointValue = SomeValue * Single.Parse("2,834645669291");
            return PointValue;
        }

        #endregion

        #region ------------------------------Check line break----------------------------
        /// <summary>
        /// Check line break
        /// </summary>
        /// <param name="ModelValue"></param>
        /// <returns>ExpectedResult</returns>
        public static bool ChecklineBreak(string st)
        {
            bool result = false;
            try
            {
                int index = st.IndexOf(@"\");
                if (index >= 0)
                {
                    if (st[index + 1].ToString() == "n")
                    {
                        result = true;
                    }
                }
            }
            catch (SystemException)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region ------------------------------Get line before breake----------------------------
        /// <summary>
        /// Substring the line befor break
        /// </summary>
        /// <param name="st"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetlineBeforeBreake(string st)
        {
            int stringLenght = st.Length;
            int LengthBeforEqual = 0;
            int index = st.IndexOf(@"\");
            LengthBeforEqual = index;
            string ExpectedResult = st.Substring(0, LengthBeforEqual);
            return (ExpectedResult);
        }
        #endregion

        #region ------------------------------Get line after break----------------------------
        /// <summary>
        /// Substring the line after break
        /// </summary>
        /// <param name="st"></param>
        /// <returns>ExpectedResult</returns>
        public static string GetlineAfterBreake(string st)
        {
            int LengthBeforEqual = 0;
            int LengthAfterEqual = 0;
            int index = st.IndexOf(@"\");
            LengthBeforEqual = (index + 2);
            LengthAfterEqual = (st.Length - (index + 2));
            string ExpectedResult = st.Substring(LengthBeforEqual, LengthAfterEqual);
            return (ExpectedResult);
        }

        #endregion

        #region -------------------button Valider Pdf Name_Click ------------------
        /// <summary>
        /// Button Valider Pdf Name_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static bool ValidateCompanyName(string name)
        {
            try
            {
                bool nRetour = false;
                string StringToTest = name;

                string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[~`+=" + "\"";

                char[] specialCharactersArray = specialCharacters.ToCharArray();

                int index = StringToTest.IndexOfAny(specialCharactersArray);
                //index == -1 no special characters
                if (index == -1)
                {
                    nRetour = false;
                }
                else
                {
                    nRetour = true;
                }

                return nRetour;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static string ReplaceCompanyName(string name)
        {
            try
            {
                string nRetour = "";
                string StringToTest = name;

                Regex specialCharacters = new Regex("[;\\\\/:*?\"<>|&']");

                nRetour = specialCharacters.Replace(name, "_");
                return nRetour;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}