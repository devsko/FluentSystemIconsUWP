﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Fluent.Icons;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FluentSystemTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public FluentSymbol[] AllFluentSymbols => (FluentSymbol[])Enum.GetValues(typeof(FluentSymbol));

        public MainPage()
        {
            this.InitializeComponent();

            ButtonPanel.Children.Add(
                new FluentSymbolIcon(FluentSymbol.Icons24)
            );
            ButtonPanel.Children.Add(
                new FluentIconElement(FluentSymbol.AppFolder24)
                {
                    HorizontalAlignment = HorizontalAlignment.Center
                }
            );
        }
    }
}
