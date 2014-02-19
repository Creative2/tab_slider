using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Tab_Slider
{
    class ThemeManager 
    {
        string themefile;
        XmlDocument xdoc;
        
        public ThemeDetail themeAttribute = new ThemeDetail();
        public ThemeInfo infotheme = new ThemeInfo();
        string currentpath;
        string themepath;
        string themename;
        public bool isIntialised=false;

        public ThemeManager(string themefilepath)
        {
            themefile = themefilepath;
            xdoc = new XmlDocument();
            xdoc.Load(themefile);
            currentpath = Environment.CurrentDirectory + "\\Theme";
            themepath = currentpath + "\\" + themefile.Substring(themefile.LastIndexOf(@"\") + 1);
            themename = themepath.Substring(themepath.LastIndexOf("\\") + 1);
            themename = themename.Remove(themename.LastIndexOf("."));

        }
        public static ImageSource imageSourceGnrator(string path,Pathtype pt)
        {
            try
            {
                if (pt == Pathtype.path)
                {                
                    return new BitmapImage(new Uri(path));
                }
                else
                {
                    return new BitmapImage(new Uri(path, UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error in image..");
                return null;
            
            }
            
        }
        public bool CheckThemerResources()
        {
            try
            {
                XmlNode main = xdoc.SelectSingleNode("themeInfo");
                XmlNode SPBG = main.SelectSingleNode("SliderPanalBG");
                XmlNode SPICONBG = main.SelectSingleNode("SlidePanalIconBG");
                XmlNode Settings = main.SelectSingleNode("settings");
                XmlNode HolderBG = main.SelectSingleNode("HolderBG");
                XmlNode scroolbtn = main.SelectSingleNode("SlidePnanalScrollButn");
                XmlNode addbt = main.SelectSingleNode("SlidePanalAddButton");
                XmlNode title = main.SelectSingleNode("titletext");
                XmlNode notactive = main.SelectSingleNode("NotActive");
                infotheme.AuthorName = main.Attributes[1].Value.ToString();
                infotheme.ThemeCopyright = main.Attributes[2].Value.ToString();
                infotheme.ThemeName = main.Attributes[0].Value.ToString();
                themeAttribute.Br_Panal = new ImageBrush(imageSourceGnrator(currentpath + "\\" + themename + "\\" + SPBG.Attributes["path"].Value.ToString(),Pathtype.path));
                themeAttribute.Br_IconActive = new ImageBrush(imageSourceGnrator(currentpath + "\\" + themename + "\\" + SPICONBG.Attributes["path"].Value.ToString(), Pathtype.path));
                themeAttribute.Br_Holder = new ImageBrush(imageSourceGnrator(currentpath + "\\" + themename + "\\" + HolderBG.Attributes["path"].Value.ToString(), Pathtype.path));
                themeAttribute.bs_scrollbutn = imageSourceGnrator(currentpath + "\\" + themename + "\\" + scroolbtn.Attributes["path"].Value.ToString(), Pathtype.path);
                themeAttribute.bs_settings = imageSourceGnrator(currentpath + "\\" + themename + "\\" + Settings.Attributes["path"].Value.ToString(), Pathtype.path);
                themeAttribute.bs_notactive = imageSourceGnrator(currentpath + "\\" + themename + "\\" + notactive.Attributes["path"].Value.ToString(), Pathtype.path);
                themeAttribute.PosXAdd = double.Parse(title.Attributes["PosX"].Value.ToString());
                themeAttribute.PosYAdd = double.Parse(title.Attributes["PosY"].Value.ToString());
                themeAttribute.PosXSetng = double.Parse(Settings.Attributes["posx"].Value.ToString());
                themeAttribute.PosYSetng = double.Parse(Settings.Attributes["posy"].Value.ToString());
                themeAttribute.addbtnleft = double.Parse(addbt.Attributes["left"].Value);
                themeAttribute.addbtntop = double.Parse(addbt.Attributes["top"].Value);
                string temp = scroolbtn.Attributes["vertical"].Value.ToString();                                
                themeAttribute.PagerYMargin = themeAttribute.Br_Panal.ImageSource.Height - 40;
                

                if (addbt.Attributes["loc"].Value.Equals("r"))
                    themeAttribute.isaddbtnright = true;
                else
                    themeAttribute.isaddbtnright = false;
                themeAttribute.Bs_AddButton = imageSourceGnrator(currentpath + "\\" + themename + "\\" + addbt.Attributes["path"].Value.ToString(), Pathtype.path);
                isIntialised = true;
                return true;
            }
            catch (Exception e)
            {
                return false;
            
            }
         



        }
        


    }
}
