using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Tab_Slider
{
    class IconDetailsHandler:DetailStruct
    {
        #region Variable        
        XMLHandler xmlh;
        public IconDetailsHandler(string DBXML)
        {
            xmlh = new XMLHandler(DBXML);
        }
        
        
        #endregion

        #region Functions For Icon
       
        public List<IconDetails> GetAllIconDetailByPanal(int panalindex)
        {

            List<IconDetails> allicon = new List<IconDetails>();
           System.Xml.XPath.XPathNodeIterator nodes= xmlh.GetIconsForPnal(panalindex);
            while (nodes.MoveNext())
            {

                IconDetails icon = new IconDetails();
                
                
                icon.isFile = bool.Parse(nodes.Current.GetAttribute("isFile",""));
                icon.Path = TOPATH(nodes.Current.GetAttribute("Path", ""));
                icon.Image = TOPATH(nodes.Current.GetAttribute("Image", ""));
                icon.runCount = int.Parse(nodes.Current.GetAttribute("Count", ""));
                allicon.Add(icon);
            }                
            return allicon;
        }

        public void AddIconDetail(string IconImagePath,bool isFile,string path,int PanalIndex)
        {
            IconDetails ico = new IconDetails();
            ico.Image =TOXML(IconImagePath);
            ico.Path =TOXML(path);
            
            ico.isFile = isFile;
            xmlh.AddIconDetail(ico, PanalIndex);

        }
        
        public void UpdateIconImage(string path,string image,int panalindex)
        {
            IconDetails icons = new IconDetails();
            icons.Image=TOXML(image);
            icons.Path=TOXML( path);
            xmlh.UpdateIconImage(icons, panalindex);
        }

        public void RemoveIconDetail(string path,int panalindex)
        {
            IconDetails icon = new IconDetails();
            icon.Path =TOXML(path);            
            xmlh.RemoveIconDetail(icon, panalindex);
        }

        public void UpdateCount(int panalindex,string path,int newcount)
        {
            xmlh.updateCount(panalindex, path,newcount);
        
        }
        #endregion

        #region Function For Holder

        public List<HolderDetails> GetHolderDetails() 
        {
            List<HolderDetails> holders = new List<HolderDetails>();
            XmlNodeList allholderxml = xmlh.GetAllHolder();
            
            foreach (XmlNode xmn in allholderxml)
            { 
                HolderDetails holder=new HolderDetails();
                holder.Image=xmn.Attributes["Image"].Value.ToString();
                holder.id = int.Parse(xmn.Attributes["ID"].Value.ToString());
                holder.Name = xmn.Attributes["Name"].Value.ToString();                
                holders.Add(holder);            
            }
            
            return holders;
        }

        public void AddHolder(string name, int id, string image)
        {
            HolderDetails holder = new HolderDetails();
            holder.Name = name;
            holder.id = id;
            holder.Image = image;
            
            
            xmlh.AddPanal(holder);

        
        }

        public void DeletePanal(int panalid, int count)
        {
            xmlh.DeletePanal(panalid, count);
        
        }

        public void Updatepanal(int id, string image, string name)
        {
            HolderDetails holdr = new HolderDetails();
            holdr.Image = image;
            holdr.Name = name;
            xmlh.UpdatePanal(id, holdr);
        
        }

        public void UpdateSlideEffect(int valueSlide,int ValueFade)
        {
            xmlh.UpdateEffectSlide(valueSlide, ValueFade);            
        }

        public void updateShadwo(bool ShadowText, bool ShadowICon)
        {
            xmlh.Effects(ShadowText, ShadowICon);
        }

        public void updateSize(int index)
        {
            xmlh.SizeUpdate(SizeFromIndex(index));
        
        }
        public bool getSHadowText()
        {
            return bool.Parse(xmlh.GetIconShadow());
        }
        public bool GetShadowIcon()
        {
            return bool.Parse(xmlh.GetShadowText());
        }
        public SizeICON GetSizeICON()
        {
            return SizeFromString(xmlh.GetSize());
        
        }
        public double GetFADE()
        {
            try
            {
                return double.Parse(xmlh.GetFadeValue());
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message+  " \nInvalid fding Duration.\n Automatically recovered to default");
                UpdateSlideEffect(500, 200);
                return 200;
            }

        }
        public double GetSlide()
        {
            try
            {
                return double.Parse(xmlh.GetSlideValue());
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + " \nInvalid slide duration. \nAutomatically recovered To default");
                UpdateSlideEffect(500, 200);
                return 500;
            }

        }
       
        #endregion

    }
}
