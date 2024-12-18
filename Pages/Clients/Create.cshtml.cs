using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace snowboardstore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.adress = Request.Form["address"];

            if (string.IsNullOrEmpty(clientInfo.name) ||
                string.IsNullOrEmpty(clientInfo.email) ||
                string.IsNullOrEmpty(clientInfo.phone) ||
                string.IsNullOrEmpty(clientInfo.adress))
            {
                errorMessage = "All fields are required.";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-S2C4DSQ;Initial Catalog=mystore;Integrated Security=True;Encrypt=False;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients (name, email, phone, adress) VALUES (@name, @email, @phone, @adress);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@adress", clientInfo.adress);

                        command.ExecuteNonQuery();
                    }
                }

                clientInfo = new ClientInfo();
                successMessage = "New client added successfully.";
                Response.Redirect("/Clients/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
