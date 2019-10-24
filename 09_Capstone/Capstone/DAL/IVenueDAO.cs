using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public interface IVenueDAO
    {   
        //get all venue names
        IList<Venue> GetVenueName();

        // get single
        //Venue GetVenues(int id);

    }
}
