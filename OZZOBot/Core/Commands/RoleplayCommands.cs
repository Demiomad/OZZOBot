using DSharpPlus.Commands;
using DSharpPlus.Commands.Trees.Metadata;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OZZOBot.Core.Commands
{
    [Description("do random shi with people")]
    public class RoleplayCommands
    {
        [Command("tickle"), Description("lets you tickle a member")]
        public async ValueTask TickleCommand(CommandContext ctx,
            [Description("the member you wanna tickle")] DiscordMember? member = null)
        {
            if (member is null || member == ctx.User)
            {
                await ctx.EditResponseAsync("at least mention someone man");
                return;
            }

            if (member == ctx.Client.CurrentUser)
            {
                await ctx.EditResponseAsync("i cant tickle myself");
                return;
            }

            using (var stream = Utils.GetStream("OZZOBot.Resources.tickle.gif"))
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{ctx.User.Username} is tickling {member.Username}",
                    ImageUrl = Utils.GetAttachmentUrl("tickle.gif"),
                    Footer = new DiscordEmbedBuilder.EmbedFooter()
                    {
                        Text = Utils.Subtexts[Random.Shared.Next(Utils.Subtexts.Length)]
                    },
                    Color = DiscordColor.Blurple
                };

                var msg = new DiscordMessageBuilder();

                msg.AddFile("tickle.gif", stream)
                    .AddEmbed(embed);

                await ctx.EditResponseAsync(msg);
            }

            try
            {
                await member.SendMessageAsync($"{ctx.User.Mention} is tickling you");
            }
            catch (UnauthorizedException)
            {
                await ctx.FollowupAsync($"{member.Mention} probably has dms disabled");
            }
            catch (Exception ex)
            {
                await ctx.FollowupAsync($":x: {ex.Message}\n```{ex}```");
            }
        }

        [Command("crack"), Description("lets you crack a member"), TextAlias("fuck", "rail")]
        public async ValueTask CrackCommand(CommandContext ctx,
            [Description("the member you wanna crack")] DiscordMember? member = null)
        {
            if (member is null || member == ctx.User)
            {
                await ctx.EditResponseAsync("oh- oh my god you dont have anyone to crack..? damn i feel bad for you");
                return;
            }

            if (member == ctx.Client.CurrentUser)
            {
                await ctx.EditResponseAsync("you cant crack me im a clanker");
                return;
            }

            using (var stream = Utils.GetStream("OZZOBot.Resources.crack.gif"))
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{ctx.User.Username} is cracking {member.Username}",
                    ImageUrl = Utils.GetAttachmentUrl("crack.gif"),
                    Footer = new DiscordEmbedBuilder.EmbedFooter()
                    {
                        Text = Utils.Subtexts[Random.Shared.Next(Utils.Subtexts.Length)]
                    },
                    Color = DiscordColor.Blurple
                };

                var msg = new DiscordMessageBuilder();

                msg.AddFile("crack.gif", stream);
                msg.AddEmbed(embed);

                await ctx.EditResponseAsync(msg);
            }

            try
            {
                await member.SendMessageAsync($"{ctx.User.Mention} is cracking you\n" +
                    Utils.CrackSubtexts[Random.Shared.Next(Utils.CrackSubtexts.Length)]);
            }
            catch (UnauthorizedException)
            {
                await ctx.FollowupAsync($"{member.Mention} probably has dms disabled");
            }
            catch (Exception ex)
            {
                await ctx.FollowupAsync($":x: {ex.Message}\n```{ex}```");
            }
        }

        [Command("hug"), Description("lets you hug a member")]
        public async ValueTask HugCommand(CommandContext ctx,
            [Description("the member you wanna hug")] DiscordMember? member = null)
        {
            if (member is null || member == ctx.User)
            {
                await ctx.EditResponseAsync("you dont have anyone to hug? fine...");
                return;
            }

            if (member == ctx.Client.CurrentUser)
            {
                await ctx.EditResponseAsync("fine...");
                return;
            }

            using (var stream = Utils.GetStream("OZZOBot.Resources.hug.png"))
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{ctx.User.Username} is hugging {member.Username}",
                    ImageUrl = Utils.GetAttachmentUrl("hug.png"),
                    Footer = new DiscordEmbedBuilder.EmbedFooter()
                    {
                        Text = Utils.Subtexts[Random.Shared.Next(Utils.Subtexts.Length)]
                    },
                    Color = DiscordColor.Blurple
                };

                var msg = new DiscordMessageBuilder();

                msg.AddFile("hug.png", stream);
                msg.AddEmbed(embed);

                await ctx.EditResponseAsync(msg);
            }

            try
            {
                await member.SendMessageAsync($"{ctx.User.Mention} is hugging you\naww...");
            }
            catch (UnauthorizedException)
            {
                await ctx.FollowupAsync($"{member.Mention} probably has dms disabled");
            }
            catch (Exception ex)
            {
                await ctx.FollowupAsync($":x: {ex.Message}\n```{ex}```");
            }
        }

        [Command("cuddle"), Description("lets you cuddle with a member")]
        public async ValueTask CuddleCommand(CommandContext ctx,
            [Description("the member you wanna cuddle with")] DiscordMember? member = null)
        {
            if (member is null || member == ctx.User)
            {
                await ctx.EditResponseAsync("you dont have anyone to cuddle with? i dont exist");
                return;
            }

            if (member == ctx.Client.CurrentUser)
            {
                await ctx.EditResponseAsync("im a clanker!!!!! IM NOT REAL!!!");
                return;
            }

            using (var stream = Utils.GetStream("OZZOBot.Resources.cuddle.gif"))
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = $"{ctx.User.Username} is cuddling with {member.Username}",
                    ImageUrl = Utils.GetAttachmentUrl("cuddle.gif"),
                    Footer = new DiscordEmbedBuilder.EmbedFooter()
                    {
                        Text = Utils.Subtexts[Random.Shared.Next(Utils.Subtexts.Length)]
                    },
                    Color = DiscordColor.Blurple
                };

                var msg = new DiscordMessageBuilder();

                msg.AddFile("cuddle.gif", stream);
                msg.AddEmbed(embed);

                await ctx.EditResponseAsync(msg);
            }

            try
            {
                await member.SendMessageAsync($"{ctx.User.Mention} is cuddling with you\ngood night ig");
            }
            catch (UnauthorizedException)
            {
                await ctx.FollowupAsync($"{member.Mention} probably has dms disabled");
            }
            catch (Exception ex)
            {
                await ctx.FollowupAsync($":x: {ex.Message}\n```{ex}```");
            }
        }
    }
}
