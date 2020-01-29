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
    class GameGrid
    {
        private const int defaultNumberOfFields = 16;
        private int gameAreaSize = 600;
        private int statisticsAreaSize = 45;
        private int windowMarginSize = 10;
        private ColumnDefinition column;
        private RowDefinition row;
        private Grid gameGrid = new Grid();
        private Cards cards;
        private GameLogic gameLogic;
        public TextBlock tbResults = new TextBlock();
        public TextBlock tbCounter = new TextBlock();

        public MainWindow GameWindow { get; private set; }
        public int AmountOfCards { get; private set; }
        public int AmountOfPairs { get; private set; }
        public double AmountOfColumns { get; private set; }
        public double AmountOfRows { get; private set; }
        public GameGrid()
        {
        }
        public GameGrid(MainWindow window, int amount = defaultNumberOfFields)
        {
            window.Width = gameAreaSize + 2 * windowMarginSize;
            window.Height = gameAreaSize + 3 * windowMarginSize + 2 * statisticsAreaSize;
            window.Title = "Match colors";
            window.ResizeMode = ResizeMode.NoResize;
            
            GameWindow = window;
            AmountOfCards = amount;
            AmountOfPairs = amount / 2;
            AmountOfColumns = AmountOfRows = Sqrt(Convert.ToDouble(amount));

            int width = Convert.ToInt32(gameAreaSize / AmountOfColumns);
            int height = Convert.ToInt32(gameAreaSize / AmountOfRows);
            cards = new Cards(AmountOfCards, width, height);
            Create_labels();
            gameLogic = new GameLogic(this, cards);
            Create_textblocks();
        }

        public void Create_grid()
        {
            Create_and_add_columns_to_the_grid();
            Create_and_add_rows_to_the_grid();
            Add_cards_to_grid();
            Show_grid();
        }

        private void Create_and_add_columns_to_the_grid()
        {
            double firstColumn = 0;
            double lastColumn = AmountOfColumns + 1;
            for (int i = 0; i < AmountOfColumns + 2; i++)
            {
                column = new ColumnDefinition() { Name = $"cdef{i}" };
                if (column.Name == $"cdef{firstColumn}" || column.Name == $"cdef{lastColumn}")
                    column.Width = new GridLength(windowMarginSize);
                gameGrid.ColumnDefinitions.Add(column);
            }
        }

        private void Create_and_add_rows_to_the_grid()
        {
            double firstRow = 0;
            double rowAfterBoard = AmountOfRows + 1;
            double lastWindowRow = AmountOfRows + 4;
            double firstRowInStatisticsField = AmountOfRows + 2;
            double secondRowInStatisticsField = AmountOfRows + 3;
            for (int i = 0; i < AmountOfRows + 3 + 2; i++)
            {
                row = new RowDefinition() { Name = $"rdef{i}" };
                if (row.Name == $"rdef{firstRow}" || row.Name == $"rdef{lastWindowRow}" || row.Name == $"rdef{rowAfterBoard}")
                    row.Height = new GridLength(windowMarginSize);
                else if (row.Name == $"rdef{firstRowInStatisticsField}" || row.Name == $"rdef{secondRowInStatisticsField}")
                    row.Height = new GridLength(statisticsAreaSize);
                gameGrid.RowDefinitions.Add(row);
            }
        }

        private void Add_cards_to_grid()
        {
            int id = 0;
            for (int i = 1; i <= AmountOfColumns; i++)
            {
                for (int j = 1; j <= AmountOfRows; j++, id++)
                {
                    Grid.SetRow(cards.OneOfCards(id), i);
                    Grid.SetColumn(cards.OneOfCards(id), j);
                    gameGrid.Children.Add(cards.OneOfCards(id));
                }
            }
        }

        private void Create_labels()
        {
            Label lbResults = new Label();
            lbResults.Content = "Znaleziono par: ";
            //lbResults.Margin = new Thickness(20,2,20,2);
            lbResults.FontSize = 24;
            lbResults.FontWeight = FontWeights.Bold;
            lbResults.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(lbResults, Convert.ToInt32(AmountOfRows) + 2);
            Grid.SetColumn(lbResults, 1);
            Grid.SetColumnSpan(lbResults, Convert.ToInt32(AmountOfColumns) - 1);
            gameGrid.Children.Add(lbResults);

            Label lbCounter = new Label();
            lbCounter.Content = "Wykonano ruchów: ";
            //lbCounter.Margin = new Thickness(20, 2, 20, 2);
            lbCounter.FontSize = 24;
            lbCounter.FontWeight = FontWeights.Bold;
            lbCounter.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(lbCounter, Convert.ToInt32(AmountOfRows) + 3);
            Grid.SetColumn(lbCounter, 1);
            Grid.SetColumnSpan(lbCounter, Convert.ToInt32(AmountOfColumns) - 1);
            gameGrid.Children.Add(lbCounter);
        }

        private void Create_textblocks()
        {
            tbResults.Text = "0";
            tbResults.FontSize = 24;
            tbResults.FontWeight = FontWeights.Bold;
            tbResults.Margin = new Thickness(20, 2, 20, 2);
            Grid.SetRow(tbResults, Convert.ToInt32(AmountOfRows) + 2);
            Grid.SetColumn(tbResults, Convert.ToInt32(AmountOfColumns));
            gameGrid.Children.Add(tbResults);

            tbCounter.Text = "0";
            tbCounter.FontSize = 24;
            tbCounter.FontWeight = FontWeights.Bold;
            tbCounter.Margin = new Thickness(20, 2, 20, 2);
            Grid.SetRow(tbCounter, Convert.ToInt32(AmountOfRows) + 3);
            Grid.SetColumn(tbCounter, Convert.ToInt32(AmountOfColumns));
            gameGrid.Children.Add(tbCounter);
        }

        private void Show_grid()
        {
            gameGrid.ShowGridLines = true;
            GameWindow.Content = gameGrid;
        }

        public void Show_hints()
        {
            Hints hints = new Hints(this, cards);
            hints.CreateHints();
        }
    }
}
