using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for SpecificsControl.xaml
    /// </summary>
    public partial class SpecificsControl : UserControl
    {
        public event RoutedEventHandler? ClearButtonClicked;

        public SpecificsControl()
        {
            InitializeComponent();
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            ClearButtonClicked?.Invoke(sender, e);
        }
    }
}
