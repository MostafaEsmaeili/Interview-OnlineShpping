using Microsoft.Extensions.Configuration;
using Order.Application.Common.Extensions;
using Order.Application.Common.interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class CatalogMicroService : ICatalogMicroService
    {
        private readonly IConfiguration _configuration;
        const string GetProductAddress = "api/Product/{id}";
        public CatalogMicroService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration= configuration;
        }
        public async Task<bool> IsValidProduct(Guid productId, CancellationToken cancellationToken = default)
        {
          
                var client = new RestClient(_configuration["CatalogMicroServiceAddress"]);
                client.Timeout = -1;
                var request = new RestRequest(GetProductAddress,Method.GET,DataFormat.Json);
                request.AddUrlSegment("id", productId);
               var a = client.BuildUri(request);
                IRestResponse response =await client.ExecuteAsync(request, cancellationToken);
                return response.IsSuccessful && !response.Content.IsNullOrEmpty();
        }
    }
}
