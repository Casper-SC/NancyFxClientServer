using System.IO;
using System.Net;
using Constants;
using Data;
using Newtonsoft.Json;
using Utils;

namespace Client
{
    class MonitoringClient
    {
        private readonly string _baseAddress;

        public MonitoringClient(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public LocationStateContainer GetLocationStates()
        {
            return Get<LocationStateContainer>(ApiPath.LocationStatesV1);
        }

        private void Post<TRequest>(TRequest request, string apiPath, string method)
            where TRequest : class
        {
            string address = Url.Combine(_baseAddress, apiPath, method);
            using (var httpResponse = SendPostRequest(request, address))
            {
            }
        }

        private TResult Post<TRequest, TResult>(TRequest request, string apiPath, string method)
            where TRequest : class
            where TResult : class
        {
            string address = Url.Combine(_baseAddress, apiPath, method);
            using (var httpResponse = SendPostRequest(request, address))
            {
                using (var responseStream = httpResponse.GetResponseStream())
                {
                    return Deserialize<TResult>(responseStream);
                }
            }
        }

        private HttpWebResponse SendPostRequest<TRequest>(TRequest request, string address)
            where TRequest : class
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(address);

            httpRequest.KeepAlive = true;
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.ContentType = "application/json";

            using (var requestStream = httpRequest.GetRequestStream())
            {
                Serialize(requestStream, request);
            }

            try
            {
                return (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(ex);
                }

                throw;
            }
        }

        private TResult Get<TResult>(string apiPath)
            where TResult : class
        {
            string address = Url.Combine(_baseAddress, apiPath);
            using (var httpResponse = SendGetRequest(address))
            {
                using (var responseStream = httpResponse.GetResponseStream())
                {
                    return Deserialize<TResult>(responseStream);
                }
            }
        }

        private HttpWebResponse SendGetRequest(string address)
        {
            var request = (HttpWebRequest)WebRequest.Create(address);
            request.KeepAlive = true;
            request.Method = WebRequestMethods.Http.Get;

            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(ex);
                }

                throw;
            }
        }

        public static void Serialize<T>(Stream stream, T value)
            where T : class
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(stream))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, value);
            }

            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            //serializer.WriteObject(stream, value);
        }

        public static byte[] Serialize<T>(T value)
            where T : class
        {
            byte[] res = null;

            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(ms, value);
                res = ms.ToArray();
            }

            return res;
        }

        public static T Deserialize<T>(Stream stream)
            where T : class
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(reader);
            }
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            //return serializer.ReadObject(stream) as T;
        }
    }
}