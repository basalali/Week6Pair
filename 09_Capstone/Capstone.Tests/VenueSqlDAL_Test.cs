using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class VenueSqlDAL_Test
    {
        //Test will return a list of all venue names

        private string ConnectionString { get; } = "Data Source=.\\sqlexpress;Initial Catalog=excelsior_venues;Integrated Security=True";
        private int venueID = 0;

        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand cmd;
            connection.Open();
            cmd = new SqlCommand("SELECT name FROM venue", connection);
            
            cmd = new SqlCommand("SELECT * FROM venue " +
            "JOIN city ON city.id = venue.city_id " +
            "JOIN state ON state.abbreviation = city.state_abbreviation " +
            "JOIN category_venue ON category_venue.venue_id = venue.id " +
            "JOIN category ON category.id = category_venue.category_id " +
            "WHERE venue_id = @venue_id ", connection);
            venueID = (int)cmd.ExecuteScalar();
            connection.Close();

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        [TestMethod]
        public void Get_Venue_Name()
        {
            //arrange
            VenueSqlDAL venueSql = new VenueSqlDAL(ConnectionString);
            List<Venue> venuename = venueSql.GetVenueName();

            //assert
            Assert.IsNotNull(venuename);
        }

        [TestMethod]
        public void Get_Venue_Details()
        {
            VenueSqlDAL venueSql = new VenueSqlDAL(ConnectionString);
            //List<Venue> venueDetails = (List<Venue>)venueSql.GetVenueDetails();

            
            //Assert.IsNotNull(venueDetails);
        }

    }

    
}
