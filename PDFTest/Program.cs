using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFTest
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //TextSharp wr = new TextSharp();
            //Console.Write(wr.ReadPdfFile());
            //Console.ReadKey();
            //wr.CreateDoc();
            //wr.FontSetting();
            //wr.OpenDoc("doc1.pdf");

        }
    }
}
