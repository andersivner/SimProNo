﻿using System;
using System.Windows.Forms;
namespace WindowsApplication2
{
    static class Program
    {
        ///
        /// The main entry point for the application.
        ///
        [STAThread]
        static void Main(string[] args)
        {
            NUnit.Gui.AppEntry.Main(args);
        }
    }
}