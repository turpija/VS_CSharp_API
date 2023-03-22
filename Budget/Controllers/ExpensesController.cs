using Budget.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using WebGrease.Css.Extensions;

namespace Budget.Controllers
{
    public class ExpensesController : ApiController
    {
        static DummyList baza = new DummyList();

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

            return expense;
        }


        // GET all expenses
        [Route("api/expense/")]
        [HttpGet]
        public HttpResponseMessage GetExpenses()
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
                        return Request.CreateResponse(HttpStatusCode.NotFound, "no content");

                    }
                    while (reader.Read())
                    {
                        expenses.Add(PopulateExpenseWithReaderData(reader));
                    }
                    reader.Close();
                    command.Connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, expenses);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
                }


            }
        }


        // GET expense by id
        [Route("api/expense/{id}")]
        [HttpGet]

        public HttpResponseMessage GetExpenseById(string id)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM [Expense] WHERE [Id] = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        command.Connection.Close();
                        return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
                    }

                    reader.Read();
                    Expense expense = PopulateExpenseWithReaderData(reader);
                    reader.Close();
                    
                    return Request.CreateResponse(HttpStatusCode.OK, expense);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
                }
            }
        }



        // POST expense 
        [Route("api/expense/")]
        [HttpPost]

        public HttpResponseMessage PostExpense(Expense expenseFromBody)
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

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex )
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
                }
            }
        }



            // GET all categories
            [Route("api/expenses/categories")]
        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            return Request.CreateResponse(HttpStatusCode.OK, baza.Kategorije);
        }

        /*
         * GET
        sqlconnection -> connection string
        using sqlconnection{
            sql command ("select ...", connection)
            command.parameters.addwithvalue("@id",id);
            connection.open
            sqlreader.execute
        if (reader.hasRows)
            while(reader.read()) {
            new person
            person.name = reader.getstring(1);    
            add person
            }
            connection.close
        }

        POST
        number of affected rows = command.executenonquery

        */



        //GET all expenses for person
        [Route("api/expenses/{personName}")]
        [HttpGet]
        public HttpResponseMessage GetForPerson(string personName)
        {

            Person osoba = baza.Persons.Find(item => item.Name == personName);
            if (osoba == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "nema tako nekog");
            }
            List<Expense> allItems = baza.Expenses.Where(item => item.Person.Name == personName).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, allItems);
        }

        // POST add new category
        [Route("api/expenses/category")]
        [HttpPost]
        public HttpResponseMessage Post(Category categoryFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }
            Category categoryToAdd = baza.Kategorije.Find(item => item.Id == categoryFromBody.Id);
            if (categoryToAdd != null)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, $"category with id:{categoryFromBody.Id} already exists");
            }
            baza.Kategorije.Add(categoryFromBody);
            return Request.CreateResponse(HttpStatusCode.Accepted, categoryFromBody);
        }

        // PUT update category
        [Route("api/expenses/category")]
        [HttpPut]
        public HttpResponseMessage Put(Category categoryFromBody)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
            }
            Category categoryToUpdate = baza.Kategorije.Find(item => item.Id == categoryFromBody.Id);
            if (categoryToUpdate == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"category with id: {categoryFromBody.Id} does not exists");
            }

            categoryToUpdate.Name = string.IsNullOrWhiteSpace(categoryFromBody.Name) ? categoryToUpdate.Name : categoryFromBody.Name;
            return Request.CreateResponse(HttpStatusCode.OK, categoryToUpdate);
        }
        // DELETE category
        [Route("api/expenses/category")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Category categoryToDelete = baza.Kategorije.Find(item => item.Id == id);

            if (categoryToDelete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"category with id:{id} does not exists");
            }
            baza.Kategorije.Remove(categoryToDelete);
            return Request.CreateResponse(HttpStatusCode.OK, $"item with id:{id} deleted");

        }
    }
}