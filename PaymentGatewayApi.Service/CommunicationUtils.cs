using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace PaymentGatewayApi.Service
{
    public static class CommunicationUtils
    {
        
        public static IRestResponse ConnectToBank(string endPoint, Method method, object body = null)
        {
            var client = new RestClient($"{endPoint}");
            client.Timeout = -1;

            var request = new RestRequest(method);
            if(method != Method.GET)
            {
                //request.AddHeader("contentType", "application/json");
                request.AddHeader("Content-Type", "application/json");
                //TODO dodać header z credentialami

                string json = JsonConvert.SerializeObject(body);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }

            return client.Execute(request);
        }
    }
}
