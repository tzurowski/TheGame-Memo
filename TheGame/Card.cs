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
    class Card
    {   
        Button card;    // deklaracja odwołania do obiektu klasy Button

        /// <summary>
        /// Metoda klasy Card odpowiedzialna za stworzenie pojedyńczej karty
        /// </summary>
        /// <param name="buttonID">indywidualny numer identyfikujący przycisk</param>
        /// <param name="width">szerokość przycisku, domyślnie 100</param>
        /// <param name="height">wysokość przycisku, domyślnie 100</param>
        /// <returns>zwracamy stworzoną kartę (przycisk), obiekt klasy Button</returns>
        public Button CreateCard(int buttonID, int width = 100, int height = 100)
        {
            card = new Button();    // stworzenie nowego obiektu klasy Button (tworzenie przycisku/karty)
            card.Width = width;     // nadanie przyciskowi szerokości przesłanej do metody jako parametr  
            card.Height = height;   // nadanie przyciskowi wysokości przesłanej do metody jako parametr
            card.Name = $"btn{buttonID}";   // nadanie nazwy nowo stworzonemu przyciskowi (np. btn1, gdzie 1 to przesłane ID do metody)
            card.FontSize = 20;     // ustawienie wielkości czcionki na 20px (potrzebne dla lepszej widoczności dla wskazówek)
            card.FontWeight = FontWeights.UltraBold;    // pogrubienie czcionki znajdującej sie na przycisku 
            card.Background = new SolidColorBrush(Colors.LightGray); // nadanie domyślnego koloru tła przycisku jako LightGray
            //card.AddHandler(Button.ClickEvent, new RoutedEventHandler(gameLogic.RevealCard)); <- to jest zbędne aczkolwiek służy jako wywołanie zdalnej metody podczas wywołania zdarzenia (kliknięcie) 

            return card;
        }
        
    }
}

