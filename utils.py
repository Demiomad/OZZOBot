import random, json, os, discord
from discord.ext import commands

SOUNDS = ["ah", "mmh", "ngh", "hngh", "mmm", "nya", "nyah", "oh"]
ITEMS = ["the floor", "the screen", "my room", "my hands", "my wall"]
ACTIVITIES = ["watching videos", "chatting", "gooning", "using this bot", "existing", "playing geometry dash", "beating a demon level", "asking chatgpt"]
PREFIXES = ["fuck", "shit", "oh my god"]
RESPONSES = ["stop it", "nngh~", "what the fuck do you want", "...", "??", "hm?", "fuck you",
             "**WHAT DO YOU WANT**", "mmrp~", "`sudo rm -rf / --no-preserve-root`", "a-are you a femboy..?"]
FEEDBACK_WEBHOOK_URL = "https://discord.com/api/webhooks/1485651416000368802/uzuc20rb96Kkv6PZLNqyIA0pPLgUM8hXPGthCX9j-reH8F-eEJLYw1QtigauTP1xnHFT"
channels_with_sound = []
feedbacks = {}

def get_random_sound():
    tilde_amount = random.randint(1, 2)
    return random.choice(SOUNDS) + "~" * tilde_amount

def owner_or_admin():
    async def predicate(ctx: commands.Context):
        return await ctx.bot.is_owner(ctx.author) or ctx.author.guild_permissions.administrator
    return commands.check(predicate)

def save_data():
    print("Saving data...")
    global feedbacks
    global channels_with_sound

    data = {
        "sound_enabled_channels": channels_with_sound
    }

    with open("db.json", "w") as f:
        json.dump(data, f, separators=(',', ':'))

def load_data():
    if not os.path.exists("db.json"):
        print("No database file found")
        return

    print("Loading data...")

    global feedbacks
    global channels_with_sound

    output = dict()
    with open("db.json", "r") as f:
        output = json.load(f)

    channels_with_sound = output.get("sound_enabled_channels", [])

def is_valid_reply(msg: discord.Message):
    ref = msg.reference
    resolved = ref.resolved
    return ref and resolved and isinstance(ref.resolved, discord.Message)