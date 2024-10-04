using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Fluent.Icons
{
    /// <summary>
    /// Represents an icon that uses a <see cref="FluentSymbol"/> for its content.
    /// </summary>
    public sealed class FluentIconElement : PathIcon
    {
        /// <summary>
        /// Constructs an empty <see cref="FluentIconElement"/>.
        /// </summary>
        public FluentIconElement() { }

        /// <summary>
        /// Constructs a <see cref="FluentIconElement"/> displaying the specified symbol.
        /// </summary>
        /// <param name="symbol"></param>
        public FluentIconElement(FluentSymbol symbol)
        {
            Symbol = symbol;
        }

        /// <summary>
        /// Constructs a <see cref="FluentIconElement"/> with the specified source.
        /// </summary>
        public FluentIconElement(FluentIconSource source)
        {
            Data = source.Data;
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
            nameof(Symbol), typeof(FluentSymbol), typeof(FluentIconElement),
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
            nameof(Size), typeof(int), typeof(FluentIconElement),
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
            nameof(Type), typeof(FluentType), typeof(FluentIconElement),
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
            nameof(EnableBestMatch), typeof(bool), typeof(FluentIconElement),
            new PropertyMetadata(false, new PropertyChangedCallback(OnValueChanged))
        );

        public bool IsNoExactMatch
        {
            get { return (bool)GetValue(IsNoExactMatchProperty); }
            private set { SetValue(IsNoExactMatchProperty, value); }
        }

        public static readonly DependencyProperty IsNoExactMatchProperty = DependencyProperty.Register(
            nameof(IsNoExactMatch), typeof(bool), typeof(FluentIconElement), null
        );

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FluentIconElement self = (FluentIconElement)d;
            if (self.GetValue(SymbolProperty) is FluentSymbol symbol)
            {
                // Set internal Data to the Path from the look-up table
                (self.Data, self.IsNoExactMatch) = FluentSymbolIcon.Match(symbol, self.Size, self.Type, self.EnableBestMatch);
            }
        }
    }
}
