using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color; // oznaczenie każdego wstąpienia klasy Color jako klasę składową przestrzeni nazw System.Windows.Media (przestrzeń nazw System.Drawing rónież posiada klasę o tej samej nazwie)
using static System.Windows.Media.Colors;   // dodanie statycznej klasy Colors do projektu dzięki czemu nie musimy się odwoływać do niej gdy chcemu użyć jakiegoś jej elementu
using System.Windows;

namespace TheGame
{
    class CardColor
    {
        List<SolidColorBrush> colorList;    // deklaracja odwołania do listy kolorów
        List<Color> tempColorList;      // deklaracja odwołania do tymczasowej listy kolorów która posłuży nam jako pula do losowania
        Random random = new Random();  // stworzeni obiektu klasy Random, który nam posłuży do losowania kolorów

        private int amountOfCards;      // stworzenie zmiennej odpowiedzialnej za ilość kart

        /// <summary>
        /// Konstruktor odpowiedzialny za przekazanie klasie ilości kart
        /// </summary>
        /// <param name="amount">ilość kart</param>
        public CardColor(int amount)
        {
            amountOfCards = amount; // przypisanie zmiennej globalnej wartość ilości kart przekazaną do konstruktora
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie 16 elementowej listy 8 kolorów
        /// </summary>
        /// <returns>zwracamy 16 elementową listę</returns>
        private List<SolidColorBrush> CreateList()
        {
            colorList = new List<SolidColorBrush>(); // stworzenie listy teł dla kart
            tempColorList = new List<Color>(AllColors()); // stworzenie listy kolorów i przypisanie jej zawartości listy stworzenj w klasie CardColor w metodzie AllColors()
            int count = tempColorList.Count;  // stworzenie zmiennej odpowiedzialnej za ilość elementów listy tempColorList
            // losowanie 8 kolorów z listy AllColors()
            for (int i = 0; i < amountOfCards / 2; i++)
            {
                int randomColor = random.Next(count -1); // wylosowanie randomowej liczby z przedziału od 0 do ilość elementów listy tempColorList - 1
                colorList.Add(new SolidColorBrush(tempColorList[randomColor])); // dodanie wylosowanego koloru do listy colorList 
                tempColorList.RemoveAt(randomColor); //usunięcie wylosowanego koloru z puli kolorów
                count--; // zmniejszenie o 1 rozmiaru puli kolorów
            }
            colorList.AddRange(colorList); // dodanie na koniec 8 elementowej listy kolorów, listę colorList (tę samą)
            return colorList;
        }
        
        /// <summary>
        /// Metoda odpowiedzialna z przelosowanie listy stworznej w CreateList()
        /// </summary>
        /// <returns>zwracamy listę losowo ułozonych 8 kolorów</returns>
        public List<SolidColorBrush> RandomAppearanceList()
        {
            List<SolidColorBrush> appearance = new List<SolidColorBrush>(); // stworzenie nowej listy odpowiedzialnej za tła kart
            List<SolidColorBrush> temp = new List<SolidColorBrush>(CreateList());   // stworzenie listy kolorów i przypisanie jej zawartości listy stworzenj w klasie CardColor w metodzie CreateList()
            int count = temp.Count; // stworzenie zmiennej odpowiedzialnej za ilość elementów listy temp
            int randomedColor;  // stworzenie zmiennej losowej
            // pętla odpowiedzialna za przelosowanie listy temp 
            for (int i = 0; i < amountOfCards; i++)
            {
                randomedColor = random.Next(count - 1); // wylosowanie randomowej liczby z przedziału od 0 do ilość elementów listy temp - 1
                appearance.Add(temp[randomedColor]);    // dodanie wylosowanego koloru do listy colorList 
                temp.Remove(temp[randomedColor]);   //usunięcie wylosowanego koloru z puli kolorów
                count--;    // zmniejszenie o 1 rozmiaru puli kolorów
            }
            return appearance;
        }

        /// <summary>
        /// Metoda odpowiedzialna za stworzenie listy unikatowych kolorów
        /// </summary>
        /// <returns>zwracamy listę unikatowych kolorów</returns>
        private List<Color> AllColors() 
        {
            List<Color> allColors = new List<Color>(); // stworzenie nowej listy w której zapiszemy znane nam unikatowe kolory (unikatowe = kolory się niepowtarzają)
            allColors.Add(AntiqueWhite); // dodanie koloru do listy 
            allColors.Add(Aqua);
            allColors.Add(Aquamarine);
            allColors.Add(Black);
            allColors.Add(Blue);
            allColors.Add(BlueViolet);
            allColors.Add(Brown);
            allColors.Add(CadetBlue);
            allColors.Add(Chartreuse);
            allColors.Add(Chocolate);

            allColors.Add(CornflowerBlue);
            allColors.Add(DarkBlue);
            allColors.Add(DarkCyan);
            allColors.Add(DarkGoldenrod);
            allColors.Add(DarkGray);
            allColors.Add(DarkKhaki);
            allColors.Add(DarkMagenta);
            allColors.Add(DarkRed);
            allColors.Add(DarkSlateBlue);
            allColors.Add(DarkSlateGray);

            allColors.Add(DeepPink);
            allColors.Add(DeepSkyBlue);
            allColors.Add(ForestGreen);
            allColors.Add(Fuchsia);
            allColors.Add(Gold);
            allColors.Add(HotPink);
            allColors.Add(IndianRed);
            allColors.Add(LightCoral);
            allColors.Add(LightPink);
            allColors.Add(Orange);

            allColors.Add(Red);
            allColors.Add(Yellow);
            allColors.Add(Tomato);

            return allColors;
        }
    }
}
