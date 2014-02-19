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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace Tab_Slider
{
    /// <summary>
    /// Interaction logic for settings.xaml
    /// </summary>
    public partial class settings : Window
    {
        IconDetailsHandler Concept2DB = new IconDetailsHandler(Environment.CurrentDirectory + @"\demo.Concep2DB");
        string IOCNFOLDER = Environment.CurrentDirectory + "\\ICOns\\";
        string NewPath = null;
        string changedImage = null;
        //[DllImport("user32.dll")]
        //static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        //[DllImport("user32.dll")]
        //static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        //private const int GWL_STYLE = -16;

        //private const uint WS_SYSMENU = 0x80000;

        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        //    SetWindowLong(hwnd, GWL_STYLE,
        //        GetWindowLong(hwnd, GWL_STYLE) & (0xFFFFFFFF ^ WS_SYSMENU));

        //    base.OnSourceInitialized(e);
        //}
        public settings()
        {
            InitializeComponent();
            
            chk_iconShadow.IsChecked = Concept2DB.GetShadowIcon();
            chk_textshadow.IsChecked = Concept2DB.GetShadowIcon();
            sld_fade.Value = Concept2DB.GetFADE();
            sld_slide.Value = Concept2DB.GetSlide();
            cmb_size.SelectedIndex = DetailStruct.SizetoIndex(Concept2DB.GetSizeICON());
            int i = 0;
            while (true)
            {
                SpecialPath current= SpecialFolder.GetSPecialPath(i);
                if (current.name.Equals(string.Empty))
                {
                    break;
                }
                else
                    cmb_special.Items.Add(current.name);
                i++;

            }

        }
        void LoadThemesInfo()
        {
            ThemeValidation validation = new ThemeValidation();
            cmb_themelist.Items.Clear();
            foreach (ThemeInfo ti in validation.allthemeinfo)
            {
                cmb_themelist.Items.Add(ti.ThemeName);

            }
        }
        void LoadHolderDetails()
        {
            cmb_holder.Items.Clear();
            List<HolderDetails> allhoder= Concept2DB.GetHolderDetails();
            foreach (HolderDetails hldr in allhoder)
            {
                cmb_holder.Items.Add((hldr.id+1) + ". " + hldr.Name);
            
            }
            hldername.Text = "";
            img_edit.IsEnabled = false;            
            btn_remove.IsEnabled = false;
            
            img_edit.Source = new BitmapImage(new Uri(ImageConstatnts.NotFoundImage, UriKind.Relative));
            
            
        
        
        }             
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
            // cmb_themelist.SelectedIndex= cmb_themelist.Items.IndexOf("Default Glass v1.0");    //CHage With Default Theme
            LoadHolderDetails();
            LoadThemesInfo();
             
            
            
        }
        private void cmb_themelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemeValidation validation = new ThemeValidation();
            rct_themedeatilbox.Document.Blocks.Clear();
            rct_themedeatilbox.AppendText("Author Name-" + validation.allthemeinfo[cmb_themelist.SelectedIndex].AuthorName);
            rct_themedeatilbox.AppendText("\nTheme Name-" + validation.allthemeinfo[cmb_themelist.SelectedIndex].ThemeName);
            rct_themedeatilbox.AppendText("\nInstruction By Author-" + validation.allthemeinfo[cmb_themelist.SelectedIndex].ThemeCopyright);

        }
        private void cmb_holder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_holder.SelectedIndex < cmb_holder.Items.Count && cmb_holder.SelectedIndex >=0)
            {
                HolderDetails seleted = Concept2DB.GetHolderDetails()[cmb_holder.SelectedIndex];
                hldername.Text = seleted.Name;
                hldername.Text = "";
                img_edit.IsEnabled = true;
                bt_update.IsEnabled = true;
                btn_remove.IsEnabled = true;
                hldername.Text = seleted.Name;               
                

                img_edit.Source = DetailStruct.CreateResizedImage(ThemeManager.imageSourceGnrator(IOCNFOLDER + seleted.Image, Pathtype.path), 90, 90, 0);
                
                LoadIOCNIST();
            }
        }
        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if(cmb_holder.SelectedIndex>=0)
                if (MessageBox.Show("You are about to delete a Holder and Its Related All Shortcut", "Tab Slider -Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == System.Windows.MessageBoxResult.Yes)
                {
                    
                    Concept2DB.DeletePanal(cmb_holder.SelectedIndex, cmb_holder.Items.Count);
                    //cmb_holder.SelectedIndex = 0;
                    
                }
            
            LoadHolderDetails();

        }   
        private void img_new_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openfile=new System.Windows.Forms.OpenFileDialog();
            openfile.Filter = "Supported Image(*.png)|*.png";
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openfile.FileName.EndsWith(".png") || openfile.FileName.EndsWith(".PNG"))
                {
                    img_edit.Source = DetailStruct.CreateResizedImage(ThemeManager.imageSourceGnrator(openfile.FileName, Pathtype.path), 90, 90, 0);
                    NewPath = openfile.FileName;
                }
                else
                    MessageBox.Show("File Not Supported");
            }
        }
        private string ICONADD(string PATH)
        {
            string newname;
            if (!NewPath.Contains(Environment.CurrentDirectory + "\\Icons"))
            {
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\Icons\\" + NewPath.Substring(NewPath.LastIndexOf("\\") + 1)))
                {
                    newname = NewPath.Substring(NewPath.LastIndexOf("\\") + 1);
                    newname = newname.Replace(".", DateTime.Now.Millisecond.ToString() + ".");                    
                    System.IO.File.Copy(NewPath, Environment.CurrentDirectory + "\\Icons\\" + newname, true);

                }
                else
                {
                    newname = NewPath.Substring(NewPath.LastIndexOf("\\") + 1);
                    System.IO.File.Copy(NewPath, Environment.CurrentDirectory + "\\Icons\\" + newname, true);


                }
                return newname;
            }
            else
            {
                newname = NewPath.Substring(NewPath.LastIndexOf("\\") + 1);
                return newname;
            }
        
        
        }        
        private void bt_update_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_holder.SelectedIndex > -1)
            {
                if (changedImage != null)
                    Concept2DB.Updatepanal(cmb_holder.SelectedIndex, changedImage, hldername.Text);
                else
                    Concept2DB.Updatepanal(cmb_holder.SelectedIndex, Concept2DB.GetHolderDetails()[cmb_holder.SelectedIndex].Image, hldername.Text);

                MessageBox.Show("Updated Successfully", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadHolderDetails();
            }
            else
                MessageBox.Show("Please select holder first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }               
        private void sld_slide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbl_Slide.Content =((int) sld_slide.Value).ToString() + " MS";
            
        }
        private void sld_fade_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbl_Fade.Content = ((int)sld_fade.Value) + " MS";
            
        }
        private void btn_effectSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Concept2DB.updateShadwo(chk_textshadow.IsChecked.Value, chk_iconShadow.IsChecked.Value);
                Concept2DB.updateSize(cmb_size.SelectedIndex);
                Concept2DB.UpdateSlideEffect((int)sld_slide.Value, (int)sld_fade.Value);                               
                if (MessageBox.Show("Panal Rendring Properties Successfully Saved..Please Restrart Application to Make Effects Working..\n Do You Want To Restart Application", "Succesfully Updates-Restart Required", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                  System.Windows.Forms.Application.Restart();
                
                }
            }
            catch
            { 
            
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            openfile.Filter = "Supported Image(*.png)|*.png";
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openfile.FileName.EndsWith(".png") || openfile.FileName.EndsWith(".PNG"))
                {
                    img_edit.Source = DetailStruct.CreateResizedImage(ThemeManager.imageSourceGnrator(openfile.FileName, Pathtype.path), 90, 90, 0);
                    NewPath = openfile.FileName;
                }
                else
                    MessageBox.Show("File Not Supported");
            }
            changedImage = ICONADD(NewPath);
            

        }

        private void rd_spcl_Checked(object sender, RoutedEventArgs e)
        {
            cmb_special.IsEnabled = true;
        }
        public void LoadIOCNIST()
        {
            lst_icons.Items.Clear();
            foreach (IconDetails icon in Concept2DB.GetAllIconDetailByPanal(cmb_holder.SelectedIndex))
            {
                if (icon.isFile)
                    lst_icons.Items.Add(icon.Path.Substring(icon.Path.LastIndexOf("\\") + 1));
                else
                    lst_icons.Items.Add(icon.Path);
            
            }
        
        }
        public string[] files;
        private void bt_browse_Click(object sender, RoutedEventArgs e)
        {
            if (rd_files.IsChecked.Value)
            {

                System.Windows.Forms.OpenFileDialog filedilog = new System.Windows.Forms.OpenFileDialog();
                filedilog.Multiselect=true;
                if (filedilog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tx_path.Text = "Total " + filedilog.FileNames.Length + " selected";
                    files = filedilog.FileNames;
                }
                
            
            }
            else if (rd_fldr.IsChecked.Value)
            {
                System.Windows.Forms.FolderBrowserDialog folderdilog = new System.Windows.Forms.FolderBrowserDialog();
                if (folderdilog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    tx_path.Text = folderdilog.SelectedPath;
                }            
                LoadIOCNIST();
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (rd_spcl.IsChecked.Value)
            {
                Concept2DB.AddIconDetail("", false, SpecialFolder.GetSPecialPath(cmb_special.SelectedIndex).addrs, cmb_holder.SelectedIndex);
                LoadIOCNIST();
            }
            else if (rd_fldr.IsChecked.Value)
            {
                if (tx_path.Text != "" && !tx_path.Text.StartsWith("Total"))
                {
                    Concept2DB.AddIconDetail("", false, tx_path.Text, cmb_holder.SelectedIndex);
                    tx_path.Text = "";
                    LoadIOCNIST();
                }
                else
                    MessageBox.Show("Select only folder or Use file option.", "Stop", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (rd_files.IsChecked.Value)
            {
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        Concept2DB.AddIconDetail("", true, file, cmb_holder.SelectedIndex);
                    }
                    files = null;
                    tx_path.Text = "";
                    LoadIOCNIST();
                }
                else
                {

                    MessageBox.Show("Please add Files first ", "Stop", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
            
            }


        }

        private void bt_addnew_Click(object sender, RoutedEventArgs e)
        {
            
                if (NewPath != null)
                {
                    string newname = ICONADD(NewPath);                    
                        Concept2DB.AddHolder(hldername.Text, Concept2DB.GetHolderDetails().Count, newname);                                        
                    NewPath = null;
                    LoadHolderDetails();

                }
                else if (NewPath == null)
                {
                    MessageBox.Show("Please choose icon image first");
                    

                }
            
            
        }

        private void lst_icons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && lst_icons.SelectedIndex > -1 && cmb_holder.SelectedIndex>-1)
            {
                //MessageBox.Show(Concept2DB.GetAllIconDetailByPanal(cmb_holder.SelectedIndex)[lst_icons.SelectedIndex].Path);
                string path = Concept2DB.GetAllIconDetailByPanal(cmb_holder.SelectedIndex)[lst_icons.SelectedIndex].Path;
                Concept2DB.RemoveIconDetail(path, cmb_holder.SelectedIndex);
                lst_icons.Items.RemoveAt(lst_icons.SelectedIndex);
                LoadIOCNIST();


            }
        }

        private void image4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/yogender.lucky");
        }

        private void image5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://concept2yogi.deviantart.com");
        }
                         
    }
}



        

       
        
        

       
