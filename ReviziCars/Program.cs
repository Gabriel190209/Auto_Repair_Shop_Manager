using ReviziCars.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReviziCars
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Settings.Default.IsLogged == true)
            {
                Application.Run(new Form3());
            }
            else
            {
                Application.Run(new Form1());
            }

            Environment.Exit(0);
        }

        public static DialogResult Alert(this string txt)
        {
            return MessageBox.Show(txt, "ReviziCars", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
