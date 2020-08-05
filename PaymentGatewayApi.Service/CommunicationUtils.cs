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
                request.AddHeader("Content-Type", "application/json");

                string json = JsonConvert.SerializeObject(body);
                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }

            return client.Execute(request);
        }
    }
}
