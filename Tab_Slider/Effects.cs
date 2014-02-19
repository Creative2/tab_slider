using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Text;

namespace Tab_Slider
{
    class Effects
    {
        public DropShadowEffect DropEffect()
        {
            DropShadowEffect dropeffect = new DropShadowEffect();
            dropeffect.ShadowDepth = 3;
            dropeffect.Color = Color.FromRgb(0, 0, 0);
            dropeffect.Opacity = .7;
            dropeffect.BlurRadius = 5.8;
            return dropeffect;        
        }
        public void Fade(Canvas c, SliderTargetPos slide, double duration)
        {
            DoubleAnimation fadeanima = new DoubleAnimation();
            if (slide == SliderTargetPos.front)
            {
                fadeanima.From = c.Opacity;
                fadeanima.To = 1;
                fadeanima.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(duration));
                c.BeginAnimation(Canvas.OpacityProperty, fadeanima);
            }
            else
            {
                fadeanima.From = c.Opacity;
                fadeanima.To = 0;
                fadeanima.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(duration));
                c.BeginAnimation(Canvas.OpacityProperty, fadeanima);
            }
        
        }
        public void MouseNHolder(Canvas b, Thickness tothickness, bool isactive)
        {
            ThicknessAnimation holderslide = new ThicknessAnimation();
            if (isactive)
            {
                holderslide.To = tothickness;
                holderslide.From = b.Margin;
                holderslide.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                b.BeginAnimation(Canvas.MarginProperty, holderslide);
            }
            else
            {
                holderslide.To = tothickness;
                holderslide.From = b.Margin;
                holderslide.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                b.BeginAnimation(Canvas.MarginProperty, holderslide);
                    
            
            }
        }
        public void NewWIdowsSlidFront(Window panal, double centerlocation, double duration)
        {
            DoubleAnimation slider = new DoubleAnimation();
            slider.To = centerlocation;

            slider.DecelerationRatio = .7;
            slider.From = -panal.Width - 100;
            slider.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
            panal.BeginAnimation(Window.LeftProperty, slider);
        
        }
        public void WIdowsSlidBack(Window panal, double duration)
        {

            DoubleAnimation slider = new DoubleAnimation();
            slider.To = -panal.Width - 100;
            slider.From = panal.Left;
            slider.AccelerationRatio = .4;
            slider.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
            panal.BeginAnimation(Window.LeftProperty, slider);
            
        }
        public void VisiblityAnimation(Image img, bool isactive)
        {
            DoubleAnimation visblityanima = new DoubleAnimation();
            if (isactive)
            {
                visblityanima.From = img.Opacity;
                visblityanima.To = 1;
                visblityanima.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            }
            else
            {
                visblityanima.From =img.Opacity ;
                visblityanima.To = .4;
                visblityanima.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            
            }
        
        }
    }
}
