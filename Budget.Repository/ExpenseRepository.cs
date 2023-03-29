using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Budget.Model;
using System.Diagnostics;
using Budget.Repository.Common;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Budget.Common;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Specialized;

namespace Budget.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        //laptop
        //private string connectionString = "Data Source=DESKTOP-D467OFD\\MOJSQLSERVER;Initial Catalog=Budget;Integrated Security=True";

        //home
        private string connectionString = "Data Source=DESKTOP-413NSIC\\SQLEXPRESS01;Initial Catalog=KucniBudget;Integrated Security=True";


        private Expense PopulateExpenseWithReaderData(SqlDataReader reader)
        {
            Expense expense = new Expense();
            expense.Id = reader.GetGuid(0);
            expense.PersonId = reader.GetGuid(1);
            expense.CategoryId = reader.GetGuid(2);
            expense.Name = reader.GetString(3);
            expense.Date = reader.GetDateTime(4);
            expense.Description = !reader.IsDBNull(5) ? reader.GetString(5) : expense.Description;
            expense.Cost = reader.GetDecimal(6);

            return expense;
        }

        private async Task<string> FindIdByName(string tableName, string columnName, string value)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"SELECT Id FROM [{tableName}]");
                    sb.AppendLine($"WHERE [{columnName}] = '{value}';");
                    SqlCommand command = new SqlCommand(sb.ToString(), connection);
                    //command.Parameters.AddWithValue("@value", value);

                    command.Connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (!reader.HasRows)
                    {
                        command.Connection.Close();
                        return null;
                    }

                    reader.Read();
                    //Expense expense = PopulateExpenseWithReaderData(reader);

                    return reader.GetGuid(0).ToString();
                    //reader.Close();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        private async Task<Expense> GetExpenseItemByIdAsync(string id)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM [Expense] WHERE [Id] = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);

                    command.Connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (!reader.HasRows)
                    {
                        command.Connection.Close();
                        return null;
                    }

                    reader.Read();
                    Expense expense = PopulateExpenseWithReaderData(reader);
                    reader.Close();

                    return expense;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<List<Expense>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            // if no parameters, create default objects
            if (paging == null) paging = new Paging();
            if (sorting == null) sorting = new Sorting();
            if (filtering == null) filtering = new Filtering();

            // set sorting order
            string sortingOrder = "ASC";
            if (sorting.SortOrderAsc == false) sortingOrder = "DESC";


            SqlConnection connection = new SqlConnection(connectionString);
            List<Expense> expenses = new List<Expense>();

            StringBuilder sb = new StringBuilder();

            // create list with filtering conditions
            List<string> filteringQuery = new List<string>();


            if (filtering.Person != null)
            {
                string personId = await FindIdByName("Person", "DisplayName", filtering.Person);
                filteringQuery.Add($"PersonId = '{personId}'");
            }
            if (filtering.Category != null)
            {
                string categoryId = await FindIdByName("Category", "Name", filtering.Category);
                filteringQuery.Add($"CategoryId = '{categoryId}'");
            }

            if (filtering.DateFrom != null)
            {
                filteringQuery.Add($"DATE >= '{filtering.DateFrom}'");
            }

            if (filtering.DateTo != null)
            {
                filteringQuery.Add($"DATE <= '{filtering.DateTo}'");
            }

            if (filtering.CostFrom != null)
            {
                filteringQuery.Add($"COST >= '{filtering.CostFrom}'");
            }

            if (filtering.CostTo != null)
            {
                filteringQuery.Add($"COST <= '{filtering.CostTo}'");
            }

            // create query string
            sb.AppendLine("SELECT * FROM [Expense]");

            // filtering has parameters? add to query
            if (filteringQuery.Any()) sb.AppendLine("WHERE " + string.Join(" AND ", filteringQuery.ToArray()));

            sb.AppendLine($"ORDER BY [{sorting.OrderBy}] {sortingOrder}");
            sb.AppendLine("OFFSET @offset ROWS");
            sb.AppendLine("FETCH NEXT @pagesize ROWS ONLY");
            sb.AppendLine(";");

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand(sb.ToString(), connection);
                    command.Parameters.AddWithValue("@offset", (paging.PageNumber - 1) * paging.PageSize);
                    command.Parameters.AddWithValue("@pagesize", paging.PageSize);

                    command.Connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (!reader.HasRows)
                    {
                        command.Connection.Close();
                        return null;
                    }
                    while (reader.Read())
                    {
                        expenses.Add(PopulateExpenseWithReaderData(reader));
                    }
                    reader.Close();
                    command.Connection.Close();
                    return expenses;

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }



        public async Task<Expense> GetByIdAsync(string id)
        {
            Expense expense = await GetExpenseItemByIdAsync(id);
            if (expense == null)
            {
                return null;
            }
            return expense;
        }


        public async Task<int> PostAsync(Expense expenseFromBody)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand
                        ("INSERT INTO [Expense] ([Id],[PersonId], [CategoryId], [Name], [Date],[Cost]) VALUES " +
                        "(@Id, @personId, @categoryId, @name, @date, @cost);", connection);
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@personId", expenseFromBody.PersonId);
                    command.Parameters.AddWithValue("@categoryId", expenseFromBody.CategoryId);
                    command.Parameters.AddWithValue("@name", expenseFromBody.Name);
                    command.Parameters.AddWithValue("@date", expenseFromBody.Date);
                    command.Parameters.AddWithValue("@cost", expenseFromBody.Cost);

                    command.Connection.Open();

                    int RowsAffected = await command.ExecuteNonQueryAsync();

                    if (RowsAffected > 0)
                    {
                        command.Connection.Close();
                        return RowsAffected;
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    return 0;
                }
            }
        }




        public async Task<bool> DeleteByIdAsync(string id)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("DELETE FROM [Expense] WHERE [Id] = @id;", connection);
                    command.Parameters.AddWithValue("@Id", id);

                    command.Connection.Open();

                    if (await command.ExecuteNonQueryAsync() > 0)
                    {
                        command.Connection.Close();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }


        public async Task<bool> UpdateByIdAsync(string id, Expense expenseUpdated)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                try
                {
                    //if expense with id exist ..
                    SqlCommand command = new SqlCommand
                        ("UPDATE [Expense] SET [Name] = @name, [PersonId] = @personId, [CategoryId] = @categoryId, [Cost] = @cost, [Date] = @date WHERE [Id] = @id;", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@name", expenseUpdated.Name);
                    command.Parameters.AddWithValue("@cost", expenseUpdated.Cost);
                    command.Parameters.AddWithValue("@date", expenseUpdated.Date);
                    command.Parameters.AddWithValue("@personId", expenseUpdated.PersonId);
                    command.Parameters.AddWithValue("@categoryId", expenseUpdated.CategoryId);

                    command.Connection.Open();

                    if (await command.ExecuteNonQueryAsync() > 0)
                    {
                        command.Connection.Close();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }


        }







    }
}
