using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OZZOBot.Core
{
    public class OzzoClient : IHostedService
    {
        private DiscordClient _client;
        private readonly DiscordActivity _activity;

        public OzzoClient(DiscordClient client)
        {
            _client = client;
            _activity = new DiscordActivity("with my dihh >///<", DiscordActivityType.Playing);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.ConnectAsync(_activity);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.DisconnectAsync();
        }
    }
}
