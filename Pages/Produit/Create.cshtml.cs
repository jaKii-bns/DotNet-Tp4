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

            //Sauvegarder les donn�es dans la base de donn�es
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=TestDemo;Integrated Security=True;Column Encryption Setting=enabled; Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString)) //Cr�ation de la connection
                {
                    connection.Open();//Ouvrir La connection
                    String sql = "INSERT INTO produit(nom,description,prix)" +
                        "values(@nom,@description,@prix);";//D�claration de la requ�te
                    using (SqlCommand cmd = new SqlCommand(sql, connection)) //pr�parer la requ�te
                    {
                        cmd.Parameters.AddWithValue("@nom", produit._nom);
                        cmd.Parameters.AddWithValue("@description", produit._description);
                        cmd.Parameters.AddWithValue("@prix", Convert.ToString(produit._prix));
                        cmd.ExecuteNonQuery(); //Ex�cuter la requ�te
                    }
                }             
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            //Reinitialiser les donn�es
            produit._nom = "";
            produit._description = "";
            produit._prix = 0;

            Response.Redirect("/Produit/Index");
        }

    }
}
