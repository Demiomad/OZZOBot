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
    [Command("sounds"), Description("sound commands")]
    public class SoundCommands
    {
        [Command("disable"), Description("disables sounds")]
        public async ValueTask DisableSoundsCommand(CommandContext ctx,
[Description("the channel you wanna disable sounds on")] DiscordChannel? channel = null)
        {
            if (ctx.User.Id != 1479747763309776971 && !ctx.Member.Permissions.HasPermission(DiscordPermission.Administrator))
            {
                await ctx.RespondAsync("only server admins/bot owner can disable sounds");
                return;
            }

            channel ??= ctx.Channel;

            if (!Utils.ChannelsWithSound.Contains(channel.Id))
            {
                await ctx.RespondAsync("this channel already has sounds disabeld");
                return;
            }

            Utils.ChannelsWithSound.Remove(channel.Id);
            await ctx.RespondAsync($"successfully disabled sounds in {channel.Mention}");
        }

        [Command("enable"), Description("enables sounds")]
        public async ValueTask EnableSoundsCommand(CommandContext ctx,
            [Description("the channel you wanna enable sounds on")] DiscordChannel? channel = null)
        {
            if (ctx.User.Id != 1479747763309776971 && !ctx.Member.Permissions.HasPermission(DiscordPermission.Administrator))
            {
                await ctx.RespondAsync("only server admins/bot owner can enable sounds");
                return;
            }

            channel ??= ctx.Channel;

            if (Utils.ChannelsWithSound.Contains(channel.Id))
            {
                await ctx.RespondAsync("this channel already has sounds enabled");
                return;
            }

            Utils.ChannelsWithSound.Add(channel.Id);
            await ctx.RespondAsync($"successfully enabled sounds in {channel.Mention}");
        }
    }
}
