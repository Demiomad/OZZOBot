using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OZZOBot.Core
{
    public static class Events
    {
        public static async Task MessageCreated(DiscordClient client, MessageCreatedEventArgs e)
        {
            if (e.Author.IsBot) return;

            if (Utils.ChannelsWithSound.Contains(e.Channel.Id) &&
                Random.Shared.NextDouble() < 0.1)
            {
                var msg = new DiscordMessageBuilder()
                    .WithContent(Utils.GetSound(Random.Shared.Next(1, 2)))
                    .WithReply(e.Message.Id);

                await msg.SendAsync(e.Channel);
                await e.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("🤤"));
            }
        }
    }
}
