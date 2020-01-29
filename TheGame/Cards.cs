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
        public Card card;   // deklaracja odwołania do obiektu klasy Card
        public CardColor cardColor;     // deklaracja odwołania do obiektu klasy CardColor
        private List<Button> cardList = new List<Button>();  // stworzenie pustej listy karty
        private List<SolidColorBrush> colorList;    // deklaracja odwołania do listy kolorów

        /// <summary>
        /// Konstruktor klasy odpowiedzialny za stworzenie talii kart
        /// </summary>
        /// <param name="numberOfCards">ilość kart do stworzenia</param>
        /// <param name="width">szerokosć kart</param>
        /// <param name="height">wysokość karty</param>
        public Cards(int numberOfCards, int width, int height)
        {
            card = new Card();  // stworzenie obiektu klasy Card
            cardColor = new CardColor(numberOfCards);   //stworzenie obiektu klasy CardColor i przesłanie parametru odpowiedzialnego za ilość kart 
            colorList = new List<SolidColorBrush>(cardColor.RandomAppearanceList());  // stworzenie listy kolorów (background przycisku) i przypisanie jej zawartości listy stworzenj w klasie CardColor w metodzie RandomAppearanceList()
            // petla odpowiedzialna za stworzenie "numberOfCards" kart
            for (int i = 0; i < numberOfCards; i++)
            {
                cardList.Add(card.CreateCard(buttonID: i, width, height)); // stworzenie pojedyńczej karty gdzie buttonID = i
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za przekazanie użytkownikowi zadanej karty
        /// </summary>
        /// <param name="ID">id karty do której chcemy się odwołać</param>
        /// <returns>zwracamy poszukiwaną kartę</returns>
        public Button OneOfCards(int ID)
        {
            return cardList[ID];
        }

        /// <summary>
        /// Metoda odpowiedzialna za przekazanie użytkownikowi listy kart
        /// </summary>
        /// <returns>zwracamy listę kart</returns>
        public List<Button> Deck_of_cards()
        {
            return cardList;
        }

        /// <summary>
        /// Metoda odpowiedzialna za przekazanie użytkownikowi listy kolorów
        /// </summary>
        /// <returns>zwracamy listę kolorów</returns>
        public List<SolidColorBrush> List_of_colors()
        {
            return colorList;
        }
    }
}
