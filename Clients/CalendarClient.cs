using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using lectionapi.Model;
using Newtonsoft.Json;

namespace lectionapi.Clients
{
    public class CalendarClient
    {
        private HttpClient _client;
        private static string _address;
        public static string _access_token;


        public CalendarClient()
        {
            _address = Constants.adress;
            _access_token = Constants.access_token;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);    
            
        }

        public async Task<CalendarEvents> GetEvents(string Start, string End )
        {
            //DateTime Start = DateTime.ParseExact(StartTime, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime End = DateTime.ParseExact(EndTime, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://www.googleapis.com/calendar/v3/calendars/primary/events?timeMin={Start}&timeMax={End}"),
                Method = HttpMethod.Get,
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", Constants.access_token);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(content);
            CalendarEvents result = JsonConvert.DeserializeObject<CalendarEvents>(content);
            //Console.WriteLine("deser " + result.Items.Count);
            return result;

        }

        public async Task<QuickAdd> QuickAddEvent(string EventName)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://www.googleapis.com/calendar/v3/calendars/primary/events/quickAdd?text={EventName}"),
                Method = HttpMethod.Post,
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", Constants.access_token);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(content);
            QuickAdd result = JsonConvert.DeserializeObject<QuickAdd>(content);
            return result;
        }

        public async Task DeleteEvent(string EventId)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://www.googleapis.com/calendar/v3/calendars/primary/events/{EventId}"),
                Method = HttpMethod.Delete,
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", Constants.access_token);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

        public async Task<InsertEvent> AddEvent(InsertEventParameters parameters)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://www.googleapis.com/calendar/v3/calendars/primary/events"),
                Method = HttpMethod.Post,
                Content = new StringContent($"{{\"end\": {{\"dateTime\": \"{parameters.EndTime}\"}}," +
                $"\"start\": {{\"dateTime\": \"{parameters.StartTime}\"}}," +
                $"\"summary\": \"{parameters.Summary}\"," +
                $"\"description\": \"{parameters.Description}\"}}", Encoding.UTF8, "application/json"),
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", Constants.access_token);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            InsertEvent result = JsonConvert.DeserializeObject<InsertEvent>(content);
            return result;
        }
        public async Task<UpdateEvent> UpdateEvent(UpdateEventParameters parameters)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://www.googleapis.com/calendar/v3/calendars/primary/events/{parameters.EventId}"),
                Method = HttpMethod.Put,
                Content = new StringContent($"{{\"end\": {{\"dateTime\": \"{parameters.EndTime}\"}}," +
                $"\"start\": {{\"dateTime\": \"{parameters.StartTime}\"}}}}", Encoding.UTF8, "application/json"),
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", Constants.access_token);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            UpdateEvent result = JsonConvert.DeserializeObject<UpdateEvent>(content);
            return result;
        }

    }
}
