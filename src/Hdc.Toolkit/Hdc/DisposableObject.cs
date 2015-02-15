//MEF_Beta_2.zip
// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace Hdc
{
    public class DisposableObject : IDisposable
    {
        public int DisposeCount
        {
            get;
            private set;
        }

        public void Dispose()
        {
            DisposeCount++;
        }
    }
}