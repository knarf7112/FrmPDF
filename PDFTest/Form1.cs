using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFTest
{
    public partial class Form1 : Form
    {
        private string path;
        private TextSharp pdf;
        public Form1()
        {
            InitializeComponent();
            path = AppDomain.CurrentDomain.BaseDirectory;
            this.openFileDialog1.Filter = "PDF File(*.pdf)|*.pdf";
            this.openFileDialog1.ShowHelp = true;
            this.openFileDialog1.InitialDirectory = @"d:\";
            pdf = new TextSharp();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.txtContent.Text = "";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string text = pdf.ReadPdfFile(this.openFileDialog1.FileName);
                this.txtContent.Text = text;
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.DefaultExt = "pdf";
            this.saveFileDialog1.Filter = "PDF File(*.pdf)|*.pdf";
            string special = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//取得桌面路徑

            string ss =  this.txtContent.Text;
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //使用Stream只能寫成txt檔//流程是先開啟一個空的Stream並設定存檔名稱-->將byte[]資料寫入MemoryStream-->再把MemoryStream的Position位置設為0-->再將MemoryStream內的資料寫入Stream
                //using (Stream sm = this.saveFileDialog1.OpenFile())
                //{
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        byte[] tmp = Encoding.Default.GetBytes(ss);
                //        ms.Write(tmp, 0, tmp.Length);
                //        ms.Position = 0;
                //        ms.WriteTo(sm);
                //    }
                //}
                if (this.txtContent.TextLength > 0)
                {
                    pdf.WritePdfFile(this.txtContent.Text, this.saveFileDialog1.FileName);
                }
                
            }
        }

    }
}
