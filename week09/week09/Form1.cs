using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week09.Entities;

namespace week09
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirths(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeaths(@"C:\Temp\halál.csv");

            for (int year = 2005; year <= 2024; year++)
            {
                //var kezdoEv = 2005;
                foreach (var item in Population)
                {

                }

                int nbrOfMales = (from x in Population
                           where x.Gender == Gender.Male && x.IsAlive
                           select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }

        }
        public List<Person> GetPopulation (string path)
        {
            List<Person> population = new List<Person>();

            using(StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person(){
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }
            return population;
        }
        public List<BirthProbability> GetBirths(string path)
        {
            List<BirthProbability> births = new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    births.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        Childs = int.Parse(line[1]),
                        Probability = double.Parse(line[2])
                    });
                }
            }
            return births;
        }
        public List<DeathProbability> GetDeaths(string path)
        {
            List<DeathProbability> deaths = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deaths.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        Probability = double.Parse(line[2])
                    });
                }
            }
            return deaths;
        }
    }
}
