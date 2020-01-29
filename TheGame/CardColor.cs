using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using static System.Windows.Media.Colors;
using System.Windows;

namespace TheGame
{
    class CardColor
    {
        List<SolidColorBrush> colorList;
        List<Color> tempColorList;
        Random random = new Random();

        public int AmountOfCards { get; private set; }
        public CardColor(int amount)
        {
            AmountOfCards = amount;
        }
        public List<SolidColorBrush> Create_list()
        {
            colorList = new List<SolidColorBrush>();
            tempColorList = new List<Color>(All_colors());
            int count = tempColorList.Count;
            for (int i = 0; i < AmountOfCards / 2; i++)
            {
                int randomColor = random.Next(count -1);
                colorList.Add(new SolidColorBrush(tempColorList[randomColor]));
                tempColorList.RemoveAt(randomColor);
                count--;
            }
            colorList.AddRange(colorList);
            return colorList;
        }
        
        public List<SolidColorBrush> Random_appearance_list()
        {
            List<SolidColorBrush> appearance = new List<SolidColorBrush>();
            List<SolidColorBrush> temp = new List<SolidColorBrush>(Create_list());
            int count = temp.Count;
            int randomedColor;
            for (int i = 0; i < AmountOfCards; i++)
            {
                randomedColor = random.Next(count - 1);
                appearance.Add(temp[randomedColor]);
                temp.Remove(temp[randomedColor]);
                count--;
            }
            return appearance;
        }

        private List<Color> All_colors()
        {
            List<Color> allColors = new List<Color>();
            allColors.Add(AntiqueWhite);
            allColors.Add(Aqua);
            allColors.Add(Aquamarine);
            allColors.Add(Black);
            allColors.Add(Blue);
            allColors.Add(BlueViolet);
            allColors.Add(Brown);
            allColors.Add(CadetBlue);
            allColors.Add(Chartreuse);
            allColors.Add(Chocolate);

            allColors.Add(CornflowerBlue);
            allColors.Add(DarkBlue);
            allColors.Add(DarkCyan);
            allColors.Add(DarkGoldenrod);
            allColors.Add(DarkGray);
            allColors.Add(DarkKhaki);
            allColors.Add(DarkMagenta);
            allColors.Add(DarkRed);
            allColors.Add(DarkSlateBlue);
            allColors.Add(DarkSlateGray);

            allColors.Add(DeepPink);
            allColors.Add(DeepSkyBlue);
            allColors.Add(ForestGreen);
            allColors.Add(Fuchsia);
            allColors.Add(Gold);
            allColors.Add(HotPink);
            allColors.Add(IndianRed);
            allColors.Add(LightCoral);
            allColors.Add(LightPink);
            allColors.Add(Orange);

            allColors.Add(Red);
            allColors.Add(Yellow);
            allColors.Add(Tomato);

            return allColors;
        }
    }
}
