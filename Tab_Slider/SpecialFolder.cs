using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tab_Slider
{
    class SpecialFolder
    {

        private static SpecialPath XPDECODE(int index)
        {
            SpecialPath pah=new SpecialPath();
               switch(index)
               {
                       case 0:
                           
                           pah.addrs="shell:Favorites";pah.name="Favorites";
                           return pah;
                       
                       case 1:pah.addrs="shell:My Video";pah.name="My Videos";
                           return pah;
                       
                       case 2:pah.addrs="shell:My Pictures";pah.name="My Picture";
                           return pah;
                       
                       case 3:pah.addrs="shell:ConnectionsFolder";pah.name="Conncetions";
                           return pah;
                       
                       case 4:pah.addrs="shell:Administrative Tools";pah.name="Admin Tools";
                           return pah;
                       
                       case 5:pah.addrs="shell:RecycleBinFolder";pah.name="Recycle Bin";
                           return pah;
                       
                       case 6:pah.addrs= "shell:Fonts";pah.name="Fonts";
                           return pah;
                       
                      case 7:pah.addrs="shell:Personal";pah.name="My Document";
                           return pah;
                       
                       case 8:pah.addrs="shell:MyComputerFolder";pah.name="My Computer";
                           return pah;
                       
                       case 9:pah.addrs="shell:ProgramFiles";pah.name="Program Files";
                           return pah;
                       
                       case 10:pah.addrs="shell:Windows";pah.name="Winodws";
                           return pah;                       
                       
                       case 11:pah.addrs="shell:My Music";pah.name="My Music";
                           return pah;
                       
                       case 12:pah.addrs="shell:PrintersFolder";pah.name="Printers";
                           return pah;
                       
                       default:
                       pah.name=string.Empty;
                       pah.addrs=string.Empty;
                       return pah;
                       
               }

        
        
        }
             
        private static SpecialPath VISTA7DECODE(int index)
          {
                   SpecialPath pah=new SpecialPath();
               switch(index)
               {
                       case 0:
                           
                           pah.addrs="shell:Favorites";pah.name="Favorites";
                           return pah;
                       
                       case 1:pah.addrs="shell:My Video";pah.name="My Videos";
                           return pah;
                       
                       case 2:pah.addrs="shell:My Pictures";pah.name="My Picture";
                           return pah;
                       
                       case 3:pah.addrs="shell:ConnectionsFolder";pah.name="Conncetions";
                           return pah;
                       
                       case 4:pah.addrs="shell:Administrative Tools";pah.name="Admin Tools";
                           return pah;
                       
                       case 5:pah.addrs="shell:RecycleBinFolder";pah.name="Recycle Bin";
                           return pah;
                       
                       case 6:pah.addrs= "shell:Fonts";pah.name="Fonts";
                           return pah;
                       
                      case 7:pah.addrs="shell:Personal";pah.name="My Document";
                           return pah;
                       
                       case 8:pah.addrs="shell:MyComputerFolder";pah.name="My Computer";
                           return pah;
                       
                       case 9:pah.addrs="shell:ProgramFiles";pah.name="Program Files";
                           return pah;
                       
                       case 10:pah.addrs="shell:Windows";pah.name="Winodws";
                           return pah;                       
                       
                       case 11:pah.addrs="shell:My Music";pah.name="My Music";
                           return pah;
                       
                       case 12:pah.addrs="shell:PrintersFolder";pah.name="Printers";
                           return pah;
                       
                       case 13:pah.addrs="shell:Contacts";pah.name="Contacts";
                           return pah;
                       
                       case 14:pah.addrs="shell:Downloads";pah.name="Downloads";
                           return pah;
                       
                       case 15:pah.addrs="shell:Games";pah.name="Games";
                           return pah;
                       
                       default:
                       pah.name=string.Empty;
                       pah.addrs=string.Empty;
                       return pah;
                       
               }
          
          }

        public static SpecialPath GetSPecialPath(int index)
        {            
            if (Environment.Version.Build <= 5000)
            {
                
                
               return XPDECODE(index);
            
            }
            if (Environment.Version.Build > 5000)
            {

                return VISTA7DECODE(index);
            }
            else
                return new SpecialPath();
        
        }

    }
}
