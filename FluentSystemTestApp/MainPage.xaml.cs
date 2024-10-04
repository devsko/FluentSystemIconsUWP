using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Fluent.Icons;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FluentSystemTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public FluentSymbol[] AllFluentSymbols => (FluentSymbol[])Enum.GetValues(typeof(FluentSymbol));
        public Variant[] AllVariants { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ButtonPanel.Children.Add(
                new FluentSymbolIcon(FluentSymbol.Icons)
            );
            ButtonPanel.Children.Add(
                new FluentIconElement(FluentSymbol.AppFolder)
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                }
            );
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Variants.ItemsSource = Symbols.SelectedItem is FluentSymbol symbol 
                ? FluentSymbolIcon
                    .GetAllVariants(symbol)
                    .Select(t => new Variant { Symbol = symbol, Size = t.Size, Type = t.Type })
                    .ToArray() 
                : null;
        }
    }

    public class Variant
    {
        public FluentSymbol Symbol { get; set; }
        public int Size { get; set; }
        public FluentType Type { get; set; }
    }
}
