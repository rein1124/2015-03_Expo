//MEF_Beta_2.zip
// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------

#if !SILVERLIGHT

using System;
using System.IO;

namespace Hdc.IO 
{
    public class TemporaryDirectory : IDisposable
    {
        private string _directoryPath;

        public TemporaryDirectory()
        {
            _directoryPath = FileIO.GetNewTemporaryDirectory();
        }

        public string DirectoryPath
        {
            get { return _directoryPath; }
        }
        
        public void Dispose()
        {
            if (_directoryPath != null)
            {
                try
                {
                    Directory.Delete(_directoryPath, true);
                }
                catch (IOException)
                {
                }

                _directoryPath = null;
            }
        }
    }
}

#endif