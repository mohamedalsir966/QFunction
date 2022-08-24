using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyShift.Notifications.Entitys;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyShift.Notifications.Service
{
    public class ServiceEng : IService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogsRepository _LogsRepository;
        public ServiceEng(IConfiguration configuration, ILogsRepository logsRepository)
        {
            _configuration = configuration;
            _LogsRepository = logsRepository;
        }
        public async Task<string> GetDataToNotifiy()
        {
            var dataTobeNotifyed = await _LogsRepository.GetLogsQueries();
            //send the notification.

            var updateData= await _LogsRepository.UpdateLogsCommand(dataTobeNotifyed);


            var queueMessage = JsonConvert.SerializeObject(dataTobeNotifyed);

            return queueMessage;
        }
      
    }
}
