using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Budget.Models;
using System.Diagnostics;

namespace Budget.Repository
{
    public class ExpenseRepository
    {

        private string connectionString = "Data Source=DESKTOP-D467OFD\\MOJSQLSERVER;Initial Catalog=Budget;Integrated Security=True";


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

        public List<Expense> GetExpenses()
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
                    SqlDataReader reader = command.ExecuteReader();
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



    }
}
