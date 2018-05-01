/*
 * FILENAME     : Proram.cs
 * PROJECT      : BI_A01
 * PROGRAMMER   : Jody Markic
 * FIRST VERSION: 2017-09-21
 * DESCRIPTION  : This file hold sthe main entry point into the application BI_A01
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BI_A01
{
    //
    // CLASS: Program
    // DESCRIPTION: Main entry point into the BI_A01 application
    //
    //
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        //
        // METHOD: Main()
        // DESCRIPTION: The Main Method
        //
        //
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyForm());
        }
    }
}
