using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace CMPS_285.Hubs
{
    public class Notifications : Hub
    {
        public void Hello()
        {
            Clients.All.hello("Hello Everyone");
        }

        public Notifications()
        {
            //create a long runnig task to do infinite loop which will keep sending the server time in every three seconds
            var TaskTimer = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    string timeNow = DateTime.Now.ToString();
                    //sending the connection time to all connected servers 
                    Clients.All.SendServerTime(timeNow);
                    //Delay By 3 seconds
                    await Task.Delay(3000);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
