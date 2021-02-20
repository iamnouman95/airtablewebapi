using AirTableWebApi;
using AirTableWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AirTableApi.Controllers
{
    public class MessageController : ControllerBase
    {
        readonly string baseId = "appD1b1YjWoXkUJwR";
        readonly string apiKey = "key46INqjpp7lMzjd";
        readonly string tableName = "Messages";
        private const string AIRTABLE_API_URL = "https://api.airtable.com/v0/";

        [HttpGet]
        public List<GetFieldsModel> GetMessage()
        {
            try
            {
                GetResultRecordList res = null;
                string uriStr = AIRTABLE_API_URL + baseId + "/";
                List<GetFieldsModel> fList = new List<GetFieldsModel>();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    //Get call to api
                    var responseTask = client.GetAsync(uriStr + "Messages");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var response = readTask.Result;
                        res = JsonConvert.DeserializeObject<GetResultRecordList>(response);

                        foreach (var item in res.records)
                        {
                            GetFieldsModel fields = new GetFieldsModel();
                            fields.id = item.id;
                            fields.title = item.fields.Summary;
                            fields.Text = item.fields.Message;
                            fList.Add(fields);
                        }
                    }

                    return fList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ResultRecordList CreateMessage([FromBody] List<MessageModel> model)
        {
            try
            {
                ResultRecordList res = null;
                List<Records> records = null;
                string uriStr = AIRTABLE_API_URL + baseId + "/";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uriStr);
                    records = new List<Records>();

                    foreach (var item in model)
                    {
                        Fields fModel = new Fields();
                        fModel.id = "1";
                        fModel.Summary = item.title;
                        fModel.Message = item.text;
                        fModel.receivedAt = DateTime.Now;
                        records.Add(new Records { fields = fModel });
                    }

                    var recordList = new RecordsList();
                    recordList.records = records;

                    //Setting headers
                    var stringContent = new StringContent(JsonConvert.SerializeObject(recordList), System.Text.Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    //Post call to web api
                    var responseTask = client.PostAsync(tableName, stringContent);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        var response = readTask.Result;
                        res = JsonConvert.DeserializeObject<ResultRecordList>(response);
                    }
                }

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
