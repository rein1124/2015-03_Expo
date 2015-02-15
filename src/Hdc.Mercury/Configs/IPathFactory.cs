using System;

namespace Hdc.Mercury.Configs
{
    public interface IPathFactory
    {
        string Create(string path, int tagIndex, int tagIndexLength);

        string Create(string path, int tagIndex, int tagIndexLength, int generationIndex);

        //string Create(string path, int tagIndex, int tagIndexLength, bool isArray);
        string CreateArrayPath(string path, int tagIndex);

        string CreateArrayPath(string path, int tagIndex, int generationIndex);
    }
}