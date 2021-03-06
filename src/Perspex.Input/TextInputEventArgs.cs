﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perspex.Interactivity;

namespace Perspex.Input
{
    public class TextInputEventArgs : RoutedEventArgs
    {
        public IKeyboardDevice Device { get; set; }

        public string Text { get; set; }
    }
}
