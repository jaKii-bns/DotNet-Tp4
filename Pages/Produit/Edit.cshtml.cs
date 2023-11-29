using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace DOTNET_DEMO.Produit
{
    public class EditModel : PageModel
    {
        public Produit produit = new Produit();
        public string messageErreur = "";

        public void OnGet()
        {
            String _id = Request.Query["id"];
            try
            {
                //connection string
                string connectionString = "Data Source=.;Initial Catalog=TestDemo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "SELECT * FROM produit WHERE id=@id;"; //D�claration de la requ�te

                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Ex�cuter la requ�te
                    {
                        cmd.Parameters.AddWithValue("@id", _id); //Avoir le produit avec l'id pass� par la page Index
                        using (SqlDataReader reader = cmd.ExecuteReader()) //Obtenir le reader SQL apr�s ex�cution
                        {
                            if (reader.Read()) //Lire les donn�es (non vides)
                            {
                                produit._id = reader.GetInt32(0);
                                produit._nom = reader.GetString(1);
                                produit._description = reader.GetString(2);
                                produit._prix = reader.GetInt32(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
        public void OnPost()
        {
            produit._id = Convert.ToInt32(Request.Form["id"]);
            produit._nom = Request.Form["nom"];
            produit._description = Request.Form["description"];
            produit._prix = Convert.ToInt32(Request.Form["prix"]);

            if (produit._nom == "" || produit._prix == 0)
            {
                messageErreur = "Veuillez saisir le nom et le prix du produit";
                return;
            }

            try
            {
                //connection string
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "UPDATE produit SET nom=@nom,description=@description, prix=@prix WHERE id=@id;"; //D�claration de la requ�te

                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //pr�parer la requ�te
                    {
                        Console.WriteLine("Modifier le produit n: "+Convert.ToString(produit._id));
                        cmd.Parameters.AddWithValue("@id", Convert.ToString(produit._id));
                        cmd.Parameters.AddWithValue("@nom", produit._nom);
                        cmd.Parameters.AddWithValue("@description", produit._description);
                        cmd.Parameters.AddWithValue("@prix", Convert.ToString(produit._prix));
                        Console.WriteLine("preparer la requ�tes");

                        cmd.ExecuteNonQuery(); //Ex�cuter la requ�te
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            Response.Redirect("/Produit/Index");

        }
    }
}
