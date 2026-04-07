using System;
using DSharpPlus.Entities;
using Microsoft.Extensions.Hosting;
using DSharpPlus;
using DSharpPlus.Extensions;
using OZZOBot.Core.Commands;
using DSharpPlus.Commands;
using Microsoft.Extensions.DependencyInjection;
using OZZOBot.Core;
using DSharpPlus.Commands.Processors.TextCommands;
using DSharpPlus.Commands.Processors.TextCommands.Parsing;

namespace OZZOBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var token = Environment.GetEnvironmentVariable("BOT_TOKEN");

            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    services.AddHostedService<OzzoClient>()
                        .AddDiscordClient(token!, DiscordIntents.All)
                        .AddCommandsExtension((_, ext) =>
                        {
                            ext.AddCommands<JokeCommands>();
                            ext.AddCommands<RoleplayCommands>();
                            ext.AddCommands<UtilityCommands>();
                            ext.AddCommands<SoundCommands>();

                            ext.AddProcessor(new TextCommandProcessor()
                            {
                                Configuration = new TextCommandConfiguration()
                                {
                                    PrefixResolver = new DefaultPrefixResolver(true, "ozzo!").ResolvePrefixAsync,
                                    IgnoreBots = true
                                }
                            });
                        })
                        .ConfigureEventHandlers((builder) =>
                        {
                            builder.HandleMessageCreated(Events.MessageCreated);
                        });
                }).Build();

            await builder.RunAsync();
        }
    }
}