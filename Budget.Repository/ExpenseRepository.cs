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

        // private async Task<Expense> GetExpenseItemByIdAsync(...)
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


<<<<<<< Updated upstream
        public List<Expense> GetAll()
=======


        public async Task<List<Expense>> GetExpensesAsync()
>>>>>>> Stashed changes
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<Expense> expenses = new List<Expense>();

            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Expense;", connection);
                    //if request = empty result
                    //SqlCommand command = new SqlCommand("select * from expense where \"Name\" = 'pero';", connection);

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



        public async Task<Expense> GetExpenseByIdAsync(string id)
        {
            Expense expense = await GetExpenseItemByIdAsync(id);
            if (expense == null)
            {
                return null;
            }
            return expense;
        }


        public async Task<int> PostExpenseAsync(Expense expenseFromBody)
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
                    command.Parameters.AddWithValue("@categoryId", expenseUpdated.CategoryId );

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
