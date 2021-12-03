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
        List<int> fiuk = new List<int>();
        List<int> lanyok = new List<int>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Simulation()
        {
            Population = GetPopulation(path.Text);
            BirthProbabilities = GetBirths(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeaths(@"C:\Temp\halál.csv");
            for (var year = 2005; year <= yearBox.Value; year++)
            {
                //var kezdoEv = 2005;
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);

                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
                fiuk.Add(nbrOfFemales);
                lanyok.Add(nbrOfMales);
            }
        }

        void SimStep(int year, Person p)
        {
            if (!p.IsAlive) return;

            int age = year - p.BirthYear;
            var DeathP = (from x in DeathProbabilities
                          where x.Age == age && x.Gender == p.Gender
                          select x.Probability).FirstOrDefault();
            if (rng.NextDouble() <= DeathP)
            {
                p.IsAlive = false;
            }
            if (p.IsAlive != true && p.Gender!=Gender.Female)
            {
                return;
            }
            var BirthP = (from x in BirthProbabilities
                          where x.Age == age //&& x.Childs == p.NbrOfChildren
                          select x.Probability).FirstOrDefault();
            if (rng.NextDouble()<=BirthP)
            {
                var newP = new Person()
                {
                    BirthYear = year,
                    NbrOfChildren = 0,
                    IsAlive = true,
                    Gender = (Gender)(rng.Next(1, 3))
                };
                Population.Add(newP);
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

        private void button1_Click(object sender, EventArgs e)
        {
            fiuk.Clear();
            lanyok.Clear();
            richTextBox1.Clear();
            Simulation();
            DisplayResults();
        }
        private void DisplayResults()
        {
            var counter = 0;
            for (int i = 2005; i < yearBox.Value; i++)
            {
                richTextBox1.AppendText( "Szimulációs év: " + i + Environment.NewLine  + "Fiúk: " + fiuk[counter] + Environment.NewLine + "Lányok: " + lanyok[counter] + Environment.NewLine);
                counter++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.ShowDialog();
            if (open.ShowDialog()==DialogResult.OK)
            {
                path.Text = open.FileName;
            }
        }
    }
}
