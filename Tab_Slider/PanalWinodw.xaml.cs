using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using Microsoft.WindowsAPICodePack.Shell;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Tab_Slider
{
    /// <summary>
    /// Interaction logic for PanalWinodw.xaml
    /// </summary>
    public partial class PanalWinodw : Window
    {
        #region Class Object
        ThemeDetail MainTheme;
        Image AddButton=new Image();
        Image SettingButton = new Image();        
        Label TitleHolder=new Label();
        Canvas pager=new Canvas();
        Canvas C0 = new Canvas();
        Canvas C1 = new Canvas();
        Launcher parent;
        ThemeManager theme;
        Effects myeffects = new Effects();
        IconDetailsHandler alldetails = new IconDetailsHandler(Environment.CurrentDirectory + "\\demo.Concep2DB");
        double Drtn_fade;
        IconDetails[] ICONALL;        
        ContextMenu IconOption = new ContextMenu();
        #endregion
        #region Variables
        double totalpage;
        double TotalIconPerPage;
        double StackWidth=90;
        double StackHeight = 100;
        double panalSourceHeight;
        double panalSourceWidth;
        string TileText = "123456789013";
        int CurrentPage = 0;
        int PanalID = 0;
        bool isTextShadow;
        bool isIconShadow;
        double Iconsize;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent); 
        #endregion
        public PanalWinodw()
        {
            InitializeComponent();
            MenuItem DeleteICON = new MenuItem();
            MenuItem ChangeIOCN = new MenuItem();            
            DeleteICON.Header = "Delete Shortcut";                        
            ChangeIOCN.Header= "Change Shortcut ICON";
            DeleteICON.Click += new RoutedEventHandler(DeleteICON_Click);
            ChangeIOCN.Click += new RoutedEventHandler(ChangeIOCN_Click);
            IconOption.Items.Add(ChangeIOCN);
            IconOption.Items.Add(DeleteICON);
            Drtn_fade = alldetails.GetFADE();
            isTextShadow = alldetails.getSHadowText();
            isIconShadow = alldetails.GetShadowIcon();

            Iconsize = DetailStruct.SizetoDouble(alldetails.GetSizeICON());
            StackHeight = Iconsize + 36;
            StackWidth = Iconsize + 26;
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            IntPtr hWndProgMan = FindWindow("Progman", "Program Manager");
            SetParent(hWnd, hWndProgMan);
           
            
            
            
            
        }
        private string ICONADD(string PATH)
        {
            string newname;
            if (!PATH.Contains(Environment.CurrentDirectory + "\\Icons"))
            {
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\Icons\\" + PATH.Substring(PATH.LastIndexOf("\\") + 1)))
                {
                    newname = PATH.Substring(PATH.LastIndexOf("\\") + 1);
                    newname = newname.Replace(".", DateTime.Now.Millisecond.ToString() + ".");
                    MessageBox.Show(newname);
                    System.IO.File.Copy(PATH, Environment.CurrentDirectory + "\\Icons\\" + newname, true);

                }
                else
                {
                    newname = PATH.Substring(PATH.LastIndexOf("\\") + 1);
                    System.IO.File.Copy(PATH, Environment.CurrentDirectory + "\\Icons\\" + newname, true);


                }
                return newname;
            }
            else
            {
                newname = PATH.Substring(PATH.LastIndexOf("\\") + 1);
                return newname;
            }


        }        
        void ChangeIOCN_Click(object sender, RoutedEventArgs e)
        {
            string ID = (((sender as MenuItem).Parent as ContextMenu).PlacementTarget as StackPanel).Name;
            MessageBox.Show(ID);
            System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            openfile.Filter = "Only PNG Supprted(*.png)|*.png";
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openfile.FileName.EndsWith(".png"))
                { 
                        //string ID = (((sender as MenuItem).Parent as ContextMenu).PlacementTarget as StackPanel).Name;
                        string ICONID = ID.Substring(ID.IndexOf("ICON"), ID.Length - 2).Replace("ICON", ""); ;            
                        alldetails.UpdateIconImage(ICONALL[int.Parse(ICONID)].Path,ICONADD(openfile.FileName),PanalID);
                        ICONALL = alldetails.GetAllIconDetailByPanal(PanalID).ToArray();
                        DrawICONSONPAGE(CurrentPage);
                
                }
                else
                {
                    MessageBox.Show("Only PNG files are supported for IOCNS", "Unsupported Format", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
            }


            
        }

        void DeleteICON_Click(object sender, RoutedEventArgs e)
        {
            string ID = (((e.Source as MenuItem).Parent as ContextMenu).PlacementTarget as StackPanel).Name;
            string ICONID = ID.Substring(ID.IndexOf("ICON"), ID.Length - 2).Replace("ICON", ""); ;            
            alldetails.RemoveIconDetail(ICONALL[int.Parse(ICONID)].Path, PanalID);            
            ICONALL = alldetails.GetAllIconDetailByPanal(PanalID).ToArray();
            DrawICONSONPAGE(CurrentPage);
            
        }

        
        public void applyCurrenttheme(string ThemeName)
        {            
            if(ThemeName==null)
                theme = new ThemeManager(Environment.CurrentDirectory + "\\Theme\\" + "Default Glass v1.0.Concept2theme");
            else
                theme = new ThemeManager(Environment.CurrentDirectory + "\\Theme\\" + ThemeName + ".Concept2theme");

            theme.CheckThemerResources();
            MainTheme = theme.themeAttribute;            
            panalSourceHeight = MainTheme.Br_Panal.ImageSource.Height;
            panalSourceWidth = MainTheme.Br_Panal.ImageSource.Width;
            this.Height = panalSourceHeight;
            this.Width = panalSourceWidth;
            workbench.Height = this.Height;
            workbench.Width = this.Width;            
            workbench.Background = MainTheme.Br_Panal;
            AddButton.Source = MainTheme.Bs_AddButton;
            SettingButton.Source = MainTheme.bs_settings;
            AddButton.Height = 20;
            SettingButton.MaxHeight = 30;
            AddButton.Width = 50;
            SettingButton.MaxWidth = 30; 
            
        
        }
        private void PositionFixedElement()
        {
            TileText = DetailStruct.TrimToLegnth(TileText, 11);
            TitleHolder.Content = TileText;
            TitleHolder.Opacity = 0;
            TitleHolder.Foreground = Brushes.Purple;
            TitleHolder.FontWeight = FontWeight.FromOpenTypeWeight(500);
            //TitleHolder.Margin = new Thickness(MainTheme.PosXAdd, MainTheme.PosYAdd, 0, 0);            
            workbench.Children.Add(TitleHolder);
            workbench.Children.Add(SettingButton);
            //workbench.Children.Add(AddButton);
            workbench.Children.Add(pager);
            SettingButton.Stretch = Stretch.Fill;
            SettingButton.HorizontalAlignment= AddButton.HorizontalAlignment = HorizontalAlignment.Left;
            SettingButton.VerticalAlignment= AddButton.VerticalAlignment = VerticalAlignment.Top;
            AddButton.Margin = new Thickness(MainTheme.addbtnleft, MainTheme.addbtntop, 0, 0);            
            SettingButton.Margin = new Thickness(MainTheme.PosXSetng, MainTheme.PosYSetng, 0, 0);
            SettingButton.MouseLeftButtonDown += new MouseButtonEventHandler(SettingButton_MouseLeftButtonDown);
        
        }

        void SettingButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            settings SettingWindows = new settings();
            SettingWindows.cmb_holder.SelectedIndex = PanalID;
            SettingWindows.ShowDialog();
            Drtn_fade = alldetails.GetFADE();
            parent.Drtn_sld = alldetails.GetSlide();
            ICONALL = alldetails.GetAllIconDetailByPanal(PanalID).ToArray();
            DrawICONSONPAGE(CurrentPage);


            
        }
        public void CountAgain()
        {
            
            TotalIconPerPage = (int)(((theme.themeAttribute.Br_Panal.ImageSource.Width - 60) / StackWidth) * ((theme.themeAttribute.Br_Panal.ImageSource.Height- 110) / StackHeight));
            totalpage =(int) Math.Ceiling((double)(ICONALL.Length / TotalIconPerPage));
            
        }
        private void SetupPagerMargin()
        {
                    
            double temp = (MainTheme.Br_Panal.ImageSource.Width / 2-40)- (15 * totalpage / 2);
            pager.Margin = new Thickness(temp+15,MainTheme.PagerYMargin-10,0,0);
        
        }
        private void drawPager()
        {
          
            SetupPagerMargin();
            CountAgain();
            pager.Children.Clear();
            int i = 0;
            do
            {             
                Image newPage = new Image();
                newPage.Source = MainTheme.bs_notactive;
                newPage.Stretch = Stretch.UniformToFill;
                newPage.Opacity = 1;
                newPage.Height = 21;
                newPage.Width = 21;
                newPage.Margin = new Thickness((i * 22) , 6, 0, 0);
                newPage.MouseLeftButtonDown += new MouseButtonEventHandler(newPage_MouseLeftButtonDown);
                newPage.MouseEnter += new MouseEventHandler(newPage_MouseEnter);
                newPage.MouseLeave += new MouseEventHandler(newPage_MouseLeave);
                if (pager.FindName("Page" + i) != null)
                {
                    pager.UnregisterName("Page" + i);
                }
                newPage.Name = "Page" + i;
                pager.Children.Add(newPage);
                pager.RegisterName("Page" + i.ToString(), newPage);
                i++;
            } while (i < totalpage);
            (pager.FindName("Page" + 0) as Image).Source = MainTheme.bs_scrollbutn;
        
        }
        private Thickness marginGenrator(int index)
        {
            int colcount = (int)(theme.themeAttribute.Br_Panal.ImageSource.Width - 60) / (int)StackWidth;
            
            double top = (double)((StackHeight) * (int)(index / colcount));
            double left = (double)((StackWidth) * (int)(index % colcount));
            return new Thickness(left + 40, top+25 , 0, 0);
            

        }       
        private void DrawICONSONPAGE(int pageNO)
        {
            Canvas WorkingCanvas;
            //Odd PageNUmber
            if (pageNO % 2 == 1)
            {
                WorkingCanvas = C1;
                WorkingCanvas.Children.Clear();
                myeffects.Fade(C1, SliderTargetPos.front, Drtn_fade);
                myeffects.Fade(C0, SliderTargetPos.left, Drtn_fade);
                C0.Children.Clear();                               
            }
            //Even PageNumber
            else
            {
                
                WorkingCanvas = C0;
                WorkingCanvas.Children.Clear();
                myeffects.Fade(C1, SliderTargetPos.left,Drtn_fade);
                myeffects.Fade(C0, SliderTargetPos.front, Drtn_fade);
                C1.Children.Clear();
            }

            Image oldpage = (Image)pager.FindName("Page" + CurrentPage.ToString());
            if(oldpage!=null)
            myeffects.VisiblityAnimation(oldpage, false);
            //WorkingCanvas.Children.Clear();
            int count = 0;
            CurrentPage = pageNO;
            TotalIconPerPage = Math.Ceiling(TotalIconPerPage);
            
            for (int i = CurrentPage *(int) TotalIconPerPage; i < (CurrentPage + 1) * TotalIconPerPage && i < ICONALL.Length; i++)
            {
                ShellObject sfd;
                ShellFile sf;
                Image img = new Image();
                img.Stretch = Stretch.Uniform;
                if (ICONALL[i].Image == "")
                {
                        sfd = ShellObject.FromParsingName(ICONALL[i].Path);
                        if (Iconsize > 98)
                            img.Source = sfd.Thumbnail.ExtraLargeBitmapSource;
                        else if (Iconsize > 70)
                            img.Source = sfd.Thumbnail.LargeBitmapSource;
                        else
                            img.Source = sfd.Thumbnail.MediumBitmapSource;                   
                }
                else
                    img.Source=ThemeManager.imageSourceGnrator(Environment.CurrentDirectory+"\\Icons\\"+ICONALL[i].Image,Pathtype.path);
                
                StackPanel stack = new StackPanel();

                if (workbench.FindName("ICON" + i.ToString() + "P" + PanalID.ToString()) != null)
                {
                    workbench.UnregisterName("ICON" + i.ToString() + "P" + PanalID.ToString());
                }
                stack.Name = "ICON" + i.ToString() + "P" + PanalID.ToString();
                workbench.RegisterName("ICON" + i.ToString() + "P" + PanalID.ToString(), stack);                
                stack.ContextMenu = IconOption;
                
                stack.MouseEnter += new MouseEventHandler(stack_MouseEnter);
                stack.MouseLeave += new MouseEventHandler(stack_MouseLeave);
                stack.MouseLeftButtonDown += new MouseButtonEventHandler(stack_MouseLeftButtonDown);
                
                stack.HorizontalAlignment = HorizontalAlignment.Left;
                stack.VerticalAlignment = VerticalAlignment.Top;
                Label lb = new Label();
                lb.HorizontalAlignment = HorizontalAlignment.Center;
                lb.VerticalAlignment = VerticalAlignment.Center;
                img.Margin = new Thickness(0, 9, 0, 0);
                                
                OuterGlowBitmapEffect outer = new OuterGlowBitmapEffect();
                outer.GlowColor = Color.FromRgb(255, 255, 255);
                OuterGlowBitmapEffect outer2 = new OuterGlowBitmapEffect();
                outer2.GlowColor = Color.FromRgb(255, 255, 255);
                outer2.GlowSize = 7;
                outer2.Opacity = .5;
                stack.Margin = marginGenrator(count);
                DropShadowEffect Shadow = new DropShadowEffect();
                Shadow.Color = Color.FromRgb(0, 0, 0);
                Shadow.ShadowDepth = 3.3;
                Shadow.BlurRadius = 3.9;
                Shadow.Opacity = .7; 

                outer.GlowSize = 9;
                outer.Opacity = .3;
                                
                
                string s = DetailStruct.TrimToLegnth(ICONALL[i].Path,14);
                img.VerticalAlignment = VerticalAlignment.Center;
                
                img.Height = Iconsize;
                img.Width = Iconsize;
                lb.Content = s;
                lb.Effect = Shadow;
                lb.Margin = new Thickness(0, -4, 0, 0);
                lb.Foreground = Brushes.White;
                stack.Width = StackWidth;
                stack.Height = StackHeight;

                
                #region Handover to Main Widows
                stack.Children.Add(img);
                stack.Children.Add(lb);
                WorkingCanvas .Children.Add(stack);
                #endregion
                count++;
                //animateMouseInFade(oldpage, 0);



            }

        
        
        }        
        #region ICON EVENT

        void stack_MouseLeave(object sender, MouseEventArgs e)
        {
           /// myeffects.BackGroudANima(((StackPanel)sender), false);
           ((StackPanel)sender).Background = null;   
        }

        void stack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            alldetails.UpdateCount(PanalID, ICONALL[DetailStruct.GETID(((StackPanel)sender).Name, true)].Path, ICONALL[DetailStruct.GETID(((StackPanel)sender).Name, true)].runCount+1);
            DetailStruct.RunPath( ICONALL[DetailStruct.GETID(((StackPanel)sender).Name, true)].Path);
        }

        void stack_MouseEnter(object sender, MouseEventArgs e)
        {
            ((StackPanel)sender).Background = MainTheme.Br_IconActive;
           //myeffects.BackGroudANima(((StackPanel)sender), true);
            
            
        }
        #endregion
        #region Pager Events

        void newPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             int PageNO= int.Parse( ((Image)sender).Name.Replace("Page",""));
             DrawICONSONPAGE(PageNO);
            
        }

        void newPage_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = theme.themeAttribute.bs_notactive;
            //myeffects.VisiblityAnimation((Image)sender, false);
            //myeffects.ResizeAnimation((Image)sender, false, 18);
        }

        void newPage_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Source = theme.themeAttribute.bs_scrollbutn;
           // myeffects.VisiblityAnimation((Image)sender, true);
          //  myeffects.ResizeAnimation((Image)sender, true, 18);
        }
        #endregion


        public void setICON(int holderID)
        {
            CurrentPage = 0;
            ICONALL = alldetails.GetAllIconDetailByPanal(holderID).ToArray();
            PanalID = holderID;
            drawPager();
            DrawICONSONPAGE(CurrentPage);
        
        }
        public void SettheTheme()
        {
            applyCurrenttheme(null);
            PositionFixedElement();
            workbench.Children.Add(C0);
            workbench.Children.Add(C1);
            ICONALL = alldetails.GetAllIconDetailByPanal(0).ToArray();
            CountAgain();
        }
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            IntPtr hWndProgMan = GetDesktopWindow();
            SetParent(hWnd, hWndProgMan);

        }
        public void IMParent(Launcher win)
        {
            parent = win;
        }

        private void Window_Drop_1(object sender, DragEventArgs e)
        {
            string[] droppedfile = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (MessageBox.Show("Total " + droppedfile.Length.ToString() + " objects are dropped.\n Do you really want to add? ", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            { 
                foreach(string file in droppedfile)
                {
                    alldetails.AddIconDetail("", true, file, PanalID);
                }
                ICONALL = alldetails.GetAllIconDetailByPanal(PanalID).ToArray();
                drawPager();
                DrawICONSONPAGE(CurrentPage);
            }
               
        }
        private void Window_Deactivated_1(object sender, EventArgs e)
        {            
        }

        

    }
}

