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
using System.Data.OleDb;
using System.Configuration;

namespace AdoNetLess2HW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ConStr = "";
        public List<string> prs = new List<string>();
        public OleDbConnection con = new OleDbConnection();
        public MainWindow()
        {
            InitializeComponent();
            prs.Add(@"Provider=sqloledb;Data Source={3};Initial Catalog={0};User Id = {1}; Password = {2};");
            prs.Add(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User Id={1};Password={2};");

        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }

            try
            {
                StBarLabel.Foreground = new SolidColorBrush(Colors.Black);
                con.ConnectionString = ConnectionStringTextBlock.Text;
                con.Open();
                StBarLabel.Content = con.State;
            }

            catch (Exception ex)
            {
                StBarLabel.Foreground = new SolidColorBrush(Colors.Red);
                StBarLabel.Content = ex.Message;
            }
            
            
            
        }

     

        private void ComboBox_Selection(object sender, SelectionChangedEventArgs e)
        {
            TextChangedEventArgs ev = new TextChangedEventArgs(e.RoutedEvent,UndoAction.Undo);
            TextBox_TextChanged(sender, ev);

        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConStr = String.Format(prs[(ProviderComboBox.SelectedIndex)], DataBaseTextBox.Text, UserTextBox.Text, PasswordBox.Password, HostNameTextBox.Text);
            ConnectionStringTextBlock.Text = ConStr;
        }

        private void Password_TextChanged(object sender, RoutedEventArgs e)
        {
            TextChangedEventArgs ev = new TextChangedEventArgs(e.RoutedEvent, UndoAction.Undo);
            TextBox_TextChanged(sender, ev);
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StBarLabel.Foreground = new SolidColorBrush(Colors.Black);
                OleDbCommand com = new OleDbCommand(QueryTextBox.Text, con);
                int k = com.ExecuteNonQuery();
                QueryTextBox.Text += "\n\n" + k + " row affected";
            }

            catch(Exception ex)
            {
                StBarLabel.Foreground = new SolidColorBrush(Colors.Red);
                StBarLabel.Content = ex.Message;
            }
           
           
        }
    }
}
