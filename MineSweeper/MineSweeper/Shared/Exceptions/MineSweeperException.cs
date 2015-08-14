using System;

namespace MineSweeper.Shared.Exceptions
{
    /// <summary>
    /// Represent any runtime exception while running mine sweeper.
    /// </summary>
    public class MineSweeperException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MineSweeperException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MineSweeperException(string message)
            : base(message)
        {
        }
    }
}