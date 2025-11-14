using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoWordleSolver.WordleSolver
{
    public class LetterUpdateEventArgs : EventArgs
    {
        public string Message { get; set; }

        public LetterUpdateEventArgs(string message)
        {
            Message = message;
        }
    }
}
