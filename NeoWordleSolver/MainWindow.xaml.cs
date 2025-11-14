using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NeoWordleSolver.WordleSolverView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new WordleWords();
            SpecificsView.ClearButtonClicked += HandleClearButtonClicked;
        }

        private void HandleClearButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is WordleWords wordle)
            {
                wordle.FirstLetter = ' ';
                wordle.SecondLetter = ' ';
                wordle.ThirdLetter = ' ';
                wordle.FourthLetter = ' ';
                wordle.FifthLetter = ' ';

                foreach (LetterContainer letter in wordle.AllLetters.Values)
                {
                    letter.IsImpossible = false;
                    letter.IsKnown = false;
                }
            }
        }
    }
}