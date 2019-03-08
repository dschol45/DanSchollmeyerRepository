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
    public class ProductListRepo : IProductRepo
    {
        private readonly string FILENAME = "Products.txt";
        
        /// <summary>
        /// Load from a text file into the character list
        /// </summary>
        public List<Product> Load()
        {
            List<Product> results = new List<Product>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(FILENAME);
                sr.ReadLine();

                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Product p = ProductMapper.ProductFromString(row);
                    results.Add(p);
                }
            }
            catch (FileNotFoundException fileNotFound)
            {
                Console.WriteLine(fileNotFound.FileName + " was not found");
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return results;
        }
    }
}
