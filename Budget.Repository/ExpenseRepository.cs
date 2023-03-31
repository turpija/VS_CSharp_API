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
using System.Reflection;

namespace Budget.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        //laptop
        private string connectionString = "Data Source=DESKTOP-D467OFD\\MOJSQLSERVER;Initial Catalog=Budget;Integrated Security=True";

        //home
        //private string connectionString = "Data Source=DESKTOP-413NSIC\\SQLEXPRESS01;Initial Catalog=KucniBudget;Integrated Security=True";


        private ExpenseDTO PopulateExpenseWithReaderData(SqlDataReader reader)
        {
            ExpenseDTO expense = new ExpenseDTO();

            expense.Id = reader.GetGuid(0);
            //expense.PersonId = reader.GetGuid(1);
            expense.Person = new PersonDTO()
            {
                Id = reader.GetGuid(1),
                Username = reader.GetString(2),
                Password = reader.GetString(3),
                Email = reader.GetString(4)
            };
            //expense.CategoryId = reader.GetGuid(5);
            expense.Category = new CategoryDTO()
            {
                Id = reader.GetGuid(5),
                Name = reader.GetString(6)
            };
            expense.Name = reader.GetString(7);
            expense.Date = reader.GetDateTime(8);
            expense.Cost = reader.GetDecimal(9);
            expense.Description = !reader.IsDBNull(10) ? reader.GetString(10) : expense.Description;

            return expense;
        }

        private async Task<ExpenseDTO> GetExpenseItemByIdAsync(string id)
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
                    ExpenseDTO expense = PopulateExpenseWithReaderData(reader);
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

        public async Task<List<ExpenseDTO>> GetAllAsync(Paging paging, Sorting sorting, Filtering filtering)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            StringBuilder sb = new StringBuilder();
            List<ExpenseDTO> expenses = new List<ExpenseDTO>();

            // create list with filtering conditions
            List<string> filteringQuery = new List<string>();

            // if no parameters, create default objects
            if (paging == null) paging = new Paging();
            if (sorting == null) sorting = new Sorting();
            //if (filtering == null) filtering = new Filtering();


            //---------------------------------------------------------------------------
            // UBACI VARIJABLE KAO COMMAND PARAMETRE !!!
            //---------------------------------------------------------------------------


            if (filtering != null)
            {
                if (filtering.PersonId != default)
                {
                    //string personId = await FindIdByName("Person", "PersonId", filtering.PersonId.ToString());
                    filteringQuery.Add($"PersonId = '{filtering.PersonId}'");
                }
                if (filtering.CategoryId != default)
                {
                    //string categoryId = await FindIdByName("Category", "CategoryId", filtering.CategoryId.ToString());
                    filteringQuery.Add($"CategoryId = '{filtering.CategoryId}'");
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
            };

            // set sorting order
            string sortingOrder = "ASC";
            if (sorting.SortOrderAsc == false) sortingOrder = "DESC";

            // create query string
            sb.Append("SELECT Expense.Id, ");
            sb.Append("PersonId, ");
            sb.Append("Person.Username AS 'PersonUsername', ");
            sb.Append("Person.Password AS 'PersonPassword', ");
            sb.AppendLine("Person.Email AS 'PersonEmail', ");
            sb.Append("Expense.CategoryId, ");
            sb.Append("Category.Name AS 'CategoryName', ");
            sb.Append("Expense.Name, ");
            sb.Append("Expense.Date, ");
            sb.Append("Expense.Cost, ");
            sb.Append("Expense.Description ");
            sb.AppendLine("FROM EXPENSE ");

            sb.AppendLine("INNER JOIN Person ON Expense.PersonId = Person.Id");
            sb.AppendLine("INNER JOIN Category ON Expense.CategoryId = Category.Id");

            // filtering has parameters? add to query
            if (filteringQuery.Any()) sb.AppendLine("WHERE " + string.Join(" AND ", filteringQuery.ToArray()));

            sb.AppendLine($"ORDER BY [{sorting.OrderBy}] {sortingOrder}");
            sb.AppendLine("OFFSET @offset ROWS");
            sb.AppendLine("FETCH NEXT @pagesize ROWS ONLY");
            sb.AppendLine(";");

            SqlCommand command = new SqlCommand(sb.ToString(), connection);
            command.Parameters.AddWithValue("@offset", (paging.PageNumber - 1) * paging.PageSize);
            command.Parameters.AddWithValue("@pagesize", paging.PageSize);
            using (connection)
            {
                try
                {
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



        public async Task<ExpenseDTO> GetByIdAsync(string id)
        {
            ExpenseDTO expense = await GetExpenseItemByIdAsync(id);
            if (expense == null)
            {
                return null;
            }
            return expense;
        }


        public async Task<int> PostAsync(ExpenseDTO expenseFromBody)
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


        public async Task<bool> UpdateByIdAsync(string id, ExpenseDTO expenseUpdated)
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
