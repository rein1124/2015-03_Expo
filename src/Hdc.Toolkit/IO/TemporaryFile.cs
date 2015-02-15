//MEF_Beta_2.zip
// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
#if !SILVERLIGHT

using System;
using System.IO;

namespace Hdc.IO 
{
    public class TemporaryFile : IDisposable
    {
        private string _fileName;

        public TemporaryFile()
        {
            _fileName = Path.GetTempFileName();
        }

        public string FileName
        {
            get { return _fileName; }
        }
        
        public void Dispose()
        {
            if (_fileName != null)
            {
                File.Delete(_fileName);
                _fileName = null;
            }
        }
    }
}

#endif