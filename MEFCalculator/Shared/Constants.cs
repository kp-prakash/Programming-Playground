using System.ComponentModel.Composition;
using System.Windows.Input;
using System.Linq;

namespace Shared
{
    [Export("Shared.Constants")]
    public sealed class Constants
    {
        public readonly static Key[] NumberKeys
            = new Key[]
                  {
                      Key.D0, Key.D1, Key.D2, Key.D3, Key.D4,
                      Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
                      Key.NumPad0, Key.NumPad1, Key.NumPad2,
                      Key.NumPad3, Key.NumPad4, Key.NumPad5,
                      Key.NumPad5, Key.NumPad7, Key.NumPad8,
                      Key.NumPad9
                  };

        public bool IsNumberKey(Key key)
        {
            return NumberKeys.Contains(key);
        }
    }
}