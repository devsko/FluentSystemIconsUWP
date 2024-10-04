using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Fluent.Icons
{
    /// <summary>
    /// Represents an icon source that uses a Fluent System Icon as its content.
    /// </summary>
    public class FluentIconSource : PathIconSource
    {
        /// <summary>
        /// Constructs an empty <see cref="FluentIconSource"/>.
        /// </summary>
        public FluentIconSource() { }

        /// <summary>
        /// Constructs an <see cref="IconSource"/> that uses a Fluent System Icon as its content.
        /// </summary>
        public FluentIconSource(FluentSymbol symbol)
        {
            Symbol = symbol;
        }

        /// <summary>
        /// Gets or sets the Fluent System Icons glyph used as the icon content.
        /// </summary>
        public FluentSymbol Symbol
        {
            get { return (FluentSymbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Symbol"/> property.
        /// </summary>
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
            nameof(Symbol), typeof(FluentSymbol), typeof(FluentIconSource),
            new PropertyMetadata(null, new PropertyChangedCallback(OnValueChanged))
        );

        /// <summary>
        /// Gets or sets the size of the icon.
        /// </summary>
        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Size"/> property.
        /// </summary>
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            nameof(Size), typeof(int), typeof(FluentIconSource),
            new PropertyMetadata(24, new PropertyChangedCallback(OnValueChanged))
        );

        /// <summary>
        /// Gets or sets the size of the icon.
        /// </summary>
        public FluentType Type
        {
            get { return (FluentType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Type"/> property.
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            nameof(Type), typeof(FluentType), typeof(FluentIconSource),
            new PropertyMetadata(FluentType.Regular, new PropertyChangedCallback(OnValueChanged))
        );

        /// <summary>
        /// When set to <see langword="true"/>, uses the most similar icon that is available.
        /// </summary>
        public bool EnableBestMatch
        {
            get { return (bool)GetValue(EnableBestMatchProperty); }
            set { SetValue(EnableBestMatchProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="EnableBestMatch"/> property.
        /// </summary>
        public static readonly DependencyProperty EnableBestMatchProperty = DependencyProperty.Register(
            nameof(EnableBestMatch), typeof(bool), typeof(FluentIconSource),
            new PropertyMetadata(false, new PropertyChangedCallback(OnValueChanged))
        );

        public bool IsNoExactMatch
        {
            get { return (bool)GetValue(IsNoExactMatchProperty); }
            private set { SetValue(IsNoExactMatchProperty, value); }
        }

        public static readonly DependencyProperty IsNoExactMatchProperty = DependencyProperty.Register(
            nameof(IsNoExactMatch), typeof(bool), typeof(FluentIconSource), null
        );

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FluentIconSource self = (FluentIconSource)d;
            if (self.GetValue(SymbolProperty) is FluentSymbol symbol)
            {
                // Set internal Data to the Path from the look-up table
                (self.Data, self.IsNoExactMatch) = FluentSymbolIcon.Match(symbol, self.Size, self.Type, self.EnableBestMatch);
            }
        }
    }
}
