using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Tab_Slider
{
    public struct ThemeDetail
    {
        public ImageBrush Br_Panal;
        public ImageBrush Br_Holder;
        public ImageBrush Br_IconActive;
        public ImageSource Bs_AddButton;        
        public ImageSource bs_scrollbutn;
        public ImageSource bs_settings;
        public ImageSource bs_notactive;
        public double addbtnleft;
        public double addbtntop;
        public bool isaddbtnright;
        
        public double PosXAdd;
        public double PosYAdd;
        public double PosXSetng;
        public double PosYSetng;        
        public double PagerYMargin;
        


    };
    public struct SpecialPath
    {
        public string addrs;
        public string name;
    }
    public struct ImageConstatnts
    {
        public const string NotFoundImage = "/Tab%20Slider%20v1;component/Images/errorimg.PNG";
        public const string FileOnDesktop = "";
        public const string DesktopShortcut = "";
        public const string SystemPath = "";
    
    
    }
    
    public enum Pathtype
    { 
       package,path
    }
    public enum SizeICON
    { 
    Small,Medium,Large,FullLarge
    }
    public struct ThemeInfo
    {
        public string ThemeName;
        public string AuthorName;
        public string ThemeCopyright;
        public bool themeValid;
        
    };     
    
    public struct HolderDetails
    {
        public int id;
        public string Name;
        public string Image;        
    
    };
    public struct IconDetails
    {
        public bool isFile;
        public string Path;
        public string Image;
        public int runCount;


    };
    public enum SliderTargetPos
    { 
    left,front
    }
}

