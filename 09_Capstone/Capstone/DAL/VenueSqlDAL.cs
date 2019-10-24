using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    class VenueSqlDAL
    {

        //get all venues provided a venue_id

        private string connectionString;

        /// <summary>
        /// Creates a new sql-based venue dao.
        /// </summary>
        /// <param name="databaseconnectionString"></param>
        public VenueSqlDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        List<Venue> GetCitiesByVenueID(int venue_Id);

        /// <summary>
        /// Gets all cities provided a country code.
        /// </summary>
        /// <param name="countryCode">The country code to search for.</param>
        /// <returns></returns>
        IList<City> GetCitiesByCountryCode(string countryCode);

        public void GetAllVenues()
        {


        }
    }
}


  



    

