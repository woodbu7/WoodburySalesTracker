using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSalesTracker
{
    public class InitializeDataFileCsv
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Instantiates a Salesperson object
        /// Adds values to all properties and returns the object
        /// </summary>
        /// <returns></returns>
        private Salesperson InitializeSalesperson()
        {
            Salesperson salesperson = new Salesperson()
            {
                FirstName = "Madeleine",
                LastName = "Woodbury",
                AccountID = "woody123",
                CurrentStock = new Product(Product.ProductType.Treadmill, 50, false),
                CitiesVisited = new List<City>()
                {
                    
                }

            };

            return salesperson;
        }

        /// <summary>
        /// Instantiates a CsvServices object and calls the WriteSalespersonToDataFile method
        ///  with the InitializeSalesperson method as an argument
        /// </summary>
        public void SeedDataFile()
        {
            CsvServices csvService = new CsvServices(DataSettings.dataFilePathCsv);

            csvService.WriteSalespersonToDataFile(InitializeSalesperson());
        }

        #endregion
    }
}
