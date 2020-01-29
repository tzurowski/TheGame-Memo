using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheGame
{
    class Hints
    {
        Cards deck;
        GameGrid gameGrid;

        public Hints(GameGrid grid, Cards pack)
        {
            gameGrid = grid;
            deck = pack;
        }

        public void Create_hints()
        {
            int hint = 1;
            for (int i = 0; i < gameGrid.AmountOfCards; i++)
            {
                for (int j = i + 1; j < gameGrid.AmountOfCards; j++)
                {
                    if (deck.List_of_colors()[i] == deck.List_of_colors()[j])
                    {
                        deck.One_of_cards(i).Content = hint;
                        deck.One_of_cards(j).Content = hint;
                        hint++;
                    }
                    //(deck.One_of_cards(i).Content == deck.One_of_cards(j).Content) &&
                    //   ((deck.One_of_cards(i).Content == null) && (deck.One_of_cards(j).Content == null)) &&
                    //    (deck.One_of_cards(i).Foreground == deck.One_of_cards(j).Foreground)

                }
            }
        }
    }
}
