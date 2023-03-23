using Budget.Models;
using Budget.Service;
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
        //static DummyList baza = new DummyList();

        //private string connectionString = "Data Source=DESKTOP-D467OFD\\MOJSQLSERVER;Initial Catalog=Budget;Integrated Security=True";


        //private Expense PopulateExpenseWithReaderData(SqlDataReader reader)
        //{
        //    Expense expense = new Expense();
        //    expense.Id = reader.GetGuid(0);
        //    expense.PersonId = reader.GetGuid(1);
        //    expense.CategoryId = reader.GetGuid(2);
        //    expense.Name = reader.GetString(3);
        //    expense.Date = reader.GetDateTime(4);
        //    expense.Description = !reader.IsDBNull(5) ? reader.GetString(5) : expense.Description;
        //    expense.Cost = reader.GetDecimal(6);

        //    return expense;
        //}


        /*
        private Expense GetExpenseItemById(string id)
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
                        Request.CreateResponse(HttpStatusCode.NotFound, "no content");
                        return null;
                    }

                    reader.Read();
                    Expense expense = PopulateExpenseWithReaderData(reader);
                    reader.Close();

                    Request.CreateResponse(HttpStatusCode.OK);
                    return expense;
                }
                catch (Exception ex)
                {
                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
                    return null;
                }
            }
        }

        */


        // GET all expenses
        [Route("api/expenses/")]
        [HttpGet]
        public HttpResponseMessage GetExpenses()
        {
            ExpenseService service = new ExpenseService();
            List<Expense> expenses = service.GetExpenses();

            if (expenses == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
            }
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }
    }
    /*

    //GET expense by id
    [Route("api/expense/{id}")]
    [HttpGet]
    public HttpResponseMessage GetExpenseById(string id)
    {
        Expense expense = GetExpenseItemById(id);
        if (expense == null)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, "no content");
        }

        return Request.CreateResponse(HttpStatusCode.OK, expense);
    }



    // GET expense by id ORIGINAL
    //[Route("api/expense/{id}")]
    [HttpGet]
    public HttpResponseMessage GetExpenseById_Original(string id)
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
        if (!ModelState.IsValid)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, "missing required data");
        }

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
                if (command.ExecuteNonQuery() > 0)
                {
                    command.Connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "insert into database failed.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }
    }


    //DELETE
    [HttpDelete]
    [Route("api/expense/{id}")]
    public HttpResponseMessage DeleteById(string id)
    {
        //if it does not exists ...
        if (GetExpenseById(id).StatusCode != HttpStatusCode.OK)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, "no item to delete");
        }
        //otherwise

        SqlConnection connection = new SqlConnection(connectionString);

        using (connection)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Expense] WHERE [Id] = @id;", connection);
                command.Parameters.AddWithValue("@Id", id);


                command.Connection.Open();

                if (command.ExecuteNonQuery() > 0)
                {
                    command.Connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "deletion successful");
                }
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "deletion failed.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }


    }



    //PUT
    [HttpPut]
    [Route("api/expense/{id}")]
    public HttpResponseMessage Update(string id, Expense expenseFromBody)
    {
        Expense expense = GetExpenseItemById(id);
        //if it does not exists ...
        //if (GetExpenseById(id).StatusCode != HttpStatusCode.OK)
        if (expense == null)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, "no item to update");
        }
        //otherwise

        SqlConnection connection = new SqlConnection(connectionString);

        using (connection)
        {
            try
            {
                //if expense with id exist ..
                SqlCommand command = new SqlCommand
                    ("UPDATE [Expense] SET [Name] = @name, [PersonId] = @personId, [CategoryId] = @categoryId, [Cost] = @cost, [Date] = @date WHERE [Id] = @id;", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@name", expenseFromBody.Name == default ? expense.Name : expenseFromBody.Name);
                command.Parameters.AddWithValue("@cost", expenseFromBody.Cost == default ? expense.Cost : expenseFromBody.Cost);
                command.Parameters.AddWithValue("@date", expenseFromBody.Date == default ? expense.Date : expenseFromBody.Date);
                command.Parameters.AddWithValue("@personId", expenseFromBody.PersonId == default ? expense.PersonId : expenseFromBody.PersonId);
                command.Parameters.AddWithValue("@categoryId", expenseFromBody.CategoryId == default ? expense.CategoryId : expenseFromBody.CategoryId);

                command.Connection.Open();

                if (command.ExecuteNonQuery() > 0)
                {
                    command.Connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, "update successful");
                }
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "update failed.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }


    }

    */

    //ISPOD JE SVE STARO ŠTO RADI S STATIC LISTOM


    /*


    // GET all categories
    [Route("api/expenses/categories")]
    [HttpGet]
    public HttpResponseMessage GetCategories()
    {
        return Request.CreateResponse(HttpStatusCode.OK, baza.Kategorije);
    }

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


    /*
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



    */





}