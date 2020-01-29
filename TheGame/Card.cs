using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheGame
{
    class Card
    {
        Button card;

        public Button Create_card(int buttonID, int width = 100, int height = 100)
        {
            card = new Button();
            card.Width = width;
            card.Height = height;
            card.Name = $"btn{buttonID}";
            card.FontSize = 20;
            card.FontWeight = FontWeights.UltraBold;
            card.Background = new SolidColorBrush(Colors.LightGray);
            //card.AddHandler(Button.ClickEvent, new RoutedEventHandler(gameLogic.Reveal_card));

            return card;
        }
        
    }
}

