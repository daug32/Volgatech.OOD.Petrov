using Lab1.Models;

namespace Lab1.Tasks.Parsers;

internal interface IShapeParser
{
    public IShape ParseShape( string data );
}