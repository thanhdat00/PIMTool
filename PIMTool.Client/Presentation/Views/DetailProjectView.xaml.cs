using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PIMTool.Client.Presentation.Views
{
    /// <summary>
    /// Interaction logic for DetailProjectView.xaml
    /// </summary>
    public partial class DetailProjectView : UserControl
    {
        public DetailProjectView()
        {
            InitializeComponent();
        }

        private void ProjectNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
