import aiohttp
import discord, utils, asyncio, random
from discord.ext import commands

from utils import feedbacks


class MainCommands(commands.Cog):
    def __init__(self, bot: commands.Bot):
        self.bot = bot

    @commands.command(name="help", description="displays info about commands")
    async def help_cmd(self, ctx: commands.Context,
                       cmd: str = commands.parameter(default=None,
                                                     description="the command you wanna get info for")):
        if random.random() < 0.1:
            await ctx.reply("twin go figure it out yourself")
            return

        if not cmd:
            embed = discord.Embed(
                title="bot help",
                description=f"{len(self.bot.commands)} command(s) available in the bot",
                color=discord.Color.blurple()
            )

            cmds = [f"`{self.bot.command_prefix}{cmd.name}` - {cmd.description}" for cmd in self.bot.commands]
            embed.add_field(
                name="commands",
                value="\n".join(cmds)
            )

            await ctx.send(embed=embed)
        else:
            found_cmd = None

            for _cmd in self.bot.commands:
                if _cmd.name == cmd.lower():
                    found_cmd = _cmd

            if found_cmd is None:
                await ctx.send(f"command `{cmd}` not found\n"
                               f"you sure you didnt misspell it?")
                return

            embed = discord.Embed(
                title=found_cmd.name,
                description=found_cmd.description,
                color=discord.Color.blurple()
            )

            params = []

            for param_name, param in found_cmd.params.items():
                param_type = getattr(param.annotation, "__name__") if param.annotation != param.empty else "any"
                param_desc = param.description or "no description"
                param_default = param.default if param.default != param.empty else "required"

                params.append(
                    f"**{param_name}**:\n"
                    f"description: {param_desc}\n"
                    f"type: {param_type}\n"
                    f"default value: {param_default}\n"
                )

            embed.add_field(
                name="parameters",
                value="\n".join(params) if len(params) > 0 else "no parameters",
                inline=True
            )

            aliases = [f"`{self.bot.command_prefix}{alias}`" for alias in found_cmd.aliases]

            embed.add_field(
                name="aliases",
                value="\n".join(aliases) if len(aliases) > 0 else "no aliases",
                inline=False
            )

            await ctx.send(embed=embed)

    @commands.command(name="crack", description="cracks a member in this server")
    async def crack_cmd(self, ctx: commands.Context,
                        member: discord.Member = commands.parameter(default=None,
                                                                    description="the member you wanna crack")):
        try:
            if member is None:
                if ctx.message.reference:
                    member = ctx.message.reference.resolved.author

            match member:
                case None:
                    await ctx.reply("damn... you dont have anyone to crack? thats kinda sad")

                case ctx.author:
                    await member.send("ejaculation")
                    return

                case self.bot.user:
                    await ctx.reply("you cant crack me im a clanker")
                    return

                case _:
                    await member.send(f"{ctx.author.mention} is cracking you\n"
                                      f"do you like it?")

            await ctx.send(f"you are cracking {member.mention}")
        except Exception as ex:
            await ctx.send(str(ex))

    @commands.command(name="enable_sounds", description="lets the bot make occasional sounds")
    @utils.owner_or_admin()
    async def enable_sounds_cmd(self, ctx: commands.Context,
                                channel: discord.TextChannel = commands.parameter(default=None,
                                                                                  description="the target channel")):
        channel = channel or ctx.channel
        if channel.id in utils.channels_with_sound:
            await ctx.send(f"im already allowed to make sounds in {channel.mention}")
            return

        utils.channels_with_sound.append(channel.id)
        utils.save_data()

        await ctx.send(f"**successfully enabled sounds in {channel.mention}**\n"
                       f"i now have a **10%** chance to say one of these: `{', '.join(utils.SOUNDS)}` with 1-2 tildes\n\n"
                       f"**AND NO THIS DOESNT TRIGGER ON BOT MESSAGES**")

    @commands.command(name="disable_sounds", description="stops the bot from making occasional sounds")
    @utils.owner_or_admin()
    async def disable_sounds_cmd(self, ctx: commands.Context,
                                channel: discord.TextChannel = commands.parameter(default=None,
                                                                                  description="the target channel")):
        channel = channel or ctx.channel
        if channel.id not in utils.channels_with_sound:
            await ctx.send(f"im not allowed to make sounds in {channel.mention}")
            return

        utils.channels_with_sound.remove(channel.id)
        utils.save_data()

        await ctx.send(f"successfully disabled sounds in {channel.mention}")

    @commands.command(name="goon", description="makes the bot goon", aliases=["nut"])
    async def goon_cmd(self, ctx: commands.Context):
        msg = await ctx.send("mmm~ im gooning~!")
        await asyncio.sleep(random.randint(5, 20))
        await msg.edit(content=f"{random.choice(utils.PREFIXES)} its all over {random.choice(utils.ITEMS)}")

        if random.random() > 0.5:
            await ctx.send("c-can you lick it?")

    @commands.command(name="adlib", description="adlib")
    async def adlib_cmd(self, ctx: commands.Context):
        try:
            if not ctx.author.voice:
                await ctx.send(file=discord.File("adlib.wav"))
            else:
                if not ctx.voice_client:
                    await ctx.author.voice.channel.connect()

                channel = ctx.author.voice.channel

                if ctx.voice_client.channel != channel:
                    await ctx.voice_client.move_to(channel)

                future = asyncio.get_event_loop().create_future()
                ctx.voice_client.play(discord.FFmpegPCMAudio("adlib.wav"), after=lambda e: future.set_result(e))

                await future

                await ctx.voice_client.disconnect(force=True)
        except Exception as ex:
            await ctx.send(str(ex))

    @commands.command(name="socrates", description="if commands are your power, what are you without it?")
    async def socrates_cmd(self, ctx: commands.Context):
        await ctx.send(f"if {random.choice(utils.ACTIVITIES)} is your power, what are you without it?")

    @commands.command(name="feedback", description="gives feedback to the developer of this bot")
    @commands.cooldown(1, 60, commands.BucketType.user)
    async def feedback_cmd(self, ctx: commands.Context, *, message: str = commands.parameter(description="the feedback message")):
        try:
            fb_id = ctx.message.id
            async with aiohttp.ClientSession() as s:
                wh = discord.Webhook.from_url(utils.FEEDBACK_WEBHOOK_URL, session=s)

                embed = discord.Embed(
                    title=f"Feedback #{fb_id}",
                    description=message,
                    color=discord.Color.blurple()
                )

                embed.set_author(name=str(ctx.author.name), icon_url=ctx.author.display_avatar.url)
                embed.set_footer(text=f"User ID: {ctx.author.id}")

                await wh.send(embed=embed)

                feedbacks[fb_id] = message

            await ctx.send(f"feedback sent! feedback id: {fb_id}\n"
                           f"-# you can always dm demiomad tho, also the feedback id is your message id")
        except Exception as ex:
            await ctx.send(str(ex))

    @commands.command(name="pat", description="pats a member")
    async def pat_cmd(self, ctx: commands.Context, member: discord.Member = commands.parameter(default=None,
                                                    description="the member you wanna pat")):
        try:
            if member is None:
                if ctx.message.reference:
                    member = ctx.message.reference.resolved.author

            match member:
                case _ if member == ctx.author or member is None:
                    await ctx.send("oh my... you dont have anyone to pat?? dw i feel you")
                    return

                case self.bot.user:
                    await ctx.reply("im a clanker")
                    return

                case _:
                    await member.send(f"{ctx.author.mention} is patting you\n"
                                      f"awh... how cute")

            await ctx.send(f"you are patting {member.mention}")
        except Exception as ex:
            await ctx.send(str(ex))

    @commands.command(name="cuddle", description="lets you cuddle with a member")
    async def cuddle_cmd(self, ctx: commands.Context, member: discord.Member = commands.parameter(default=None,
                                                                                               description="the member you wanna cuddle with")):
        try:
            if member is None:
                if ctx.message.reference:
                    member = ctx.message.reference.resolved.author

            match member:
                case _ if member == ctx.author or member is None:
                    await ctx.send("you dont have anyone to cuddle with? well im a clanker so i unfortunately cant do that")
                    return

                case self.bot.user:
                    await ctx.reply("bro im a clanker")
                    return

                case _:
                    await member.send(f"{ctx.author.mention} is cuddling with you")

            await ctx.send(f"you are cuddling with {member.mention}")
        except Exception as ex:
            await ctx.send(str(ex))

    @commands.command(name="hug", description="lets you hug a member")
    async def hug_cmd(self, ctx: commands.Context, member: discord.Member = commands.parameter(default=None,
                                                                                                  description="the member you wanna hug")):
        try:
            if member is None:
                if ctx.message.reference:
                    member = ctx.message.reference.resolved.author

            match member:
                case _ if member == ctx.author or member is None:
                    await ctx.send("you dont have anyone to hug? fine")
                    member = self.bot.user

                case _:
                    await member.send(f"{ctx.author.mention} is hugging you")

            await ctx.send(f"you are hugging {member.mention}")
        except Exception as ex:
            await ctx.send(str(ex))

    @commands.command(name="ozzo", description="ozzo")
    async def ozzo_cmd(self, ctx: commands.Context):
        if ctx.author.id == 1474293589683994655:
            await ctx.reply("no way its ozzo!!! hi ozzo-chan!!")
            return

        await ctx.send("ozzo")

async def setup(bot: commands.Bot):
    await bot.add_cog(MainCommands(bot))