using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FfmpegApp.Class
{
    public class Directory_Handler
    {

        public Directory_Handler()
        {

        }


        public string Directory_Finder()                                   // permite o usuario achar a pasta
        {
            string Folder = null;
            var openFileDialog1 = new FolderBrowserDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Folder = openFileDialog1.SelectedPath;
            }
            return Folder;
        }


        public string SearchFile(string Folder, string TypeFile)                         // acha automaticamente tipos de arquivo na pasta
        {
            DirectoryInfo DBdirectoryInfo = new DirectoryInfo(Folder);
            string File = null;

            foreach (FileInfo file in DBdirectoryInfo.GetFiles())
            {
                if (file.Extension.Contains(TypeFile))
                {
                    File = file.FullName;
                }
            }
            return File;
        }

    }
}
