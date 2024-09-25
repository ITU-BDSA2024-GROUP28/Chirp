using System.Runtime.CompilerServices;

namespace SimpleDB;// see later


public record Cheep(string Author, string Message, long Timestamp)
{
    public override string ToString()
    {
        return $"{Author}, \"{Message}\", {Timestamp}";
    }
}

