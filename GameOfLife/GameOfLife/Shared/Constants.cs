using System.Collections.Generic;

namespace Shared
{
    public sealed class Constants
    {
        public const char Alive = 'X';
        public const char Dead = '-';
        public const char NewLine = '\n';

        public const int MinAliveNeighbours = 2;
        public const int MaxAliveNeighbours = 3;

        public static readonly IList<Delta> Deltas = new List<Delta>
                                                         {
                                                             new Delta(-1, -1),
                                                             new Delta(0, -1),
                                                             new Delta(1, -1),
                                                             new Delta(-1, 0),
                                                             new Delta(1, 0),
                                                             new Delta(-1, 1),
                                                             new Delta(0, 1),
                                                             new Delta(1, 1)
                                                         };
    }
}