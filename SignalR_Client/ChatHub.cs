using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace SignalR_Client
{
    public class ChatHub : IAsyncDisposable
    {


        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
