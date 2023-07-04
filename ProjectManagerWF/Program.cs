using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectManagerWF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmLauncher frmLauncher = new frmLauncher();
            if (frmLauncher.ShowDialog() != DialogResult.OK)
                return;

            if (frmLauncher.GuiType == GuiType.Console)
            {
                Process.Start("ProjectManager");
            }
            else
                Application.Run(new frmMain());
        }
    }

    public enum GuiType
    {
        Console,
        Window
    }
}
