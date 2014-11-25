using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.parser;

namespace PDFTest
{
    public class TextSharp
    {
        Document doc;
        BaseFont bfChinese;
        Font nowFont;
        public void CreateDoc(string fileName = "doc1.pdf")
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(Path.Combine(path, fileName)))
            {
                doc = new Document();

                PdfWriter.GetInstance(doc, new FileStream(path + fileName, FileMode.Create));
            }
        }
        public string ReadPdfFile(string fileName = "doc1.pdf")
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            StringBuilder text = new StringBuilder();
            string s = Path.Combine(path, fileName);//若是fileName含絕對路徑則只傳回fileName
            if (File.Exists(Path.Combine(path, fileName)))
            {
                PdfReader rd = new PdfReader(Path.Combine(path, fileName));
                for (int page = 1; page <= rd.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(rd, page, strategy);
                    text.Append(currentText);
                }
                rd.Close();
            }
            return text.ToString();
        }

        public void WritePdfFile(string content, string fileName = "doc2.pdf")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,fileName);
            using (doc = new Document())
            {
                
                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));//不管如何都重新建檔,FileMode.Append無法Append
                doc.Open();
                this.FontSetting();
                Chunk c = new Chunk(content,nowFont);
                Phrase p = new Phrase(c);
                Paragraph pg = new Paragraph(p);
                pg.SetLeading(0.0f, 2.0f);
                pg.FirstLineIndent = 20f;

                doc.Add(pg);
                doc.Close();
            }
        }
        //測試用
        public void OpenDoc(string fileName = "doc1.pdf")
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            doc = new Document();
            if (!File.Exists(Path.Combine(path, fileName)))
            {
                PdfWriter.GetInstance(doc, new FileStream(path + fileName, FileMode.Create));
            }
            else
            {
                FileStream fs = new FileStream(path + fileName, FileMode.Append);
                fs.Write(Encoding.ASCII.GetBytes("[test]"), 0, 6);
                PdfWriter.GetInstance(doc, fs);
            }
            
                doc.Open();
                Chunk c = new Chunk("My Second PDF + 9999國字測試 \n", nowFont);
                Phrase p1 = new Phrase(c);
                doc.Add(p1);

                Chunk c2 = new Chunk(" Paragraph是文章段落，可由phrases (句子)組成， 然後phrase又可以由chunks(文字片段)所組成。\n", nowFont);
                Phrase p2 = new Phrase(c2);
                doc.Add(p2);

                Chunk c3 = new Chunk("但要注意的是chunk不會自動換行，必須自行插入換行符號”\\n” 或是使用Environment.NewLine。至於Paragraph有許多樣式可以設定，像是Alignment、indentation、leading及spacing 等。\n", nowFont);
                Chunk c32 = new Chunk("使用iTextSharp 產生表格是十分直覺且容易的，類似CSS的寫法。\n", nowFont);
                Chunk c33 = new Chunk("比較值得注意的是，由於PdfPTable表格裡面，每一格叫做cell，因此在塞資料時，必須注意填寫方式是由左而右、由上而下。\n此外，PdfpCell 有合併欄位的功能Colspan，也有合併列的功能Rowspan，我們可以利用這兩項特性將平淡的表格做些變化。", nowFont);
                Phrase p3 = new Phrase();
                p3.Add(c3);
                p3.Add(c32);
                p3.Add(c33);
                
                Paragraph pg = new Paragraph();
                pg.Add(p3);
                pg.FirstLineIndent = 20f;//段落句首縮排
                pg.SetLeading(0.0f, 2.0f);//設定行距
                pg.Alignment = 5;
                doc.Add(pg);
                doc.AddTitle("介紹PDF");
                doc.AddAuthor("Knock");
                doc.AddSubject("PDF Top");
                var header = doc.AddHeader("Knock", "AddHeaderTest");
                doc.Close();

                
        }
        private void FontSetting()
        {
            if (File.Exists(@"D:\msjh.ttf"))
            {
                bfChinese = BaseFont.CreateFont(@"D:\msjh.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font chFont = new Font(bfChinese, 12);
                Font chFont_blue = new Font(bfChinese, 40, Font.NORMAL, new BaseColor(51, 0, 153));
                Font chFont_msg = new Font(bfChinese, 12, Font.ITALIC, BaseColor.RED);
                this.nowFont = chFont_msg;
            }
        }
    }
}
