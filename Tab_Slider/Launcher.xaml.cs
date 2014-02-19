using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Tab_Slider
{
    /// <summary>
    /// Interaction logic for Launcher.xaml
    /// </summary>
    public partial class Launcher : Window
    {
        int count = 0;
        int topmargin = 70;
        int HolderGap = 0;
        int active = -1;
        int IconRightMargin = 20;
        int IconTopMargin = 10;
        double leftmargin = -110;
        double HeightCnterScreen;
        double WidthCenterScreen;
        double HLDRWidth = 200;
        double HLDRHeight = 90;
        double HolderIconHeight = 70;
        double HolderIconWidth = 70;
        const string appname = "Tab Slider Beta Version";
        string CurrentPath = Environment.CurrentDirectory;        
        string Defaultheme = "Default Glass v1.0";        
        System.Windows.Forms.NotifyIcon mainIOCN = new System.Windows.Forms.NotifyIcon();
        IconDetailsHandler Concept2DB = new IconDetailsHandler(Environment.CurrentDirectory + @"\demo.Concep2DB");
        public double Drtn_sld;        
        Effects myeffects = new Effects();
        PanalWinodw panal = new PanalWinodw();
        System.Windows.Forms.ContextMenu iconMenu =new System.Windows.Forms.ContextMenu();
        ThemeManager theme;               
        bool errHICO = false;
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        IntPtr windowHAndle;
        settings setwindows=new settings();
        
        public void IntializeExtras()
        {
            windowHAndle = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            System.Windows.Forms.MenuItem AddHolders_Menu = new System.Windows.Forms.MenuItem();
            AddHolders_Menu.Text = "Holder Settings";
            AddHolders_Menu.Click += AddHolders_Menu_Click;
            System.Windows.Forms.MenuItem ChangeSetting_Menu = new System.Windows.Forms.MenuItem();
            ChangeSetting_Menu.Text = "Change Settings";
            ChangeSetting_Menu.Click += ChangeSetting_Menu_Click;
            System.Windows.Forms.MenuItem About_Menu = new System.Windows.Forms.MenuItem();
            About_Menu.Click += About_Menu_Click;
            About_Menu.Text = "About";
            System.Windows.Forms.MenuItem contact_menu = new System.Windows.Forms.MenuItem();
            contact_menu.Text = "Contact Developer";
            mainIOCN.ContextMenu = iconMenu;
            contact_menu.Click += contact_menu_Click;
            System.Windows.Forms.MenuItem Exit_Menu = new System.Windows.Forms.MenuItem();
            Exit_Menu.Text = "Exit";
            Exit_Menu.Click += Exit_Menu_Click;
            iconMenu.MenuItems.Add(AddHolders_Menu);
            iconMenu.MenuItems.Add(ChangeSetting_Menu);
            iconMenu.MenuItems.Add(About_Menu);
            iconMenu.MenuItems.Add(contact_menu);
            iconMenu.MenuItems.Add(Exit_Menu);
        
        }        
        




        public Launcher()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Can run only one instance of application at a time");
                Application.Current.Shutdown();
            }

            InitializeComponent();            
            IntializeMainWindow();                       
            IntializeVisual();
            IntializeExtras();            
            mainIOCN.ShowBalloonTip(10000, "Tab Slider Beta Version", "Use tab Slider notification icon to configure slider..Right click on Dekstop in view option \n*UNCHECK Show Desktop icon*", System.Windows.Forms.ToolTipIcon.None);
        }

        void Exit_Menu_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to close this application", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                Application.Current.Shutdown();
            
            }
        }

        void contact_menu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Redirecting to developer's facebook profile....\nGive your Suggetions to improve this concept.. You can message me..");
            System.Diagnostics.Process.Start("https://www.facebook.com/yogender.lucky");
        }

        void About_Menu_Click(object sender, EventArgs e)
        {
            ADDICON about = new ADDICON();
            about.ShowDialog();            
        }

        void ChangeSetting_Menu_Click(object sender, EventArgs e)
        {
            setwindows.tabControl1.SelectedIndex = 0;
            setwindows.ShowDialog();
        }

        void AddHolders_Menu_Click(object sender, EventArgs e)
        {
            
            setwindows.tabControl1.SelectedIndex = 1;
            setwindows.ShowDialog();
            this.Window_Loaded(null, null);
        }       
        
        public void IntializeMainWindow()
        {
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.g1.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.g1.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Top = 0;
            this.Left = 0;
            this.WindowState = WindowState.Maximized;

            ClipToBounds = false;
            Drtn_sld = Concept2DB.GetSlide();
            try
            {
                //MessageBox.Show(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                mainIOCN.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mainIOCN.BalloonTipText = appname + " is minimized here.. for more option\n Please Right Click the Icon and Choose Option";
            mainIOCN.BalloonTipTitle = "Pay Attention!!";
            mainIOCN.Visible = true;
            theme = new ThemeManager(CurrentPath + "\\Theme\\" + Defaultheme + ".Concept2theme");
           
            
        
        }
        public void IntializeVisual()
        {
            bool status = theme.CheckThemerResources();
            if (!status)
                if (MessageBox.Show(status + "\n Do you want to continue..", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
                    this.Close();
            //MessageBox.Show(panal.Height.ToString());
           WidthCenterScreen = (System.Windows.SystemParameters.PrimaryScreenWidth / 2) - (theme.themeAttribute.Br_Panal.ImageSource.Width/2);
           HeightCnterScreen = (System.Windows.SystemParameters.PrimaryScreenHeight / 2) - (theme.themeAttribute.Br_Panal.ImageSource.Height / 2);
           panal.Left = -panal.Width - 100;
           panal.Top = HeightCnterScreen;
           panal.IMParent(this);
           panal.SettheTheme();
           Win32.SetBottomMost(this); 
        
        }
        #region Holder Deletion Postwork
        public void FillSpace(int deleted)
        {
            for (int i = deleted; ; i++)
            {

                Canvas temp = (Canvas)g1.FindName("B" + (i + 1).ToString());
                if (temp == null || i == count)
                    break;
                temp.Name = "B" + (i).ToString();
                g1.RegisterName("B" + (i).ToString(), temp);
                g1.UnregisterName("B" + (i + 1).ToString());
                ThicknessAnimation thk = new ThicknessAnimation();

                thk.To = new Thickness(leftmargin, temp.Margin.Top - temp.Height - HolderGap, 0, 10);
                thk.From = temp.Margin;
                thk.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                temp.BeginAnimation(Canvas.MarginProperty, thk);



            }
            count--;

        }
        #endregion
        public bool CreateHolder(string imagepath, string name)
        {
            
            Canvas b = new Canvas();
            Image img = new Image();
            b.Name = "B" + count;                   
            b.MouseEnter += new MouseEventHandler(b_MouseEnter);
            b.MouseLeave += new MouseEventHandler(b_MouseLeave);
            b.MouseLeftButtonDown += new MouseButtonEventHandler(b_MouseLeftButtonDown);            
            b.Background = theme.themeAttribute.Br_Holder;                       
            b.VerticalAlignment = VerticalAlignment.Top;
            b.Width = HLDRWidth;
            b.Height = HLDRHeight;
            img.Width = HolderIconWidth;
            img.Height = HolderIconHeight;
            img.FlowDirection = FlowDirection.LeftToRight;
            img.Source = DetailStruct.CreateResizedImage(ThemeManager.imageSourceGnrator(imagepath, Pathtype.path), 72, 72, 0);
            img.Stretch = Stretch.None;
            b.Children.Add(img);
            if (g1.FindName(b.Name) != null)
                g1.UnregisterName(b.Name);
            g1.RegisterName(b.Name, b);
            g1.Children.Add(b);
            
            if (img.Source == null)
                img.Source = ThemeManager.imageSourceGnrator(ImageConstatnts.NotFoundImage, Pathtype.package);            
                b.HorizontalAlignment = HorizontalAlignment.Left;
                b.Margin = new Thickness(leftmargin, topmargin +count * (b.Height + HolderGap), 0, 10);
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.Margin = new Thickness(b.Width - img.Width - IconRightMargin, IconTopMargin, 0, 0);
            
            

            
            
            count++;
            return true;

        }
        
        
        void b_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (active == int.Parse(((Canvas)sender).Name.Substring(1)))
            {
                active = -1;
                myeffects.WIdowsSlidBack(panal,Drtn_sld);

            }
            else
            {
                active = int.Parse(((Canvas)sender).Name.Substring(1));
                for (int i = 0; i < count; i++)
                {
                    if (active != i)
                    {
                        Canvas b = (Canvas)this.g1.FindName("B" + i);
                        myeffects.MouseNHolder(b,new Thickness(leftmargin, topmargin + i * (b.Height + HolderGap), 0, 10),false);
                        
                    }
                }
                
                panal.setICON(active);                
                panal.Show();
                myeffects.NewWIdowsSlidFront(panal, WidthCenterScreen,Drtn_sld);

            
            }
        }

        void b_MouseLeave(object sender, MouseEventArgs e)
        {
            
            Canvas b = ((Canvas)sender);                                    
            int bno = int.Parse(((Canvas)sender).Name.Substring(1));
            Thickness myMargin = new Thickness(leftmargin,topmargin+ bno * (b.Height + HolderGap), 0, 10);
            if (bno != active)
                myeffects.MouseNHolder(b, myMargin, false);
            
           
            
        }

        void b_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas b = ((Canvas)sender);
            int bno = int.Parse(((Canvas)sender).Name.Substring(1));
            
            
            Thickness toMargin = new Thickness(leftmargin + 40, topmargin + bno * (b.Height + HolderGap), 0, 10);
            myeffects.MouseNHolder(b, toMargin, true);
            
        }
        private void startup()
        {
            
           Microsoft.Win32.RegistryKey key =Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                       @"Software\Microsoft\Windows\CurrentVersion\Run", true);                       
           key.SetValue("Tab Slider",System.Diagnostics.Process.GetCurrentProcess().ProcessName+".exe");            

            key.Close();
        }
        public void ADDFIRSTRUN()
        {
            
            startup();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Concept2DB.AddHolder("System", 0, "System.PNG");
            Concept2DB.AddHolder("Shortcut", 1, "Shortcut.PNG");
            Concept2DB.AddHolder("Files", 2, "files.PNG");
            Concept2DB.AddIconDetail("", false, "shell:MyComputerFolder", 0);
            Concept2DB.AddIconDetail("", false, "shell:RecycleBinFolder", 0);
            Concept2DB.AddIconDetail("", false, "shell:Personal", 0);
            Concept2DB.AddIconDetail("", false, "shell:My Video", 0);
            Concept2DB.AddIconDetail("", false, "shell:My Pictures", 0);
            Concept2DB.AddIconDetail("", false, "shell:Games", 0);
            Concept2DB.AddIconDetail("", false, "shell:ConnectionsFolder", 0);
            foreach (string file in System.IO.Directory.GetFiles(desktop))
            {
                if(file.EndsWith(".lnk"))
                Concept2DB.AddIconDetail("", false, file, 1);
                else
                Concept2DB.AddIconDetail("", false, file, 2);
            
            }
            Window_Loaded(null, null);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            count = 0;
             
            List<HolderDetails> allholder = Concept2DB.GetHolderDetails();

            if (allholder.Count == 0)
            {
                MessageBox.Show("Thank you for choosing Tab Slider..\n Your review will be helpful.....","Thank you",MessageBoxButton.OK,MessageBoxImage.Information);
                ADDFIRSTRUN();

            }
            else
            {


                //IntializeVisual();
                g1.Children.Clear();
                foreach (HolderDetails holder in allholder)
                    CreateHolder(CurrentPath + "\\icons\\" + holder.Image, holder.Name);

                WindowInteropHelper wndHelper = new WindowInteropHelper(this);

                int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);

                exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
                SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);

                SetWindowPos(new WindowInteropHelper(this).Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);
            }
            active = 0;
            Canvas b = (Canvas)FindName("B0");
            Thickness toMargin = new Thickness(leftmargin + 40, topmargin + 0 * (b.Height + HolderGap), 0, 10);
            myeffects.MouseNHolder(b, toMargin, true);
            panal.setICON(0);
            panal.Show();
            myeffects.NewWIdowsSlidFront(panal, WidthCenterScreen, Drtn_sld);


        }
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
        int Y, int cx, int cy, uint uFlags);
        const int WM_WINDOWPOSCHANGING = 0x0046;


        #region Window styles
        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent); 
        
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        

        private void Window_GotFocus_1(object sender, RoutedEventArgs e)
        {
            

        }

        private void Window_StateChanged_1(object sender, EventArgs e)
        {

        }        
        
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
        }
        }

        
}
