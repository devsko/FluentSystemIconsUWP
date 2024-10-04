using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Fluent.Icons
{
    public class FluentSymbolIcon : Control
    {
        private readonly static ResourceMap symbolResources = ResourceManager.Current.MainResourceMap.GetSubtree("Fluent.Icons/FluentSymbolIcon");

        private PathIcon iconDisplay;

        public FluentSymbolIcon()
        {
            DefaultStyleKey = typeof(FluentSymbolIcon);
        }

        /// <summary>
        /// Constructs a <see cref="FluentSymbolIcon"/> with the specified symbol.
        /// </summary>
        public FluentSymbolIcon(FluentSymbol symbol) : this()
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
            nameof(Symbol), typeof(FluentSymbol), typeof(FluentSymbolIcon),
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
            nameof(Size), typeof(int), typeof(FluentSymbolIcon),
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
            nameof(Type), typeof(FluentType), typeof(FluentSymbolIcon),
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
            nameof(EnableBestMatch), typeof(bool), typeof(FluentSymbolIcon),
            new PropertyMetadata(false, new PropertyChangedCallback(OnValueChanged))
        );

        public bool IsNoExactMatch
        {
            get { return (bool)GetValue(IsNoExactMatchProperty); }
            private set { SetValue(IsNoExactMatchProperty, value); }
        }

        public static readonly DependencyProperty IsNoExactMatchProperty = DependencyProperty.Register(
            nameof(IsNoExactMatch), typeof(bool), typeof(FluentSymbolIcon), null
        );

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("IconDisplay") is PathIcon pi)
            {
                iconDisplay = pi;
                // Awkward workaround for a weird bug where iconDisplay is null
                // when OnSymbolChanged fires in a newly created FluentSymbolIcon
                if (GetValue(SymbolProperty) is FluentSymbol symbol)
                {
                    (iconDisplay.Data, IsNoExactMatch) = Match(symbol, Size, Type, EnableBestMatch);
                }
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FluentSymbolIcon self = (FluentSymbolIcon)d;
            if (self.iconDisplay is PathIcon iconDisplay && self.GetValue(SymbolProperty) is FluentSymbol symbol)
            {
                // Set internal Data to the Path from the look-up table
                (iconDisplay.Data, self.IsNoExactMatch) = Match(symbol, self.Size, self.Type, self.EnableBestMatch);
            }
        }

        /// <summary>
        /// Returns a new <see cref="PathIcon"/> using the path associated with the provided parameters.
        /// </summary>
        /// <param name="symbol">The <see cref="FluentSymbol"/> of the fluent icon.</param>
        /// <param name="desiredSize">The optional desired size of the fluent icon.</param>
        /// <param name="desiredType">The optional desired type of the fluent icon.</param>
        /// <param name="enableBestMatch">If <see langword="true"/> and no exact combination of symbol, size and type is found, the most similar icon found is returned (default is <see langword="false"/>).</param>
        public static PathIcon GetPathIcon(FluentSymbol symbol, int desiredSize = 24, FluentType desiredType = FluentType.Regular, bool enableBestMatch = false) => 
            new()
            {
                Data = (Geometry)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(Geometry), GetPathData(symbol, desiredSize, desiredType, enableBestMatch)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

        /// <summary>
        /// Returns a new <see cref="PathIcon"/> using the path associated with the provided parameters.
        /// </summary>
        /// <param name="symbol">The symbol as int value of the fluent icon.</param>
        /// <param name="desiredSize">The optional desired size of the fluent icon.</param>
        /// <param name="desiredType">The optional desired type of the fluent icon.</param>
        /// <param name="enableBestMatch">If <see langword="true"/> and no exact combination of symbol, size and type is found, the most similar icon found is returned (default is <see langword="false"/>).</param>
        public static Geometry GetPathData(int symbol, int desiredSize = 24, FluentType desiredType = FluentType.Regular, bool enableBestMatch = false) => 
            GetPathData((FluentSymbol)symbol, desiredSize, desiredType, enableBestMatch);

        /// <summary>
        /// Returns a new <see cref="Geometry"/> using the path associated with the provided parameters.
        /// </summary>
        /// <param name="symbol">The <see cref="FluentSymbol"/> of the fluent icon.</param>
        /// <param name="desiredSize">The optional desired size of the fluent icon.</param>
        /// <param name="desiredType">The optional desired type of the fluent icon.</param>
        /// <param name="enableBestMatch">If <see langword="true"/> and no exact combination of symbol, size and type is found, the most similar icon found is returned (default is <see langword="false"/>).</param>
        public static Geometry GetPathData(FluentSymbol symbol, int desiredSize = 24, FluentType desiredType = FluentType.Regular, bool enableBestMatch = false) => 
            enableBestMatch
                ? GetBestMatch(symbol, desiredSize, desiredType).Path
                : symbolResources.TryGetValue($"{symbol}/{desiredSize}-{desiredType}", out NamedResource symbolResource) ? GetPath(symbolResource) : null;

        public static (int Size, FluentType Type, Geometry Path, bool IsNoExactMatch) GetBestMatch(FluentSymbol symbol, int desiredSize = 24, FluentType desiredType = FluentType.Regular)
        {
            ResourceMap resources = symbolResources.GetSubtree(symbol.ToString());

            // Short-circuit a perfect match
            if (resources.TryGetValue($"{desiredSize}-{desiredType}", out NamedResource resource))
            {
                return (desiredSize, desiredType, GetPath(resource), false);
            }

            (int Size, FluentType Type, NamedResource Resource) bestMatch = default;
            int bestScore = int.MinValue;
            foreach (KeyValuePair<string, NamedResource> kvp in resources)
            {
                (int size, FluentType type) = GetSizeAndType(kvp.Key);
                int score = -Math.Abs(desiredSize - size) - Math.Abs(desiredType - type) * 50;
                if (score > bestScore)
                {
                    bestMatch = (size, type, kvp.Value);
                    bestScore = score;
                }
            }

            return (bestMatch.Size, bestMatch.Type, GetPath(bestMatch.Resource), true);
        }

        public static (int Size, FluentType Type)[] GetAllVariants(FluentSymbol symbol) => 
            symbolResources.GetSubtree(symbol.ToString()).Keys.Select(GetSizeAndType).ToArray();

        internal static (Geometry Path, bool IsNoExactMatch) Match(FluentSymbol symbol, int desiredSize, FluentType desiredType, bool enableBestMatch)
        {
            if (enableBestMatch)
            {
                (_, _, Geometry path, bool isNoExactMatch) = GetBestMatch(symbol, desiredSize, desiredType);
                return (path, isNoExactMatch);
            }
            else
            {
                return symbolResources.TryGetValue($"{symbol}/{desiredSize}-{desiredType}", out NamedResource symbolResource)
                    ? (GetPath(symbolResource), false)
                    : (null, true);
            }
        }

        private static (int Size, FluentType Type) GetSizeAndType(string key)
        {
            int pos = key.IndexOf('-');
            int size = int.Parse(key.Substring(0, pos));
            FluentType type = (key.Substring(pos + 1)) switch
            {
                "regular" => FluentType.Regular,
                "filled" => FluentType.Filled,
                "light" => FluentType.Light,
                _ => throw new ArgumentException()
            };

            return (size, type);
        }

        private static Geometry GetPath(NamedResource resource) => 
            (Geometry)Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(Geometry), resource.Resolve().ValueAsString);
    }
}
