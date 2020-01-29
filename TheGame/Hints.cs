using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheGame
{
    /// <summary>
    /// Klasa odpowiedzialna za stworzenie podpowiedzi do gry na zasadzie oznaczenia tymi samymi numerkami kart o tym samym kolorze 
    /// </summary>
    class Hints
    {
        Cards deck; // deklaracja odwołania do obiektu klasy Card
        GameGrid gameGrid;  // deklaracja odwołania do obiektu klasy GameGrid


        /// <summary>
        /// Konstruktor odpowiedzialny za umożliwenie dostępu klasie Hints do klass GameGrid i Cards
        /// </summary>
        /// <param name="grid">obiekt klasy GameGrid</param>
        /// <param name="pack">obiekt klasy Cards</param>
        public Hints(GameGrid grid, Cards pack)
        {
            gameGrid = grid; 
            deck = pack;
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie podpowiedzi na planszy
        /// </summary>
        public void CreateHints()
        {
            int hint = 1;   // stworzenie i zainicjalizowanie zmiennej odpowiedzialnej za pojedyńczą podpowiedź
            //pętle odpowiedzialne za przeszukanie listy kolorów w poszukiwaniu par kolorów, w wypadku ich znalezienia ma oznaczyć karty o tych samych indeksach tą samą cyfrą
            for (int i = 0; i < gameGrid.AmountOfCards; i++)
            {
                for (int j = i + 1; j < gameGrid.AmountOfCards; j++)
                {
                    if (deck.ListOfColors()[i] == deck.ListOfColors()[j])
                    {
                        deck.OneOfCards(i).Content = hint;  // przypisanie podpowiedzi do karty (przycisku)
                        deck.OneOfCards(j).Content = hint;  // przypisanie podpowiedzi do karty (przycisku)
                        hint++; // zmiana cyfry podpowiedzi
                    }

                }
            }
        }
    }
}
