using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    public class FailedEventArgs : EventArgs
    {
        public Player? Player { get; private set; }
        public Exception? Exception { get; private set; }

        public FailedEventArgs(Player player, Exception exception)
        {
            Player = player;
            Exception = exception;
        }

        public FailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
