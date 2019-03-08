using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Models.Interfaces;

namespace Flooring.Data
{
    public class TaxListRepo : ITaxRepo
    {
        private readonly string FILENAME = "Taxes.txt";

        /// <summary>
        /// Load from a text file into the character list
        /// </summary>
        public List<Tax> Load()
        {
            List<Tax> results = new List<Tax>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(FILENAME);
                sr.ReadLine();

                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Tax t = TaxMapper.FromString(row);
                    results.Add(t);
                }
            }
            catch (FileNotFoundException fileNotFound)
            {
                Console.WriteLine(fileNotFound.FileName + " was not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return results;
        }
    }
}
