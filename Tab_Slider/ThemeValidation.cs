using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tab_Slider
{
    class ThemeValidation
    {
        string CurrenTthemeFolder = Environment.CurrentDirectory + "\\Theme\\";
        public List<ThemeInfo> allthemeinfo = new List<ThemeInfo>();
        
        public ThemeValidation()
        {
            string[] themes = System.IO.Directory.GetFiles(CurrenTthemeFolder, "*.concept2theme",SearchOption.TopDirectoryOnly);    
             foreach(string t in themes)
             {
                 
                 ThemeManager themecheck = new ThemeManager(t);
                 if (themecheck.CheckThemerResources())
                 {
                     allthemeinfo.Add(themecheck.infotheme);

                 }
                 else
                 {
                     System.Windows.MessageBox.Show(themecheck.infotheme.ThemeName + " is not a valid theme ");
                 
                 }
             
             }

        
        }

    }
}
