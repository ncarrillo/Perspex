﻿namespace Perspex.Controls.Documents
{
    using Perspex.Media;

    /// <summary>
    /// An inline-level element intended to contain a run of formatted or unformatted text.
    /// </summary>
    public class Run : Inline
    {
        /// <summary>
        /// Defines the <see cref="Text"/> property.
        /// </summary>
        public static readonly PerspexProperty<string> TextProperty =
            PerspexProperty.Register<Run, string>(nameof(Text));

        /// <summary>
        /// Gets or sets the text of the run.
        /// </summary>
        public string Text
        {
            get { return GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="Foreground"/> property.
        /// </summary>
        public static readonly PerspexProperty<SolidColorBrush> ForegroundProperty =
            PerspexProperty.Register<Run, SolidColorBrush>(nameof(Foreground), Brushes.Black, true);

        /// <summary>
        /// Gets or sets the foreground of the run.
        /// </summary>
        public SolidColorBrush Foreground
        {
            get { return GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="FontWeight"/> property.
        /// </summary>
        public static readonly PerspexProperty<FontWeight> FontWeightProperty =
            PerspexProperty.Register<Run, FontWeight>(nameof(FontWeight), FontWeight.Normal, true);

        /// <summary>
        /// Gets or sets the font weight of the run.
        /// </summary>
        public FontWeight FontWeight
        {
            get { return GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        /// <summary>
        /// Defines the <see cref="FontSize"/> property.
        /// </summary>
        public static readonly PerspexProperty<double> FontSizeProperty =
            PerspexProperty.Register<Run, double>(nameof(FontSize), 13.0, true);

        /// <summary>
        /// Gets or sets the font weight of the run.
        /// </summary>
        public double FontSize
        {
            get { return GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
    }
}