using DSharpPlus.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Net.Gateway;
using DSharpPlus.Entities;

namespace OZZOBot.Core.Commands
{
    public class UtilityCommands
    {
        [Command("ping"), Description("pong!")]
        public async ValueTask PingCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("pong!");
        }

        [Command("help"), Description("displays command info")]
        public async ValueTask HelpCommand(CommandContext ctx)
        {
            var sb = new StringBuilder();

            foreach (var cmd in ctx.Extension.Commands.Values)
            {
                sb.AppendLine($"`ozzo!{cmd.Name}` - {cmd.Description}");
            }

            var embed = new DiscordEmbedBuilder()
            {
                Title = "bot help",
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = Utils.Subtexts[Random.Shared.Next(Utils.Subtexts.Length)]
                },
                Color = DiscordColor.Blurple,
                Description = sb.ToString()
            };

            await ctx.RespondAsync(embed);
        }
    }
}
