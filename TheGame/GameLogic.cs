using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace TheGame
{
    class GameLogic
    {
        Cards deck;
        GameGrid gameGrid;
        Button[] clickedCards = new Button[10];
        bool isFirstClick = true;
        bool isSecondClick = false;
        private int result = 0;
        private int counter = 0;
        int i = 0;
        Button previousCard = new Button();
        public GameLogic(GameGrid grid, Cards pack)
        {
            gameGrid = grid;
            deck = pack;
            foreach (var item in deck.Deck_of_cards())
                item.AddHandler(Button.ClickEvent, new RoutedEventHandler(Reveal_card));
        }
        public void Reveal_card(object sender, RoutedEventArgs e)
        {
            Button card = (Button)sender;
            if (isFirstClick == true)
            {
                clickedCards[i] = card;
                isFirstClick = false;
                isSecondClick = true;
            }
            else if (isSecondClick == true)
            {
                clickedCards[i] = card;
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
                timer.Start();
                timer.Tick += (sendeR, args) =>
                {
                    Check_cards();
                    timer.Stop();
                };
                isSecondClick = false;
            }
            if (i == 0)
            { Open_card(0); i = 1; goto Koniec; }

            if (i == 1)
            { Open_card(1); i = 0; goto Koniec; }
        Koniec:;
        }
        private void Check_cards()
        {
            if (clickedCards[0].Background == clickedCards[1].Background)
            {
                clickedCards[0].Click -= Reveal_card;
                clickedCards[1].Click -= Reveal_card;
                result++;
                gameGrid.tbResults.Text = result.ToString();
                counter++;
                gameGrid.tbCounter.Text = counter.ToString();
                if (result == gameGrid.AmountOfPairs)
                    MessageBox.Show($"Udało Ci się wygrać! Zajęło Ci to tylko {counter} prób!");
            }
            else
            {
                Hide_card(clickedCards[0]);
                Hide_card(clickedCards[1]);
                counter++;
                gameGrid.tbCounter.Text = counter.ToString();
            }
            isFirstClick = true;
        }

        private void Open_card(int u)
        {
            if (u == 0 || u == 1)
            {
                int cardID = 0;
                foreach (var item in deck.Deck_of_cards())
                {
                    if (clickedCards[u].Name == item.Name)
                        cardID = deck.Deck_of_cards().IndexOf(item);
                }
                deck.One_of_cards(cardID).Background = deck.List_of_colors()[cardID];
            }
        }

        private void Hide_card(Button card)
        {
            card.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
        }
    }
}
