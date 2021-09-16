using AW_Lopputyo_Web.Controllers;
using AW_Lopputyo_Web.Models;
using loppuprojekti.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW_Lopputyo_Web
{
    public class AnalyticsApi
    {
        /// <summary>
        /// Gets all the data the endpoint it gets as parameter. Sets susbcription key and deviceId parameters to the headers of the request.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="endpoint"></param>
        /// <param name="subscription"></param>
        /// <returns>List of Datapoints used to draw the chart.</returns>
        public async Task<List<DataPoint>> AllData(string deviceId, string endpoint, string subscription)
        {
            if (deviceId != "NotUser")
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}?device={deviceId}"),
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscription);

                    var response = await client.SendAsync(request);
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var emotions = JsonConvert.DeserializeObject<TimeEmotions>(responseString);
                    List<DataPoint> dataPoints = new List<DataPoint>();
                    if (emotions != null)
                    {
                        dataPoints.Add(new DataPoint("Happiness", emotions.Happiness));
                        dataPoints.Add(new DataPoint("Saddness", emotions.Sadness));
                        dataPoints.Add(new DataPoint("Anger", emotions.Anger));
                        dataPoints.Add(new DataPoint("Surprise", emotions.Surprise));
                        dataPoints.Add(new DataPoint("Neutral", emotions.Neutral));
                        dataPoints.Add(new DataPoint("Disgust", emotions.Disgust));
                        return dataPoints;
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///Gets all the data the endpoint it gets as parameter. Sets susbcription key and deviceId parameters to the headers of the request.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="endpoint"></param>
        /// <param name="subscription"></param>
        /// <returns>List of DataPoints used to draw the chart.</returns>
        public async Task<List<DataPoint>> HappiestDomains(string deviceId, string endpoint, string subscription)
        {
            if (deviceId != "NotUser")
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}?device={deviceId}"),
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscription);

                    var response = await client.SendAsync(request);
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var emotions = JsonConvert.DeserializeObject<List<DomainEmotions>>(responseString).OrderByDescending(e=> e.Happiness).Take(10);

                    List<DataPoint> dataPoints = new List<DataPoint>();
                    foreach (var item in emotions)
                    {
                        dataPoints.Add(new DataPoint(item.Domain, item.Happiness));
                    }
                    return dataPoints;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all the data the endpoint it gets as parameter. Sets susbcription key and deviceId parameters to the headers of the request.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="endpoint"></param>
        /// <param name="subscription"></param>
        /// <returns>List of DataPoints used to draw the chart.</returns>
        public async Task<List<DataPoint>> SaddestDomains(string deviceId, string endpoint, string subscription)
        {
            if (deviceId != "NotUser")
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}?device={deviceId}"),
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscription);

                    var response = await client.SendAsync(request);
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var emotions = JsonConvert.DeserializeObject<List<DomainEmotions>>(responseString).OrderByDescending(e => e.Sadness).Take(10);

                    List<DataPoint> dataPoints = new List<DataPoint>();
                    foreach (var item in emotions)
                    {
                        dataPoints.Add(new DataPoint(item.Domain, item.Sadness));
                    }
                    return dataPoints;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets all the data the endpoint it gets as parameter. Sets susbcription key and deviceId parameters to the headers of the request.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="endpoint"></param>
        /// <param name="subscription"></param>
        /// <returns>List of DataPoints used to draw the chart.</returns>
        public async Task<List<DataPoint>> HappiestHours(string deviceId, string endpoint, string subscription)
        {
            if (deviceId != "NotUser")
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}?device={deviceId}"),
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscription);

                    var response = await client.SendAsync(request);
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var emotions = JsonConvert.DeserializeObject<List<HourlyEmotions>>(responseString).OrderByDescending(e => e.Happiness).Take(10);

                    List<DataPoint> dataPoints = new List<DataPoint>();
                    foreach (var item in emotions)
                    {
                        dataPoints.Add(new DataPoint(item.HourOrWeekdayNumber.ToString(), item.Happiness));
                    }
                    return dataPoints;
                }
            }
            return null;
        }
        /// <summary>
        /// Gets all the data the endpoint it gets as parameter. Sets susbcription key and deviceId parameters to the headers of the request.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="endpoint"></param>
        /// <param name="subscription"></param>
        /// <returns>List of DataPoints used to draw the chart.</returns>
        public async Task<List<DataPoint>> SaddestHours(string deviceId, string endpoint, string subscription)
        {
            if (deviceId != "NotUser")
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}?device={deviceId}"),
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscription);

                    var response = await client.SendAsync(request);
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var emotions = JsonConvert.DeserializeObject<List<HourlyEmotions>>(responseString).OrderByDescending(e => e.Sadness).Take(10);

                    List<DataPoint> dataPoints = new List<DataPoint>();
                    foreach (var item in emotions)
                    {
                        dataPoints.Add(new DataPoint(item.HourOrWeekdayNumber.ToString(), item.Sadness));
                    }
                    return dataPoints;
                }
            }
            return null;
        }

        public async Task<List<DataPoint>> HappiestDays(string deviceId, string endpoint, string subscription)
        {
            if (deviceId != "NotUser")
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"{endpoint}?device={deviceId}"),
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscription);

                    var response = await client.SendAsync(request);
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var emotions = JsonConvert.DeserializeObject<List<HourlyEmotions>>(responseString).OrderByDescending(e => e.Happiness).Take(10);

                    List<DataPoint> dataPoints = new List<DataPoint>();
                    foreach (var item in emotions)
                    {
                        dataPoints.Add(new DataPoint(item.HourOrWeekdayNumber.ToString(), item.Happiness));
                    }
                    return dataPoints;
                }
            }
            return null;
        }
    }
}
