using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using uStoreInstallerCommonLogic;

namespace TestTestClearingCustomAction
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary savedState)
        {
            try
            {
                base.Install(savedState);

                CommonLogic.uStore.ExtractAppZipFile(Context.Parameters[@"targetdir"]);

                var webConfigAdmin = new WebConfiguration(CommonLogic.uStore.PathToAdminAppWebConfig);

                DbCommand.ExecuteTransaction(new List<DbQueries>()
                {
                    DbCommand.CreateDbQueries(webConfigAdmin.uStoreConnectionString, Resource.InstallPreset)
                });
            }
            catch (Exception exc)
            {
                var error = CommonLogic.WriteEventToWindowsLog(exc, "TestTestClearingPlugin");
                throw new Exception(error);
            }
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            System.Diagnostics.Process.Start("http://www.microsoft.com");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            try
            {
                base.Uninstall(savedState);
            }
            catch (Exception exc)
            {
                CommonLogic.WriteEventToWindowsLog(exc, "TestTestClearingPlugin");
            }
        }
    }
}
