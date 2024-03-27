using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Migrations.Model.UpdateDatabaseOperation;
using System.Windows.Controls;

using System.Windows;

namespace TestTaskWPF.Model
{


    class MyDbContextIni : CreateDatabaseIfNotExists<DB>// CreateDatabaseIfNotExists<DB> DropCreateDatabaseIfModelChanges
    {



        protected override void Seed(DB _db)
        {

            string CS = @"Data Source=DESKTOP-BHIETPU;Initial Catalog=WPFTest;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            MessageBox.Show(CS.ToString());


            var resourceNameGRTXT = "TestTaskWPF.Car.txt";
            var asm = Assembly.GetExecutingAssembly();
            using (Stream streami = asm.GetManifestResourceStream(resourceNameGRTXT))
            using (StreamReader sri = new StreamReader(streami))
            //   using (StreamReader sr = new StreamReader(File.Open("InstallPlacesTXT.txt", FileMode.Open)))
            {
                using (SqlConnection txtbaglan = new SqlConnection(CS))
                {
                    txtbaglan.Open();
                    string line = "";

                    while ((line = sri.ReadLine()) != null)
                    {

                        string[] parts = line.Split(new string[] { "," }, StringSplitOptions.None);

                        string cmdTxt = String.Format("INSERT INTO [dbo].[Car] ( [Name],[NumCar],[WTara],) VALUES ('{0}','{1}','{2}')", parts[0], parts[1], parts[2]);
                        Console.WriteLine(cmdTxt);

                        using (SqlCommand cmddd = new SqlCommand(cmdTxt, txtbaglan))
                        {
                            cmddd.ExecuteNonQuery();
                        }

                    }
                }
            }


     
            var resourceNameInstall = "TestTaskWPF.WeightLists.txt";

            using (Stream streami = asm.GetManifestResourceStream(resourceNameInstall))
            using (StreamReader sri = new StreamReader(streami))
            //   using (StreamReader sr = new StreamReader(File.Open("InstallPlacesTXT.txt", FileMode.Open)))
            {
                using (SqlConnection txtbaglan = new SqlConnection(CS))
                {
                    txtbaglan.Open();
                    string line = "";

                    while ((line = sri.ReadLine()) != null)
                    {

                        string[] parts = line.Split(new string[] { "," }, StringSplitOptions.None);

                        string cmdTxt = String.Format("INSERT INTO[dbo].[WeightLists] ( [NumCar], [WNetto],[WBrutto],[WTara],[DTTara],[DTBrutto],[Car_Id] ) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
                        Console.WriteLine(cmdTxt);

                        using (SqlCommand cmddd = new SqlCommand(cmdTxt, txtbaglan))
                        {
                            cmddd.ExecuteNonQuery();
                        }

                    }
                }
            }



        }
    }



    public class DB : DbContext
    {


        static DB()
        {
            Database.SetInitializer<DB>(new MyDbContextIni());

        }


        public DB() : base(@"Data Source=DESKTOP-BHIETPU;Initial Catalog=WPFTest;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")


        { Database.SetInitializer(new MigrateDatabaseToLatestVersion<DB, Migrations.Configuration>()); }

        //public DbSet<Car> Cars { get; set; }
        //public DbSet<WeightList> WeightLists { get; set; }







    }
}

