using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoWordleSolver.WordleSolver
{
    public class LetterContainer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public char Letter { get; init; }

        private bool _isImpossible = false;

        public bool IsImpossible
        {
            get => _isImpossible;
            set
            {
                _isImpossible = value;
                OnPropertyChanged(nameof(IsImpossible));
            }
        }

        private bool _isKnown = false;

        public bool IsKnown
        {
            get => _isKnown;
            set
            {
                _isKnown = value;
                OnPropertyChanged(nameof(IsKnown));
            }
        }

        public LetterContainer(char letter)
        {
            Letter = letter;
        }
    }
}
