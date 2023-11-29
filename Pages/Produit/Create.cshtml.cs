using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DOTNET_DEMO.Produit
{
    public class CreateModel : PageModel
    {
        public Produit produit=new Produit();
        public string messageErreur = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            produit._nom = Request.Form["nom"];
            produit._description = Request.Form["description"];
            produit._prix = Convert.ToInt32(Request.Form["prix"]);

            if(produit._nom == ""|| produit._prix ==0)
            {
                messageErreur = "Veuillez saisir le nom et le prix du produit";
                return;
            }

            //Sauvegarder les données dans la base de données
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=TestDemo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Création de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "INSERT INTO produit(nom,description,prix)" +
                        "values(@nom,@description,@prix);";//Déclaration de la requête
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //préparer la requête
                    {
                        cmd.Parameters.AddWithValue("@nom", produit._nom);
                        cmd.Parameters.AddWithValue("@description", produit._description);
                        cmd.Parameters.AddWithValue("@prix", Convert.ToString(produit._prix));
                        cmd.ExecuteNonQuery(); //Exécuter la requête
                    }
                }             
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            //Reinitialiser les données
            produit._nom = "";
            produit._description = "";
            produit._prix = 0;

            Response.Redirect("/Produit/Index");
        }

    }
}
