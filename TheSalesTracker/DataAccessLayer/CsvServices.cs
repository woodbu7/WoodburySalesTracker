using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheSalesTracker
{
    public class CsvServices
    {
        #region Fields

        private string _dataFilePath;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public CsvServices(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        #endregion

        #region Methdos

        /// <summary>
        /// Instantiates a Salesperson object, reads data from the Date.csv file into the object,
        /// and returns the object
        /// </summary>
        /// <returns></returns>
        public Salesperson ReadSalespersonFromDataFile()
        {
            // instantiates a new salesperson object
            Salesperson salesperson = new Salesperson();

            string salespersonInfo;
            string[] salespersonInfoArray;
            List<City> citiesTraveled;

            // instantiate a FileStream object for writing
            FileStream rfileStream = File.OpenRead(DataSettings.dataFilePathCsv);

            // wrap the FileStream object in a using statement to ensure of the dispoese using (fileStream)
            using (rfileStream)
            {
                // wrap the FileStream object in a StreamWriter object to simplify writing strings
                StreamReader sReader = new StreamReader(rfileStream);

                using (sReader)
                {
                    salespersonInfo = sReader.ReadLine();
                    //citiesTraveled = sReader.ReadLine();
                }
            }

            // convert and write data to salesperson object
            salespersonInfoArray = salespersonInfo.Split(',');
            salesperson.FirstName = salespersonInfoArray[0];
            salesperson.LastName = salespersonInfoArray[1];
            salesperson.AccountID = salespersonInfoArray[2];

            // validate product type, set to "None" if !Enum
            if (!Enum.TryParse<Product.ProductType>(salespersonInfoArray[3], out Product.ProductType productType))
            {
                productType = Product.ProductType.None;
            }
            salesperson.CurrentStock.Type = productType;

            salesperson.CurrentStock.AddProducts(Convert.ToInt32(salespersonInfoArray[4]));
            salesperson.CurrentStock.OnBackorder = Convert.ToBoolean(salespersonInfoArray[5]);

            //salesperson.CitiesVisited = citiesTraveled.Split(',').ToList();

            return salesperson;
        }

        /// <summary>
        /// takes a Salesperson object and writes it to the Date.csv file
        /// </summary>
        /// <param name="salesperson"></param>
        public void WriteSalespersonToDataFile(Salesperson salesperson)
        {
            string salespersonData;
            char delineator = ',';

            StringBuilder sb = new StringBuilder();

            // add salesperson and product info to string
            sb.Clear();
            sb.Append(salesperson.FirstName + delineator);
            sb.Append(salesperson.LastName + delineator);
            sb.Append(salesperson.AccountID + delineator);
            sb.Append(salesperson.CurrentStock.Type.ToString() + delineator);
            sb.Append(salesperson.CurrentStock.NumberOfUnits.ToString() + delineator);
            sb.Append(salesperson.CurrentStock.OnBackorder.ToString() + delineator);
            sb.Append(Environment.NewLine);

            // add cities traveled to the string
            //foreach (string city in salesperson.CitiesVisited)
            //{
            //    sb.Append(city + delineator);
            //}

            // remove the last delineator
            if (sb.Length != 0)
            {
                sb.Length--;
            }

            // convert StringBuilder object to a string
            salespersonData = sb.ToString();

            // initilaize a FileStream object for writing
            FileStream wfileStream = File.OpenWrite(DataSettings.dataFilePathCsv);

            // wrap the FileStream object in a using statement to ensure of the dispose using (wfileStream)
            using (wfileStream)
            {
                // wrap the FileStream object in a StreamWriter object to simplify writing strings
                StreamWriter sWriter = new StreamWriter(wfileStream);

                using (sWriter)
                {
                    sWriter.Write(salespersonData);
                }
            }

        }

        #endregion
    }
}
