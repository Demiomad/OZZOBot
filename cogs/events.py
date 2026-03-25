import discord, random
from discord.ext import commands

import utils


class Events(commands.Cog):
    def __init__(self, bot: commands.Bot):
        self.bot = bot

    @commands.Cog.listener()
    async def on_ready(self):
        print(f"Logged in as {self.bot.user.name}!")

    @commands.Cog.listener()
    async def on_message(self, message: discord.Message):
        if message.author.bot:
            return

        if (utils.is_valid_reply(message) and message.reference.resolved.author == self.bot.user) and not message.content.startswith(self.bot.command_prefix):
            await message.reply(random.choice(utils.RESPONSES))

        if random.random() < 0.1 and message.channel.id in utils.channels_with_sound:
            await message.add_reaction("🤤")
            await message.channel.send(utils.get_random_sound())

    @commands.Cog.listener()
    async def on_command_error(self, ctx: commands.Context, error: commands.CommandError):
        if isinstance(error, commands.MissingPermissions):
            await ctx.send("you cant do that")
        elif isinstance(error, commands.BotMissingPermissions):
            await ctx.send("i cant do that")
        elif isinstance(error, commands.CommandOnCooldown):
            await ctx.send("this command is on cooldown\n\n"
                           f"maybe wait {error.retry_after:.0f} seconds?")
        elif isinstance(error, commands.MissingRequiredArgument):
            await ctx.send(f"you forgot an argument in `{self.bot.command_prefix}{ctx.command.name}`")
        elif isinstance(error, commands.CommandNotFound):
            return
        else:
            raise error

async def setup(bot: commands.Bot):
    await bot.add_cog(Events(bot))