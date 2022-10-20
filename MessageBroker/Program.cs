using StackExchange.Redis;
using System;
using MessageBroker.Listener;
using System.Threading;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace MessageBroker
{
    class Program
    {
        static ReportListener listener = new ReportListener();
        async static Task Main(string[] args)
        {
                listener.Listen();
        }
    }
}
