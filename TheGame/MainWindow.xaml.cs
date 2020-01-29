using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheGame
{
    /// <summary>
    /// Klasa odpowiedzilana za wywołanie gotwych elementów
    /// tj. siatki i podpowiedzi
    /// </summary>
    public partial class MainWindow : Window
    {
        GameGrid game;  // deklaracja obiektu klasy GameGrid
        public MainWindow()
        {
            InitializeComponent();  // metoda odpowiedzialna za stworzenie okna aplikacji (systemowa metoda)
            game = new GameGrid(this);  // stworzenie obiektu klasy GameGrid
            game.CreateGrid(); // stworzenie siatki do gry
            game.Show_hints(); // pokazanie podpowiedzi na kartach
            
        }
    }
}
