using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace DOTNET_DEMO.Produit
{
    public class IndexModel : PageModel
    {
        //La liste des produit qu'on doit remplir à l'aide de la méthod get()
        public List<Produit> ProduitList = new List<Produit>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";
                
                using (SqlConnection connection = new SqlConnection(connectionString)) //Création de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "SELECT * FROM produit;";//Déclaration de la requête
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Exécuter la requête
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader()) //Obtenir le reader SQL aprés exécution
                        {
                            while (reader.Read()) //Lire les données (non vides)
                            {
                                Produit produit = new Produit(); //Sauvegarder les données dans un objet
                                produit._id = reader.GetInt32(0); //Ajouter les properiétsé
                                produit._nom=reader.GetString(1);
                                produit._description = reader.GetString(2);
                                produit._prix=reader.GetInt32(3);

                                ProduitList.Add(produit); //Ajouter l'objet à la liste des objets
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ex.ToString());
            }
        }
    }
    public class Produit
    {
        public int _id;
        public string _nom="";
        public string _description="";
        public int _prix=0;
    }
}
