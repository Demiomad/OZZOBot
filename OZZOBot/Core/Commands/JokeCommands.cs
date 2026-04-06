using DSharpPlus.Commands;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OZZOBot.Core.Commands
{
    [Description("commands added for the funnies")]
    public class JokeCommands
    {
        [Command("socrates"), Description("do you control the command, or does the command control you?")]
        public async ValueTask SocratesCommand(CommandContext ctx)
        {
            var useAlt = Random.Shared.NextDouble() < 0.5;

            if (!useAlt)
            {
                var activity = Utils.Activities[Random.Shared.Next(Utils.Activities.Length)];
                await ctx.RespondAsync($"if {activity} is your power, what are you without it?");
            }
            else
            {
                var item = Utils.Items[Random.Shared.Next(Utils.Items.Length)];
                await ctx.RespondAsync($"do you control {item}, or does {item} control you?");
            }
        }

        [Command("goon"), Description("the bot goons now!? we're so cooked")]
        public async ValueTask GoonCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("aaahh~!! im gooning!!");

            await Task.Delay(Random.Shared.Next(1000, 15000));

            await ctx.EditResponseAsync($"ah~...\n" +
                $"{Utils.Prefixes[Random.Shared.Next(Utils.Prefixes.Length)]} its all over {Utils.Locations[Random.Shared.Next(Utils.Locations.Length)]}");
        }

        [Command("ozzo"), Description("ozzo")]
        public async ValueTask OzzoCommand(CommandContext ctx)
        {
            if (ctx.User.Id == 1474293589683994655)
                await ctx.RespondAsync("no way its ozzo!! hi ozzo-chan!!");
            else
                await ctx.RespondAsync("ozzo");
        }
    }
}
