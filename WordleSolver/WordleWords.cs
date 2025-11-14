using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeoWordleSolver.WordleSolver
{
    public class WordleWords : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> _allWords = new List<string>();

        public List<string> PossibleWords //Easiest way to do it is to update three separate filters and find the overlapping words
        {
            get
            {
                List<string> result = new List<string>();

                result = PossibleWordsFound.Intersect(PossibleWordsKnown).ToList();
                result = result.Intersect(NotImpossibleWords).ToList();

                return result;
            }
        }
        public List<string> PossibleWordsFound { get; set; } = new List<string>();

        public List<string> PossibleWordsKnown { get; set; } = new List<string>();

        public List<string> NotImpossibleWords { get; set; } = new List<string>();

        public int PossibleWordsCount => PossibleWords.Count;

        public List<char> AllChars { get; } = new List<char>()
        {
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z',
            ' '
        };

        public Dictionary<char, LetterContainer> AllLetters { get; set; } = new()
        {
            { 'A', new LetterContainer('A') },
            { 'B', new LetterContainer('B') },
            { 'C', new LetterContainer('C') },
            { 'D', new LetterContainer('D') },
            { 'E', new LetterContainer('E') },
            { 'F', new LetterContainer('F') },
            { 'G', new LetterContainer('G') },
            { 'H', new LetterContainer('H') },
            { 'I', new LetterContainer('I') },
            { 'J', new LetterContainer('J') },
            { 'K', new LetterContainer('K') },
            { 'L', new LetterContainer('L') },
            { 'M', new LetterContainer('M') },
            { 'N', new LetterContainer('N') },
            { 'O', new LetterContainer('O') },
            { 'P', new LetterContainer('P') },
            { 'Q', new LetterContainer('Q') },
            { 'R', new LetterContainer('R') },
            { 'S', new LetterContainer('S') },
            { 'T', new LetterContainer('T') },
            { 'U', new LetterContainer('U') },
            { 'V', new LetterContainer('V') },
            { 'W', new LetterContainer('W') },
            { 'X', new LetterContainer('X') },
            { 'Y', new LetterContainer('Y') },
            { 'Z', new LetterContainer('Z') }
        };

        public List<LetterContainer> Letters => AllLetters.Values.ToList();

        public List<char> KnownLetters { get; set; } = new List<char>();

        private char _firstLetter = ' ';

        public char FirstLetter
        {
            get => _firstLetter;
            set
            {
                _firstLetter = value;
                FilterByFound();
                OnPropertyChanged(nameof(FirstLetter));
            }
        }

        private char _secondLetter = ' ';

        public char SecondLetter
        {
            get => _secondLetter;
            set
            {
                _secondLetter = value;
                FilterByFound();
                OnPropertyChanged(nameof(SecondLetter));
            }
        }

        private char _thirdLetter = ' ';

        public char ThirdLetter
        {
            get => _thirdLetter;
            set
            {
                _thirdLetter = value;
                FilterByFound();
                OnPropertyChanged(nameof(ThirdLetter));
            }
        }

        private char _fourthLetter = ' ';

        public char FourthLetter
        {
            get => _fourthLetter;
            set
            {
                _fourthLetter = value;
                FilterByFound();
                OnPropertyChanged(nameof(FourthLetter));
            }
        }

        private char _fifthLetter = ' ';

        public char FifthLetter
        {
            get => _fifthLetter;
            set
            {
                _fifthLetter = value;
                FilterByFound();
                OnPropertyChanged(nameof(FifthLetter));
            }
        }

        public WordleWords()
        {
            string relativePath = @"..\Words\AllWords.txt";
            string fullPath = Path.GetFullPath(relativePath);

            try
            {
                _allWords = File.ReadAllLines(fullPath).ToList();
                PossibleWordsFound = _allWords;
                PossibleWordsKnown = _allWords;
                NotImpossibleWords = _allWords;

                foreach (LetterContainer letter in AllLetters.Values)
                {
                    letter.PropertyChanged += LetterKnownChanged;
                }
            }
            catch
            {
                throw new Exception("File error!");
            }
        }

        public void FilterByFound()
        {
            List<string> possibleList = new List<string>();

            foreach (string word in _allWords)
            {
                if (word[0] == Char.ToLower(FirstLetter) || FirstLetter == ' ')
                {
                    if (word[1] == Char.ToLower(SecondLetter) || SecondLetter == ' ')
                    {
                        if (word[2] == Char.ToLower(ThirdLetter) || ThirdLetter == ' ')
                        {
                            if (word[3] == Char.ToLower(FourthLetter) || FourthLetter == ' ')
                            {
                                if (word[4] == Char.ToLower(FifthLetter) || FifthLetter == ' ')
                                {
                                    possibleList.Add(word);
                                }
                            }
                        }
                    }
                }
            }

            PossibleWordsFound = possibleList;
            OnPropertyChanged(nameof(PossibleWords));
            OnPropertyChanged(nameof(PossibleWordsFound));
            OnPropertyChanged(nameof(PossibleWordsCount));
        }

        private void LetterKnownChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LetterContainer.IsKnown))
            {
                FilterByKnown();
            }
            else if (e.PropertyName == nameof(LetterContainer.IsImpossible))
            {
                FilterByImpossible();
            }
        } 

        public void FilterByKnown()
        {
            List<string> possibleList = new List<string>();

            foreach (string word in _allWords)
            {
                bool valid = true;
                foreach (LetterContainer letter in AllLetters.Values)
                {
                    if (letter.IsKnown && !word.ToList().Contains(Char.ToLower(letter.Letter)))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid) possibleList.Add(word);
            }

            PossibleWordsKnown = possibleList;
            OnPropertyChanged(nameof(PossibleWords));
            OnPropertyChanged(nameof(PossibleWordsKnown));
            OnPropertyChanged(nameof(PossibleWordsCount));
        }

        public void FilterByImpossible()
        {
            List<string> possibleList = new List<string>();

            foreach (string word in _allWords)
            {
                bool valid = true;
                foreach (LetterContainer letter in AllLetters.Values)
                {
                    if (letter.IsImpossible && word.ToList().Contains(Char.ToLower(letter.Letter)))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid) possibleList.Add(word);
            }

            NotImpossibleWords = possibleList;
            OnPropertyChanged(nameof(PossibleWords));
            OnPropertyChanged(nameof(NotImpossibleWords));
            OnPropertyChanged(nameof(PossibleWordsCount));
        }
    }
}
