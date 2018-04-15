using MESCloud.WMS.ProduceData.Reels.Dto;
//using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MESCloud.CommonDto
{
    public class NotificationService
    {

        //HubConnection hubConnection { get; set; }

        HttpHelp HttpHelp;

        string baseUrl;

        public NotificationService(IConfiguration configuration, HttpHelp httpHelp)
        {
            baseUrl = configuration["App:HubService"];

            HttpHelp = httpHelp;

            //hubConnection = new HubConnectionBuilder()
            //    .WithUrl(baseUrl + "/HubService")
            //    .WithConsoleLogger(LogLevel.Trace)
            //    .Build();


            //hubConnection.StartAsync();
        }


        public async Task Notification(string methondName, object data)
        {
            await Task.Factory.StartNew(() =>
            {

                HttpHelp.Post<object>(baseUrl + "/api/Notification/Send", new NotificationDto() { Name = methondName, Data = data });

            });
            //await hubConnection.SendAsync(methondName, data);
        }

        //public async Task Notification(string methondName, object data)
        //{
        //    await hubConnection.SendAsync(methondName, data);
        //}
    }

    public class NotificationDto
    {

        public string Name { get; set; }

        public object Data { get; set; }

    }
}
