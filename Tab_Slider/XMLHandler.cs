using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Tab_Slider
{

    class XMLHandler
    {
        XmlDocument xDocument;
        string pathname;
        bool IsOld=false;
        

        public XMLHandler(string filepath)
        {
            pathname = filepath;
            xDocument = new XmlDocument();
            xDocument.Load(filepath);
        }

        #region Function for ICON Per Panal 


        //public int CountIconForThisPanal(int PanalIndex)         
        //{
        //    ReloadDB();
        //    return xDocument.SelectSingleNode("ConceptDB").SelectSingleNode("holder[@id='" + PanalIndex.ToString() + "']").SelectNodes("file").Count;    
        
        //}

        public System.Xml.XPath.XPathNodeIterator GetIconsForPnal(int PanalIndex) {
            xDocument.Load(pathname);
           System.Xml.XPath.XPathNavigator xnavi=xDocument.CreateNavigator();
            System.Xml.XPath.XPathExpression exp=xnavi.Compile("ConceptDB//"+@"holder[@ID='" + PanalIndex.ToString() + @"']/*");
            exp.AddSort("@Count",System.Xml.XPath.XmlSortOrder.Descending,System.Xml.XPath.XmlCaseOrder.None,"",System.Xml.XPath.XmlDataType.Number);
            System.Xml.XPath.XPathNodeIterator list = xnavi.Select(exp);
            return list;
        }        

        public void AddIconDetail(IconDetails details, int PanalIndex)
        {
            xDocument.Load(pathname);
            XmlNode newfile = xDocument.CreateNode(XmlNodeType.Element, "file", null);
            XmlAttribute xIsFile = xDocument.CreateAttribute("isFile");
            xIsFile.Value = details.isFile.ToString();
            XmlAttribute xpath = xDocument.CreateAttribute("Path");
            xpath.Value = details.Path;
            XmlAttribute ximage = xDocument.CreateAttribute("Image");
            ximage.Value = details.Image;            
            XmlAttribute Count = xDocument.CreateAttribute("Count");
            Count.Value = "1";
            newfile.Attributes.Append(xIsFile);
            newfile.Attributes.Append(xpath);
            newfile.Attributes.Append(ximage);
            newfile.Attributes.Append(Count);
            xDocument.SelectSingleNode("ConceptDB").SelectSingleNode("holder[@ID='" + PanalIndex.ToString() + "']").AppendChild(newfile);
            SaveDB();
            IsOld = false;
        }
        public void updateCount(int PanalIndex,string path,int newCount)
        {
            xDocument.SelectSingleNode("ConceptDB").SelectSingleNode("holder[@ID='" + PanalIndex.ToString() + "']").SelectSingleNode("file[@Path='" + path + "']").Attributes["Count"].Value = newCount.ToString();
            SaveDB();
            
        }
        public void UpdateIconImage(IconDetails details, int PanalIndex) 
        {
            XmlNode rootnode = xDocument.SelectSingleNode("ConceptDB").SelectSingleNode("holder[@ID='"+PanalIndex.ToString()+"']");
            XmlNode updatenode = rootnode.SelectSingleNode("file[@Path='" + details.Path + "']");
            updatenode.Attributes["Image"].Value = details.Image;
            SaveDB();
        
        
        }

        public void RemoveIconDetail(IconDetails details, int PanalIndex) 
        {
         
            XmlNode Prentnode = xDocument.SelectSingleNode("//holder[@ID='" + PanalIndex.ToString() + "']");
         //   xDocument.DocumentElement.SetAttribute("pathto",details.Path);
            XmlNode deletenode = xDocument.SelectSingleNode("//descendant::file[@Path='"+details.Path+"']");
            if (deletenode != null)
                deletenode.ParentNode.RemoveChild(deletenode);
            SaveDB();        
        }


        #endregion

        #region Function for Per Panal/Holder

        public XmlNodeList GetAllHolder()
        {
            return xDocument.SelectSingleNode("ConceptDB").SelectNodes("holder");
        
        }

        public void AddPanal(HolderDetails details)
        {
            XmlNode newfile = xDocument.CreateNode(XmlNodeType.Element, "holder", null);
            XmlAttribute id = xDocument.CreateAttribute("ID");
            id.Value = details.id.ToString();
            XmlAttribute image = xDocument.CreateAttribute("Image");
            image.Value = details.Image;
            XmlAttribute name = xDocument.CreateAttribute("Name");
            name.Value = details.Name;            
            

            newfile.Attributes.Append(id);
            newfile.Attributes.Append(image);
            newfile.Attributes.Append(name);            
            
            xDocument.SelectSingleNode("ConceptDB").AppendChild(newfile);
            SaveDB();
        }        
        
        public bool DeletePanal(int deletedid,int couunt)
        {
            //try
            //{
                xDocument.Load(pathname);
                XmlNode Prentnode = xDocument.SelectSingleNode("ConceptDB");
                Prentnode.RemoveChild(Prentnode.SelectSingleNode("holder[@ID='" + deletedid.ToString() + "']"));
                if (couunt != 0)
                    RearrangeID(deletedid, couunt);
                SaveDB();
                System.Windows.MessageBox.Show("Successfully Deleted", "Deleted", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                //return true;
            //}
            //catch (Exception ex)
            //{
               // System.Windows.MessageBox.Show("Unknown Error Occured!!! Please Restart Application.. \n "+ex.Message, "Deletion Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                return false;
            //}
        }

        public bool UpdatePanal(int Panalid,HolderDetails details)
        {
            try
            {
                XmlNode rootnode = xDocument.SelectSingleNode("ConceptDB").SelectSingleNode("holder[@ID='" + Panalid.ToString() + "']");
                rootnode.Attributes["Image"].Value = details.Image;
                rootnode.Attributes["Name"].Value = details.Name;                
                
                SaveDB();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            
            }
        
        
        }
        
        #endregion

        #region HouseKeeping Function
        private void RearrangeID(int deleted, int count)
        {


            XmlNode rootnode = xDocument.SelectSingleNode("ConceptDB");
            for (int i = deleted; i < count-1; i++)
            {

                XmlNode updatenode = rootnode.SelectSingleNode("holder[@ID='" + (i + 1).ToString() + "']");
                XmlAttribute idattrib = updatenode.Attributes["ID"];
                idattrib.Value = i.ToString();


            }
            SaveDB();

        }

        public void SaveDB()
        {
            xDocument.Save(pathname);
            IsOld = true;
           
        }


        public void ReloadDB()        
        {
            xDocument.Load(pathname);                    
        }
        
        #endregion

        public void UpdateEffectSlide(int valueSLide,int ValueFade)
        {
            xDocument.SelectSingleNode("ConceptDB").Attributes["Slide"].Value = valueSLide.ToString();
            xDocument.SelectSingleNode("ConceptDB").Attributes["Fade"].Value = ValueFade.ToString();
            SaveDB();
        
        }
        public void Effects(bool TextShadow, bool IconShadow)
        {
            xDocument.SelectSingleNode("ConceptDB").Attributes["TextShadow"].Value = TextShadow.ToString();
            xDocument.SelectSingleNode("ConceptDB").Attributes["IconShadow"].Value = IconShadow.ToString();
            SaveDB();
        }
        public void SizeUpdate(SizeICON size)
        {
            xDocument.SelectSingleNode("ConceptDB").Attributes["IconSize"].Value = size.ToString();
        }
        public string GetSize()
        {
            return xDocument.SelectSingleNode("ConceptDB").Attributes["IconSize"].Value.ToString();
        }
        public string GetShadowText()
        {
            return xDocument.SelectSingleNode("ConceptDB").Attributes["TextShadow"].Value.ToString();
        }
        public string GetIconShadow()
        {
            return xDocument.SelectSingleNode("ConceptDB").Attributes["IconShadow"].Value.ToString();
        }
        public string GetSlideValue()
        {
            return xDocument.SelectSingleNode("ConceptDB").Attributes["Slide"].Value.ToString();
        }
        public string GetFadeValue()
        {
            return xDocument.SelectSingleNode("ConceptDB").Attributes["Fade"].Value.ToString();
        }
    }
}
