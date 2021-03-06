﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Perspex.Styling
{
    /// <summary>
    /// Defines a style.
    /// </summary>
    public class Style : IStyle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        public Style()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        /// <param name="selector">The style selector.</param>
        public Style(Func<Selector, Selector> selector)
        {
            Selector = selector(new Selector());
        }

        /// <summary>
        /// Gets or sets style's selector.
        /// </summary>
        public Selector Selector { get; set; }

        /// <summary>
        /// Gets or sets style's setters.
        /// </summary>
        public IEnumerable<ISetter> Setters { get; set; } = new List<ISetter>();

        /// <summary>
        /// Attaches the style to a control if the style's selector matches.
        /// </summary>
        /// <param name="control">The control to attach to.</param>
        public void Attach(IStyleable control)
        {
            var description = "Style " + Selector.ToString();
            var match = Selector.Match(control);

            if (match.ImmediateResult != false)
            {
                foreach (var setter in Setters)
                {
                    setter.Apply(this, control, match.ObservableResult);
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the style.
        /// </summary>
        /// <returns>A string representation of the style.</returns>
        public override string ToString()
        {
            return "Style: " + Selector.ToString();
        }
    }
}
