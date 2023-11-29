using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace DOTNET_DEMO.Produit
{
    public class DeleteModel : PageModel
    {
        public string messageErreur="";
        public Produit produit=new Produit();
        public void OnGet()
        {
            String _id = Request.Query["id"];
            try
            {
                //connection string
                string connectionString = "Data Source=.;Initial Catalog=TestDemo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Création de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "DELETE FROM produit WHERE id=@id;"; //Déclaration de la requête

                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Exécuter la requête
                    {
                        cmd.Parameters.AddWithValue("@id", _id); //Avoir le produit avec l'id passé par la page Index
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            Response.Redirect("/Produit/Index");
        }
        public void OnPost()
        {

        }
    }
}
