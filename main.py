import discord, dotenv, os, asyncio, utils
from discord.ext import commands

class OzzoBot(commands.Bot):
    def __init__(self):
        intents = discord.Intents.default()
        intents.message_content = True
        intents.members = True
        super().__init__(command_prefix="ozzo!", intents=intents, help_command=None)

        self.activity = discord.Game(name="with my dihh")

    async def close(self):
        utils.save_data()

dotenv.load_dotenv()

bot = OzzoBot()

async def main():
    utils.load_data()
    async with bot:
        for entry in os.listdir("cogs"):
            if entry.endswith(".py") and not entry.startswith("_"):
                cog_name = entry[:-3]
                await bot.load_extension(f"cogs.{cog_name}")

        await bot.start(os.getenv("BOT_TOKEN"))

asyncio.run(main())