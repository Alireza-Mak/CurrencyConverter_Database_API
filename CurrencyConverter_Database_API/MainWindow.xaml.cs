using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private int currencyId;

        public MainWindow()
        {
            InitializeComponent();
            SetCurrentYear();

            // Clear all input controls
            ClearControls();

            //clear master
            ClearMaster();


        }

        /// <summary>
        /// This method establishes a connection to the database using the connection string from the configuration file.
        /// We can then use this connection to execute SQL commands and queries.
        /// </summary>
        public void OpenConnection()
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
            OpenConnection();
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


        /// <summary>
        /// This method clears all input controls on the form, including text boxes and combo boxes.
        /// </summary>
        public void ClearControls()
        {
            try
            {
                txtCurrency.Text = string.Empty;
                if (cmbFromCurrency.Items.Count > 0)
                {
                    cmbFromCurrency.SelectedIndex = 0;
                }
                if (cmbToCurrency.Items.Count > 0)
                {
                    cmbToCurrency.SelectedIndex = 0;
                }
                lblCurrency.Content = "";

                txtCurrency.Focus();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method opens a database connection, executes a query to fetch all records from
        /// the  Currency_Master table, and populates the data grid view with the results. If no records  are found, the
        /// data grid view is cleared.
        /// </summary>
        public void GetData()
        {
            OpenConnection();
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM Currency_Master";
            sqlCommand = new SqlCommand(query, sqlConnection);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                dgvCurrency.ItemsSource = dataTable.DefaultView;

            }
            else
            {
                dgvCurrency.ItemsSource = null;
            }
            sqlConnection.Close();
        }

        /// <summary>
        /// This method clears the input fields and resets the form to its initial state.
        /// </summary>
        public void ClearMaster()
        {
            try
            {
                txtRate.Text = string.Empty;
                txtCurrencyName.Text = string.Empty;
                btnSave.Content = "Save";
                GetData();
                currencyId = 0;
                BindCurrencyData();
                txtRate.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method validates the input fields for currency name and rate before performing
        /// the save or update operation. If the <c>currencyId</c> is greater than zero, the method updates the existing
        /// currency record; otherwise, it inserts a new record. Confirmation dialogs are displayed before performing
        /// save or update operations. Any exceptions encountered during the operation are displayed to the user in a
        /// message box.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Save button.</param>
        /// <param name="e">The event data associated with the click event.</param>
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtCurrencyName.Text == null || txtCurrencyName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter currency name", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtCurrencyName.Focus();
                    return;
                }
                else if (txtRate.Text == null || txtRate.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter rate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtRate.Focus();
                    return;
                }
                if (currencyId != 0 && currencyId > 0)
                {
                    if (MessageBox.Show("Are you sure you want to Update ?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpenConnection();
                        string query = "UPDATE Currency_Master SET Rate = @Rate, CurrencyName = @CurrencyName WHERE Id = @Id";
                        sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlCommand.Parameters.AddWithValue("@Rate", txtRate.Text);
                        sqlCommand.Parameters.AddWithValue("@CurrencyName", txtRate.Text);
                        sqlCommand.Parameters.AddWithValue("@Id", currencyId);
                        sqlCommand.ExecuteScalar();
                        sqlConnection.Close();
                        MessageBox.Show("Data Updated successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    ClearMaster();
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to Save ?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpenConnection();
                        string query = "INSERT INTO Currency_Master (CurrencyName, Rate) VALUES (@CurrencyName, @Rate)";
                        sqlCommand = new SqlCommand(query, sqlConnection);
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        sqlCommand.Parameters.AddWithValue("@CurrencyName", txtCurrencyName.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Rate", txtRate.Text);
                        sqlCommand.ExecuteScalar();
                        sqlConnection.Close();
                    }
                    ClearMaster();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles the click event for the Cancel button.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Cancel button.</param>
        /// <param name="e">The event data associated with the click event.</param>
        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearMaster();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// This method performs the following actions based on the selected cell: 
        /// If the selected cell is in the "Edit" column, it populates the UI fields with the selected currency's details for editing.
        /// If the selected cell is in the "Delete" column, it prompts the user for confirmation and deletes the selected currency if
        /// confirmed, the method assumes that the DataGrid's items are bound to a collection of objects,  and that the "Id", "Rate",
        /// and "CurrencyName" fields exist in the data source.
        /// </summary>
        /// <param name="sender">The source of the event, typically the <see cref="DataGrid"/> that triggered the event.</param>
        /// <param name="e">The event data containing information about the changed selection.</param>
        public void DgvCurrency_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //Create object for DataGrid
                DataGrid grid_container = (DataGrid)sender;

                //Create object for DataRowView
                DataRowView row_selected = grid_container.CurrentItem as DataRowView;

                //row_selected is not null
                if (row_selected != null)
                {
                    if (grid_container.Items.Count > 0)
                    {
                        if (dgvCurrency.Items.Count > 0)
                        {
                            if (grid_container.SelectedCells.Count > 0)
                            {
                                // Get the Id of the selected currency from the DataRowView
                                currencyId = Int32.Parse(row_selected["Id"].ToString());

                                //DisplayIndex is equal to one than it is Edit cell  
                                if (grid_container.SelectedCells[0].Column.DisplayIndex == 0)
                                {
                                    txtRate.Text = row_selected["Rate"].ToString();
                                    txtCurrencyName.Text = row_selected["CurrencyName"].ToString();
                                    btnSave.Content = "Update";
                                }

                                //DisplayIndex is equal to one than it is Delete cell  
                                if (grid_container.SelectedCells[0].Column.DisplayIndex == 1)
                                {
                                    if (MessageBox.Show("Are you sure you want to delete ?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        OpenConnection();
                                        string query = "DELETE FROM Currency_Master WHERE Id=@Id";
                                        sqlCommand = new SqlCommand(query, sqlConnection);
                                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                                        sqlCommand.Parameters.AddWithValue("@Id", currencyId);
                                        sqlCommand.ExecuteScalar();
                                        sqlConnection.Close();
                                        MessageBox.Show("Data deleted successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                        ClearMaster();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Clear_Click(object sender, RoutedEventArgs e)
        {
        }
        public void Convert_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// This method is used to validate the input in the currency text box to ensure that only numeric values are entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
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
