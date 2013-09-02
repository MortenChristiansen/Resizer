using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Resizer.Views;

namespace Resizer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ResizeForm form = new ResizeForm();
            (form as IView).Initialize();
            Application.Run(form);
        }
    }
}
