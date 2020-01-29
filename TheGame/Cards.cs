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
    class Cards
    {
        public Card card;
        public CardColor cardColor;
        private List<Button> cardList = new List<Button>();
        private List<SolidColorBrush> colorList;
        public Cards(int numberOfCards, int width, int height)
        {
            card = new Card();
            cardColor = new CardColor(numberOfCards);
            colorList = new List<SolidColorBrush>(cardColor.Random_appearance_list());
            for (int i = 0; i < numberOfCards; i++)
            {
                cardList.Add(card.Create_card(buttonID: i, width, height));
            }
        }
        public Button One_of_cards(int ID)
        {
            return cardList[ID];
        }
        public List<Button> Deck_of_cards()
        {
            return cardList;
        }
        public List<SolidColorBrush> List_of_colors()
        {
            return colorList;
        }
    }
}
