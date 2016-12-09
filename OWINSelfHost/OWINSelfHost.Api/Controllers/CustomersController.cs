using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OWINSelfHost.Api.Models;

namespace OWINSelfHost.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/customers")]
    public class CustomersController : ApiController
    {
        static CustomersController()
        {
            customers.Add(new Customer
                          {
                              Id = 1, 
                              FirstName = "Sebastian", 
                              LastName = "Lux",
                              Groups = new List<string>{"Admin", "Remote"}
                          });
        }
        
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// http://.../api/v1/customers
        /// </example>
        [Route(Name = "customers")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        /// <summary>
        /// Get by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>
        /// http://.../api/v1/customers/1
        /// </example>
        [Route("{id}", Name = "GetCustomerById")]
        public HttpResponseMessage Get(int id)
        {
            var customerById = customers.FirstOrDefault(customer => customer.Id == id);
            if (customerById == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, customerById);
        }

        /// <summary>
        /// Get all, filtered by group.
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// http://.../api/v1/customers?group=Admin
        /// </example>
        [Route]
        public HttpResponseMessage Get(string group)
        {
            var customersByGroup = customers.Where(customer => customer.Groups != null && customer.Groups.Contains(group));

            if (customersByGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, customersByGroup);
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// <example>
        /// http://.../api/v1/customers
        /// </example>
        [Route(Name = "CreateCustomer")]
        public HttpResponseMessage Post([FromBody]Customer customer)
        {
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            customers.Add(customer);

            var response = Request.CreateResponse(HttpStatusCode.OK);

            var uri = Url.Link("GetCustomerById", new { id = customer.Id });
            response.Headers.Location = new Uri(uri);

            return response;
        }

        /// <summary>
        /// Update customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <example>
        /// http://.../api/v1/customers/1
        /// </example>
        [Route("{id}")]
        public void Put(int id, [FromBody]Customer customer)
        {
            var customerToUpdate = customers.FirstOrDefault(item => item.Id == id);

            if (customerToUpdate == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            customerToUpdate.FirstName = customer.FirstName ?? customerToUpdate.FirstName;
            customerToUpdate.LastName = customer.LastName ?? customerToUpdate.LastName;
            customerToUpdate.Groups = customer.Groups ?? customerToUpdate.Groups;
        }

        /// <summary>
        /// Delete customer.
        /// </summary>
        /// <param name="id"></param>
        /// <example>
        /// http://.../api/v1/customers/1
        /// </example>
        [Route("{id}")]
        public void Delete(int id)
        {
            var customerToDelete = customers.FirstOrDefault(item => item.Id == id);

            if (customerToDelete == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customers.Remove(customerToDelete);
        }

        private static readonly List<Customer> customers = new List<Customer>();
    }
}