﻿using System;
using System.Collections.Generic;

namespace Assets.scripts.Utils.Disposables
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Retain(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach(var disposables in _disposables)
            {
                disposables.Dispose();
            }

            _disposables.Clear();
        }
    }
}