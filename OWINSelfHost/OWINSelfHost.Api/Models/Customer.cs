using System;
using System.Collections.Generic;

namespace OWINSelfHost.Api.Models
{
    public class Customer
    {
        public Int32 Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public List<String> Groups  { get; set; }
    }
}