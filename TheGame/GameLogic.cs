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
        Cards deck; // deklaracja odwołania do obiektu klasy Cards
        GameGrid gameGrid;  // deklaracja odwołania do obiektu klasy GameGrid
        Button[] clickedCards = new Button[10]; // stworzenie tablicy do zapisywania ile kart kliknięto (wystarczy tablica 3/4 elementowa ponieważ służy tylko do zapisu kart klikniętych w danej turze
        bool isFirstClick = true;   // stworzenie zmiennej odpowiedzialnej za informację czy w naszej turze kliknięta karta jest pierwszą naszą klikniętą kartą
        bool isSecondClick = false; // stworzenie zmiennej odpowiedzialnej za informację czy w naszej turze kliknięta karta jest drugą naszą klikniętą kartą
        private int result = 0;     // stworzenie zmiennej odpowiedzialnej za zliczanie znalezionych par
        private int counter = 0;    // stworzenie zmiennej odpowiedzialnej za zliczanie wykonanych ruchów
        private int i = 0;  // stworzenie zmiennej odpowiedzialnej za rozpoznanie którą kartę kliknęliśmy

        /// <summary>
        /// Konstruktor odpowiedzialny za umożliwenie dostępu klasie GameLogic do klas GameGrid i Cards
        /// </summary>
        /// <param name="grid">obiekt klasy GameGrid</param>
        /// <param name="pack">obiekt klasy Cards</param>
        public GameLogic(GameGrid grid, Cards pack)
        {
            gameGrid = grid;
            deck = pack;

            // pętla która przejdzie przez listę zwracaną przez metodę DeckOfCards i każdemu elementowi tej listy (każdej karcie) nada zdarzeniu kliknięcia wywołanie metody RevealCard
            foreach (var item in deck.DeckOfCards())
                item.AddHandler(Button.ClickEvent, new RoutedEventHandler(RevealCard));
        }

        /// <summary>
        /// Metoda odpowiedzialna za odkrycie karty i wywołanie odpowiednich następstw (sprawdzenie czy jest już para itd.) za pomocą innych metod
        /// </summary>
        /// <param name="sender">kliknięty obiekt (w tym wypadku przycisk)</param>
        /// <param name="e">przesyłane argumenty klikniętego obiektu</param>
        public void RevealCard(object sender, RoutedEventArgs e)
        {
            Button card = (Button)sender; // przypisanie zadeklarowanemu obiektowi kliknięty obiekt, dzięki czemu teraz do kliknietego obiektu możemy odwoływać się jak do obiektu klassy Button o nazwie card
            // sprawdzenie czy klikamy pierwszą kartę w turze
            if (isFirstClick == true)
            {
                clickedCards[i] = card; // do tablicy odpowiedzialnej za kliknięte karty (clickedCards) zapisujemy tę kartę
                isFirstClick = false;   // ustawiamy zmienną odpowiedzialną za klikniecie pierwszej karty na "false" ponieważ następna karta nie może być pierwszą klikniętą kartą
                isSecondClick = true;   // ustawiamy zmienną odpowiedzialną za kliknięcie drugiej karty na "true" ponieważ po pierwszej karcie następna kliknięta karta jest drugą 
            }
            // sprawdzenie czy klikamy drugą kartę w turze
            else if (isSecondClick == true)
            {
                clickedCards[i] = card; // do tablicy odpowiedzialnej za kliknięte karty (clickedCards) zapisujemy tę kartę
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) }; // stworzenie stoper odpowiedzialny za odliczenie określonej ilości czasu po odkryciu karty aby się ona natychmiastowo nie zamykała
                timer.Start(); // wystartowanie stoper
                timer.Tick += (sendeR, args) =>
                {   // tu zapisujemy co ma się wykonać przed zatrzymaniem stopera
                    CheckCards(); // sprawdzamy czy obia kliknięte karty są takie same (wywołujemy metodę)
                    timer.Stop();   // zatrzymanie stopera
                };
                isSecondClick = false; // // ustawiamy zmienną odpowiedzialną za kliknięcie drugiej karty na "false" ponieważ po drugiej karcie następna kliknięta karta nie jest drugą
            }
            // odkrywamy kartę jeśli jest pierwszą lub drugą (zabezpieczamy przed szybkim kliknieciem trzeciej)
            if (i == 0)
            { Open_card(0); i = 1; goto Koniec; }   //idziemy do etykiety Koniec

            if (i == 1)
            { Open_card(1); i = 0; goto Koniec; }   //idziemy do etykiety Koniec
        Koniec:;    // etykieta Koniec
        }
        private void CheckCards()
        {
            // sprawdzamy czy tła dwóch wybranych kart są takie same
            if (clickedCards[0].Background == clickedCards[1].Background)
            {
                clickedCards[0].Click -= RevealCard;    // usuwamy wywołanie metody w zdarzeniu kliknięcia
                clickedCards[1].Click -= RevealCard;    // usuwamy wywołanie metody w zdarzeniu kliknięcia
                result++;   // zwiększamy licznik znalezionych par o 1
                gameGrid.tbResults.Text = result.ToString();    // pokazujemy licznik w textBoxie ukazującym użytkownikowi znalezione pary
                counter++;  // zwiększamy ilość wykonanych ruchów o 1
                gameGrid.tbCounter.Text = counter.ToString();   // pokazujemy licznik w textBoxie ukazującym użytkownikowi wykonane ruchy
                // jeśli ilość znalezionych par będzie równa maksymalnej możliwej liczbie par do znalezienia wyświetlamy komunikat o ukończeniu gry
                if (result == gameGrid.AmountOfPairs)
                    MessageBox.Show($"Udało Ci się wygrać! Zajęło Ci to tylko {counter} prób!");
            }
            else
            {
                Hide_card(clickedCards[0]); // chowamy pierwszą klikniętą kartę ponieważ nie jest taka sama jak druga
                Hide_card(clickedCards[1]); // chowamy drugą klikniętą kartę ponieważ nie jest taka sama jak pierwsza
                counter++;  // zwiększamy ilość wykonanych ruchów o 1
                gameGrid.tbCounter.Text = counter.ToString();   // pokazujemy licznik w textBoxie ukazującym użytkownikowi wykonane ruchy
            }
            isFirstClick = true; // po znalezieniu par lub ponownym zakryciu kart ustawiamy zmienną odpowiedzialną za kliknięcie pierwszej karty na "true" (zaczynamy nową turę)
        }

        /// <summary>
        /// Metoda odpowiedzialna za odkrycie karty
        /// </summary>
        /// <param name="u">kliknięta karta (pierwsza lub druga)</param>
        private void Open_card(int u)
        {
            // sprawdzamy czy kliknięto pierwszą lub drugą kartę, jesli tak to odkrywamy ją
            if (u == 0 || u == 1)
            {
                int cardID = 0; // stworzenie zmiennej ktora posłuży do znalezienia indexu odkrywanej karty
                // przeszukujemy całą listę w poszukiwaniu karty o tej samej nazwie co kliknięta karta
                foreach (var item in deck.DeckOfCards())
                {
                    if (clickedCards[u].Name == item.Name)
                        cardID = deck.DeckOfCards().IndexOf(item); // po znalezieniu takowej przypisujemy jej id do naszej zmiennej cardID
                }
                deck.OneOfCards(cardID).Background = deck.ListOfColors()[cardID]; // karcie którą odkryliśmy przypisujemy odpowiedni kolor z listy kolorów
            }
        }


        /// <summary>
        /// Metoda odpowiezialna za przywrócenie tła karty do pierwotnego (zakrycie karty)
        /// </summary>
        /// <param name="card"> karta którą chcemy zakryć</param>
        private void Hide_card(Button card)
        {
            card.Background = new SolidColorBrush(Color.FromRgb(244, 244, 244)); // ustawiamy kolor domyślny spowrotem jako kolor tła
        }
    }
}
