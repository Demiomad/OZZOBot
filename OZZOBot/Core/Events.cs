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

            var mentionsBot = e.MentionedUsers.Contains(client.CurrentUser)
                || e.Message.ReferencedMessage is not null;

            if (mentionsBot && !e.Message.Content.StartsWith("ozzo!"))
                await e.Message.RespondAsync(Utils.Responses[Random.Shared.Next(Utils.Responses.Length)]);

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
