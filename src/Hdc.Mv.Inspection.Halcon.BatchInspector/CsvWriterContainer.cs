using System;
using System.IO;
using CsvHelper;

namespace Hdc.Mv.Inspection.Halcon.BatchInspector
{
    public class CsvWriterContainer : IDisposable
    {
        private readonly StreamWriter _writer;
        private readonly FileStream _stream;
        public CsvWriter CsvWriter { get; private set; }

        public CsvWriterContainer(string fileName)
        {
            _stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _writer = new StreamWriter(_stream);
            CsvWriter = new CsvWriter(_writer);
        }

        public void Dispose()
        {
            CsvWriter.Dispose();
            _writer.Dispose();
            _stream.Dispose();
        }
    }
}