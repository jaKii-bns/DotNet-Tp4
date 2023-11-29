using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace DOTNET_DEMO.Produit
{
    public class IndexModel : PageModel
    {
        //La liste des produit qu'on doit remplir � l'aide de la m�thod get()
        public List<Produit> ProduitList = new List<Produit>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Demo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";
                
                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "SELECT * FROM produit;";//D�claration de la requ�te
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //Ex�cuter la requ�te
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader()) //Obtenir le reader SQL apr�s ex�cution
                        {
                            while (reader.Read()) //Lire les donn�es (non vides)
                            {
                                Produit produit = new Produit(); //Sauvegarder les donn�es dans un objet
                                produit._id = reader.GetInt32(0); //Ajouter les properi�ts�
                                produit._nom=reader.GetString(1);
                                produit._description = reader.GetString(2);
                                produit._prix=reader.GetInt32(3);

                                ProduitList.Add(produit); //Ajouter l'objet � la liste des objets
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
