using System;

namespace MineSweeper.Shared.Exceptions
{
    /// <summary>
    /// Represents game over and occurs when player opens a mine.
    /// </summary>
    public class GameOverException : Exception
    {
        // TODO: We could use MineSweeperException instead of this.
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GameOverException(string message)
            : base(message)
        {
        }
    }
}