using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tab_Slider
{
    class c2theme
    {
        string themeinputpath;
        string thmeoutputpath=Environment.CurrentDirectory+"\\themedumy\\";
        public string themename;
        public c2theme(string themepackpath)
        {
             themename= themepackpath.Substring(themepackpath.LastIndexOf("\\") + 1);
            themename = themename.Substring(0, themename.LastIndexOf("."));
            System.Windows.Forms.MessageBox.Show(themename);
            System.IO.File.Copy(themepackpath, thmeoutputpath + themename + ".concept2theme",true);
            System.IO.Directory.CreateDirectory(thmeoutputpath+themename);
            sys
            
        
        
        }

    }
}
