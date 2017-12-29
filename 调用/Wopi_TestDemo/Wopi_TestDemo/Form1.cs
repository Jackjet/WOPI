using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WopiService_Proxy;

namespace Wopi_TestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "(doc,docx,ppt,pptx,xls,xlsx,pdf)|*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx;*.pdf;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileFullName = openFileDialog1.FileName;

                //string fileName = Path.GetFileName(fileFullName);

                string extension = Path.GetExtension(fileFullName);
                string dateNow = DateTime.Now.ToString("yyyyMMddHHmm");

                string destFileName = dateNow + extension;

                using (FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    Wopi_Proxy.UploadFile_GetLink(destFileName, data, new Action<string>((link) =>
                        {
                          
                            if(!string.IsNullOrEmpty(link))
                            {
                                this.webBrowser1.Navigate(link);
                            }
                            
                            //Wopi_Proxy.Delete_File(destFileName, new Action<bool>((isSuccessed) =>
                            //    {
                            //        if(isSuccessed)
                            //        {

                            //        }
                            //    }));
                        }));
                }

            }
        }
    }
}
