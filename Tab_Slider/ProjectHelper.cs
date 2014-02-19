using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Text;
using System.Diagnostics;

namespace Tab_Slider
{
    class DetailStruct
    {
        static string singlequata = "#single";
        static string doublequata = "#double";               
        public static string TOXML(string path)
        {
            path = path.Replace("'", singlequata);
            path = path.Replace("\"", doublequata);

            return path;
        }
        public static string TOPATH(string path)
        {
            path = path.Replace(singlequata, "'");
            path = path.Replace(doublequata, "\"");
            return path;
        }
        public static string TrimToLegnth(string fullsting, int upto)
        {
            //try
            //{
                if (fullsting.EndsWith(":\\") || fullsting.EndsWith(":"))
                {
                    return fullsting;
                }
                if (fullsting.StartsWith("shell:"))
                { 
                    int i=0;
                    while (true)
                    {
                        SpecialPath temp= SpecialFolder.GetSPecialPath(i);
                        if (temp.addrs.Equals(fullsting))
                            return temp.name;
                        else if (temp.Equals(string.Empty))
                            break;
                        i++;                    
                    }
                    return "Unknown";
                }
                else if (!fullsting.Contains(":"))
                {
                    return fullsting;

                }
                else
                {
                    fullsting = fullsting.Substring(fullsting.LastIndexOf("\\") + 1);
                    if (fullsting.Length > upto)
                    {
                        fullsting = fullsting.Substring(0, upto - 3);
                        fullsting += "...";
                        return fullsting;
                    }
                    else
                        return fullsting;
                }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message);
            //    return "";
            
          //  }
            
        }
        public static void RunPath(string path)
        {
            Process.Start(path);
        }
        public static int GETID(string CompleteID,bool isICON)
        {
            if (isICON)
            {
                return int.Parse(CompleteID.Substring(4, CompleteID.LastIndexOf("P") - 4));

            }
            else
            {

                return int.Parse(CompleteID.Substring(CompleteID.LastIndexOf("P")+1));
            }
                
        
        }
        public static string ParseCSLID(string CLSID)
        {

            if(!CLSID.Contains("::"))
                return CLSID.Substring(CLSID.LastIndexOf("\\")+1);
            else
            switch (CLSID)
            { 
                case "::{d20ea4e1-3957-11d2-a40b-0c5020524153}":return "Administrative Tools";
                case "::{85bbd92o-42a0-1o69-a2e4-08002b30309d}":return "Briefcase";
                case "::{21ec2o2o-3aea-1o69-a2dd-08002b30309d}":return "Control Panel";
                case "::{d20ea4e1-3957-11d2-a40b-0c5020524152}":return "Fonts";
                case "::{ff393560-c2a7-11cf-bff4-444553540000}":return "History";
                case "::{00020d75-0000-0000-c000-000000000046}":return "Inbox";
                case "::{00028b00-0000-0000-c000-000000000046}":return "Microsoft Network";
                case "::{20d04fe0-3aea-1069-a2d8-08002b30309d}":return "My Computer";
                case "::{450d8fba-ad25-11d0-98a8-0800361b1103}":return "My Documents";
                case "::{208d2c60-3aea-1069-a2d7-08002b30309d}":return "My Network Places";
                case "::{1f4de370-d627-11d1-ba4f-00a0c91eedba}":return "Network Computers";
                case "::{7007acc7-3202-11d1-aad2-00805fc1270e}":return "Network Connections";
                case "::{2227a280-3aea-1069-a2de-08002b30309d}":return "Printers and Faxes";
                case "::{7be9d83c-a729-4d97-b5a7-1b7313c39e0a}":return "Programs Folder";
                case "::{645ff040-5081-101b-9f08-00aa002f954e}":return "Recycle Bin";
                case "::{e211b736-43fd-11d1-9efb-0000f8757fcd}":return "Scanners and Cameras";
                case "::{d6277990-4c6a-11cf-8d87-00aa0060f5bf}":return  "Scheduled Tasks";
                case "::{48e7caab-b918-4e58-a94d-505519c795dc}": return "Start Menu Folder";
                case "::{7bd29e00-76c1-11cf-9dd0-00a0c9034933}":return "Temporary Internet Files";
                case "::{bdeadf00-c265-11d0-bced-00a0c90ab50f}":return "Web Folders";
                default: return "Unknown";
            
            }
            
        
        }        
        public static double SizetoDouble(SizeICON size)
        {
            switch (size)
            { 
                case SizeICON.Small:
                    return 49;
                case SizeICON.Medium:
                    return 64;
                case SizeICON.Large:
                    return 96;
                
                default: return 64;
            
            }
        
        }
        public static int SizetoIndex(SizeICON size)
        {
            switch (size)
            {
                case SizeICON.Small:
                    return 0;
                case SizeICON.Medium:
                    return 1;
                case SizeICON.Large:
                    return 2;
                case SizeICON.FullLarge:
                    return 3;
                default: return 1;

            }
        }
        public static SizeICON SizeFromIndex(int sizeindex)
        {
            switch (sizeindex)
            {
                case 0:
                    return SizeICON.Small;
                case 1:
                    return SizeICON.Medium;
                case 2:
                    return SizeICON.Large;                
                
                default: return SizeICON.Medium;

            }
        }
        public static SizeICON SizeFromString(string sizeindex)
        {
            switch (sizeindex)
            {
                case "Small":
                    return SizeICON.Small;
                case "Medium":
                    return SizeICON.Medium;
                case "Large":
                    return SizeICON.Large;                
                default: return SizeICON.Medium;

            }
        }
        public static BitmapFrame CreateResizedImage(ImageSource source, int width, int height, int margin)
        {
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);

            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);

            return BitmapFrame.Create(resizedImage);
        }
    }
}
