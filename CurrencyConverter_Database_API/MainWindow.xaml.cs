using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CurrencyConverter_Database_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection;

        private SqlCommand sqlCommand;

        private SqlDataAdapter sqlDataAdapter;

        public MainWindow()
        {
            InitializeComponent();
            SetCurrentYear();

            // Initialize the database connection and bind currency data to the DataGrid
            BindCurrencyData();

        }

        /// <summary>
        /// This method establishes a connection to the database using the connection string from the configuration file.
        /// We can then use this connection to execute SQL commands and queries.
        /// </summary>
        public void DbConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// This method retrieves currency data from the Currency_Master table in the database and 
        /// binds it to two ComboBoxes (cmbFromCurrency and cmbToCurrency).
        /// </summary>
        public void BindCurrencyData()
        {
            DbConnection();
            try
            {
                // Create a DataTable to hold the data from the Currency_Master table
                DataTable dataTable = new DataTable();

                string query = "SELECT * FROM Currency_Master";

                // Create a query to get data from Currency_Master table
                sqlCommand = new SqlCommand(query, sqlConnection);

                // Create a SqlDataAdapter to execute the query and fill the DataTable
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);


                DataRow newRow = dataTable.NewRow();
                newRow["Id"] = 0;

                newRow["CurrencyName"] = "--SELECT--";

                dataTable.Rows.InsertAt(newRow, 0);

                if (dataTable.Rows.Count > 0)
                {
                    // Assign the DataTable to cmbFromCurrency and cmbToCurrency ComboBoxes
                    cmbFromCurrency.ItemsSource = dataTable.DefaultView;
                    cmbToCurrency.ItemsSource = dataTable.DefaultView;
                }
                sqlConnection.Close();

                // Set DisplayMemberPath and SelectedValuePath for both ComboBoxes and set default selected index to 0
                cmbFromCurrency.DisplayMemberPath = "CurrencyName";
                cmbFromCurrency.SelectedValuePath = "Id";
                cmbFromCurrency.SelectedIndex = 0;

                // Set DisplayMemberPath and SelectedValuePath for both ComboBoxes and set default selected index to 0
                cmbToCurrency.DisplayMemberPath = "CurrencyName";
                cmbToCurrency.SelectedValuePath = "Id";
                cmbToCurrency.SelectedIndex = 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
        }

        public void DgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
        }

        public void Clear_Click(object sender, RoutedEventArgs e)
        {
        }
        public void Convert_Click(object sender, RoutedEventArgs e)
        {
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
        }
        public void OnInputTextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public void Swap_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void SetCurrentYear()
        {
            int year = System.DateTime.Now.Year;
            currentYear.Text = year + " ";
        }
    }
}
