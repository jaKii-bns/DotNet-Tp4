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
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "DELETE FROM produit WHERE id=@id;"; //D�claration de la requ�te

                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Ex�cuter la requ�te
                    {
                        cmd.Parameters.AddWithValue("@id", _id); //Avoir le produit avec l'id pass� par la page Index
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
