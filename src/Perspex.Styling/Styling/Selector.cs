﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;

namespace Perspex.Styling
{
    public class Selector
    {
        private readonly Func<IStyleable, SelectorMatch> _evaluate;

        private readonly bool _inTemplate;

        private readonly bool _stopTraversal;

        private string _description;

        public Selector()
        {
            _evaluate = _ => SelectorMatch.True;
        }

        public Selector(
            Selector previous,
            Func<IStyleable, SelectorMatch> evaluate,
            string selectorString,
            bool inTemplate = false,
            bool stopTraversal = false)
            : this()
        {
            Contract.Requires<ArgumentNullException>(previous != null);

            Previous = previous;
            _evaluate = evaluate;
            SelectorString = selectorString;
            _inTemplate = inTemplate || previous._inTemplate;
            _stopTraversal = stopTraversal;
        }

        public Selector Previous
        {
            get; }

        public string SelectorString
        {
            get;
            set;
        }

        public Selector MovePrevious()
        {
            return _stopTraversal ? null : Previous;
        }

        public SelectorMatch Match(IStyleable control)
        {
            List<IObservable<bool>> inputs = new List<IObservable<bool>>();
            Selector selector = this;

            while (selector != null)
            {
                if (selector._inTemplate && control.TemplatedParent == null)
                {
                    return SelectorMatch.False;
                }

                var match = selector._evaluate(control);

                if (match.ImmediateResult == false)
                {
                    return match;
                }
                else if (match.ObservableResult != null)
                {
                    inputs.Add(match.ObservableResult);
                }

                selector = selector.MovePrevious();
            }

            if (inputs.Count > 0)
            {
                return new SelectorMatch(new StyleActivator(inputs));
            }
            else
            {
                return SelectorMatch.True;
            }
        }

        public override string ToString()
        {
            if (_description == null)
            {
                string result = string.Empty;

                if (Previous != null)
                {
                    result = Previous.ToString();
                }

                _description = result + SelectorString;
            }

            return _description;
        }
    }
}
