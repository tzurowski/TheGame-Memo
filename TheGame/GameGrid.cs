using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Math;

namespace TheGame
{
    /// <summary>
    /// Klasa odpowiedzialna za:
    /// - stworzenie siatki (kolumny i wiersze)
    /// - nałożenie kart na siatkę
    /// - pokzanie siatki w oknie aplikacji
    /// - stworzenie kontrolek dla pola statystyk
    /// - wywołanie podpowiedzi
    /// </summary>
    class GameGrid
    {
        private const int defaultNumberOfFields = 16;   // stworzenie zmiennej odpowiedzialnej za domyślną ilość pól
        private int gameAreaSize = 600; // stworzenie zmiennej odpowiedzialnej za rozmiar pola kart
        private int statisticsAreaSize = 45;    // stworzenie zmiennej odpowiedzialnej za rozmiar pola statystyk
        private int windowMarginSize = 10;  // stworzenie zmiennej odpowiedzialnej za rozmiar marginesów
        private ColumnDefinition column;    // zadeklarowanie obiektu klasy ColumnDefinition odpowiedzialnego za stworzenie kolumny na siatce
        private RowDefinition row;  // zadeklarowanie obiektu klasy RowDefinition odpowiedzialnego za stworzenie wiersza na siatce
        private Grid gameGrid = new Grid(); // stworzenie obiektu zmiennej Grid (siatki)
        private Cards cards;    // zadeklarowanie obiektu klasy Cards
        private GameLogic gameLogic;    // zadeklarowanie obiektu klasy GameLogic
        public TextBlock tbResults = new TextBlock();   // stworzenie TextBlocka w którym będzie wyświetlana ilość znalezionych par
        public TextBlock tbCounter = new TextBlock();   // stworzenie TextBlocka w którym będzie wyświetlana ilość wykonanych ruchów

        private MainWindow gameWindow;  // zadeklarowanie obiektu klasy MainWindow odpowiedzialnego za wyświetlane okno aplikacji
        public int AmountOfCards { get; private set; } // pole odpowiedzialne za ilość kart
        public int AmountOfPairs { get; private set; }  // pole odpowiedzialne za ilość par
        public double amountOfColumns;  // stworzenie zmiennej odpowiedzialnej za ilość kolmun
        public double amountOfRows; // stworzenie zmiennej odpowiedzialnej za ilość wierszy

