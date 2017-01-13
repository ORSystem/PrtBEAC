using EFCAO.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.Security.AccessControl;

namespace EFCAO
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Ctrl_Efcao CtrlEfcao = new Ctrl_Efcao();
            Session.Clear();
            Session.Add("CtrlEfcao", CtrlEfcao);
            Session["SelectedCulture"] = "fr-FR";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        #region ----------------------Adds an ACL entry on the specified directory for the specified account.-------------------------
        /// <summary>
        /// Adds an ACL entry on the specified directory for the specified account.
        /// </summary>
        /// <param name="dirName">Folder path to add the permissions to</param>
        /// <param name="Account"></param>
        /// <param name="Rights"></param>
        /// <param name="ControlType"></param>
        private void AddDirectorySecurity(string folderName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            try
            {
                // Create a new DirectoryInfo object.
                DirectoryInfo dInfo = new DirectoryInfo(folderName);

                // Get a DirectorySecurity object that represents the current security settings.
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.AddAccessRule(new FileSystemAccessRule(account, rights, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, controlType));

                // Set the new access settings.
                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Removes an ACL entry on the specified directory for the specified account.
        public void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            try
            {
                // Create a new DirectoryInfo object.
                DirectoryInfo dInfo = new DirectoryInfo(FileName);

                // Get a DirectorySecurity object that represents the 
                // current security settings.
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                // Add the FileSystemAccessRule to the security settings. 
                dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

                // Set the new access settings.
                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Adds an ACL entry on the specified file for the specified account.
        public void AddFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {

            // Get a FileSecurity object that represents the
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Add the FileSystemAccessRule to the security settings.
            fSecurity.AddAccessRule(new FileSystemAccessRule(account,
                rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);

        }

        // Removes an ACL entry on the specified file for the specified account.
        public void RemoveFileSecurity(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {

            // Get a FileSecurity object that represents the
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Remove the FileSystemAccessRule from the security settings.
            fSecurity.RemoveAccessRule(new FileSystemAccessRule(account,
                rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);

        }

        #endregion
    }
}