        /// <summary>
        /// Konstruktor odpowiedzialny za stworzenie okna (i poczęści siatki) 
        /// </summary>
        /// <param name="window">obiekt klasy MainWindow</param>
        /// <param name="amount">ilość pól na planszy do gry z domyślną wartością</param>
        public GameGrid(MainWindow window, int amount = defaultNumberOfFields)
        {
            window.Width = gameAreaSize + 2 * windowMarginSize; // ustawienie szerokości okna
            window.Height = gameAreaSize + 3 * windowMarginSize + 2 * statisticsAreaSize;   // ustawienie wysokości okna
            window.Title = "Match colors";  // ustawienie tytułu okna
            window.ResizeMode = ResizeMode.NoResize;    // zablokowanie możliwości manipulowania rozmiarem okna przez uzytkownika
            
            gameWindow = window;
            AmountOfCards = amount;
            AmountOfPairs = amount / 2;
            amountOfColumns = amountOfRows = Sqrt(Convert.ToDouble(amount));

            int width = Convert.ToInt32(gameAreaSize / amountOfColumns);    // oblicznie szerokości karty/pola
            int height = Convert.ToInt32(gameAreaSize / amountOfRows);  // oblicznie wysokości karty/pola
            cards = new Cards(AmountOfCards, width, height);    // stworzenie obiektu klassy Cards
            gameLogic = new GameLogic(this, cards); // stworzenie obiektu klasy GameLogic
            CreateLabels();    // wywołanie metody tworzącej opisy w polu statystyk
            CreateTextBlocks();    // wywołanie metody tworzącej textblocki w polu statystyk
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie siatki
        /// </summary>
        public void CreateGrid()
        {
            CreateAndAddColumnsToTheGrid();   // wywołanie metody odpowiedzialnej za stworzenie kolumn na siatce
            CreateAndAddRowsToTheGrid();  // wywołanie metody odpowiedzialnej za stworzenie wierszy na siatce
            AddCardsToGrid();    // wywołanie metody odpowiedzialnej za dodanie kart do siatki
            ShowGrid();    // wywołanie metody odpowiedzialnej za pokazanie siatki
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie i dodanie kolumn do siatki
        /// </summary>
        private void CreateAndAddColumnsToTheGrid()
        {
            double firstColumn = 0; // stworzenie zmiennej odpowiedzialnej za pierwszą kolumnę (margines z lewej strony)
            double lastColumn = amountOfColumns + 1;    // stworzenie zmiennej odpowiedzialnej za ostatnią kolumnę (margines z prawej strony)
            // pętla odpowiedzialna za stworzenie określonej ilość kolumn i dodanie ich do siatki
            for (int i = 0; i < amountOfColumns + 2; i++)
            {
                column = new ColumnDefinition() { Name = $"cdef{i}" }; // stworzenie kolumny o unikalnej nazwie (np. cdef1, cdef2 itd.)
                // jeśli kolumna jest marginesem ustawiamy jej stały rozmiar
                if (column.Name == $"cdef{firstColumn}" || column.Name == $"cdef{lastColumn}")  
                    column.Width = new GridLength(windowMarginSize);
                gameGrid.ColumnDefinitions.Add(column); // dodanie kolumny do siatki
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie i dodanie wierszy do siatki
        /// </summary>
        private void CreateAndAddRowsToTheGrid()
        {
            double firstRow = 0;    // stworzenie zmiennej odpowiedzialnej za pierwszy wiersz (margines od góry)
            double rowAfterBoard = amountOfRows + 1;    // stworzenie zmiennej odpowiedzialnej za wiersz rozdzielający pole z kartami i pole ze statystykami
            double lastWindowRow = amountOfRows + 4;    // stworzenie zmiennej odpowiedzialnej za ostatni wiersz (margines od dołu)
            double firstRowInStatisticsField = amountOfRows + 2;    // stworzenie zmiennej odpowiedzialnej za pierwszy wiersz w polu statystyk
            double secondRowInStatisticsField = amountOfRows + 3;   // stworzenie zmiennej odpowiedzialnej za drugi(ostatni) wiersz w polu statystyk
            // pętla odpowiedzialna za stworzenie określonej ilość wierszy i dodanie ich do siatki
            for (int i = 0; i < amountOfRows + 3 + 2; i++)
            {
                row = new RowDefinition() { Name = $"rdef{i}" };    // stworzenie wiersza o unikalnej nazwie (np. rdef1, rdef2 itd.)
                // jeśli kolumna jest marginesem ustawiamy jej stały rozmiar
                if (row.Name == $"rdef{firstRow}" || row.Name == $"rdef{lastWindowRow}" || row.Name == $"rdef{rowAfterBoard}")
                    row.Height = new GridLength(windowMarginSize);
                // jeśli kolumna jest wierszem w polu statystyk ustawiamy jej stały rozmiar
                else if (row.Name == $"rdef{firstRowInStatisticsField}" || row.Name == $"rdef{secondRowInStatisticsField}")
                    row.Height = new GridLength(statisticsAreaSize);
                gameGrid.RowDefinitions.Add(row);   // dodanie wiersza do siatki
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za dodanie kart do siatki
        /// </summary>
        private void AddCardsToGrid()
        {
            int id = 0; // stworzenie zmiennej odpowiedzialnej za id karty
            // pierwsza pętla przechozi po wierszach druga po kolumnach
            for (int i = 1; i <= amountOfColumns; i++)
            {
                for (int j = 1; j <= amountOfRows; j++, id++)
                {
                    Grid.SetRow(cards.OneOfCards(id), i);   // dodanie karty do odpowiedniego wiersza
                    Grid.SetColumn(cards.OneOfCards(id), j);    // dodanie karty do odpowiedniej kolumny
                    gameGrid.Children.Add(cards.OneOfCards(id));    // dodanie karty do siatki
                }
            }
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie opisów w polus statystyk
        /// </summary>
        private void CreateLabels()
        {
            Label lbResults = new Label();  // stworznie obiektu klasy Label
            lbResults.Content = "Znaleziono par: "; // nadanie naszemu labelowi tekstu (opis)
            //lbResults.Margin = new Thickness(20,2,20,2); // nadanie marginesów naszemu opisowi (zbędne)
            lbResults.FontSize = 24;    // zmiana rozmiaru czcionki naszego opsiu
            lbResults.FontWeight = FontWeights.Bold;    // zmiana grubości naszego opsiu
            lbResults.HorizontalAlignment = HorizontalAlignment.Center; // ustawienie naszego opisu w centrum 
            Grid.SetRow(lbResults, Convert.ToInt32(amountOfRows) + 2);  // ustawienie opisu w odpowiednim wierszu
            Grid.SetColumn(lbResults, 1);   // ustawienie opisu w odpowiedniej kolumnie
            Grid.SetColumnSpan(lbResults, Convert.ToInt32(amountOfColumns) - 1); // ustawienie na ilu kolumnach ma być nasz opis (szerokość opisu)
            gameGrid.Children.Add(lbResults);   // dodanie opisu do siatki

            Label lbCounter = new Label();
            lbCounter.Content = "Wykonano ruchów: ";    
            //lbCounter.Margin = new Thickness(20, 2, 20, 2);
            lbCounter.FontSize = 24;
            lbCounter.FontWeight = FontWeights.Bold;
            lbCounter.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(lbCounter, Convert.ToInt32(amountOfRows) + 3);
            Grid.SetColumn(lbCounter, 1);
            Grid.SetColumnSpan(lbCounter, Convert.ToInt32(amountOfColumns) - 1);
            gameGrid.Children.Add(lbCounter);
        }

        /// <summary>
        /// Metoda odpowiedzilna za stworzenie textBlocków w polu statystyk
        /// </summary>
        private void CreateTextBlocks()
        {
            tbResults.Text = "0";   // ustawienie 0 jako ilość znalezionych par podczas tworzenie textBlocka
            tbResults.FontSize = 24;    // ustawienie rozmiaru wyświetlanego tekstu w textBlocku
            tbResults.FontWeight = FontWeights.Bold;    // ustawienie grubości wyświetlanego tekstu w textBlocku
            tbResults.Margin = new Thickness(20, 2, 20, 2); // ustawienie marginesów textBlocka
            Grid.SetRow(tbResults, Convert.ToInt32(amountOfRows) + 2);  // ustawienie textBlocka w odpowiednim wierszu
            Grid.SetColumn(tbResults, Convert.ToInt32(amountOfColumns));    // ustawienie textBlocka w odpowiedniej kolumnie
            gameGrid.Children.Add(tbResults);   // dodanie textBlocka do siatki

            tbCounter.Text = "0";
            tbCounter.FontSize = 24;
            tbCounter.FontWeight = FontWeights.Bold;
            tbCounter.Margin = new Thickness(20, 2, 20, 2);
            Grid.SetRow(tbCounter, Convert.ToInt32(amountOfRows) + 3);
            Grid.SetColumn(tbCounter, Convert.ToInt32(amountOfColumns));
            gameGrid.Children.Add(tbCounter);
        }

        /// <summary>
        /// Metoda odpowiedzialna za wyświetlenie siatki w oknie
        /// </summary>
        private void ShowGrid()
        {
            gameGrid.ShowGridLines = true; // ustawienie pokazywania lini siatki
            gameWindow.Content = gameGrid;  // dodanie siatki do okna aplikacji
        }

        /// <summary>
        /// Metoda odpowiedzialna za pokazanie podpowiedzi
        /// </summary>
        public void Show_hints()
        {
            Hints hints = new Hints(this, cards); // stworzenie obiektu klasy Hints.cs
            hints.CreateHints();    // stworzenie odpowiedzi
        }
    }
}
