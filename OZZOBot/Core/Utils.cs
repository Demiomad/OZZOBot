using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OZZOBot.Core
{
    public static class Utils
    {
        public static string[] Sounds = ["ah", "mmh", "ngh", "hngh", "mmm", "nya", "nyah", "oh"];

        public static string[] Locations = ["the floor", "the screen", "my room", "my hands", "my wall"];

        public static string[] Activities = ["watching videos", "chatting", "gooning", "using this bot",
            "existing", "playing geometry dash", "beating a demon level", "asking ai", "breathing"];

        public static string[] Items = ["geometry dash", "the chicken", "this bot", "socrates"];

        public static string[] Prefixes = ["fuck", "shit", "oh my god"];

        public static string[] Responses = ["stop it", "nngh~", "what the fuck do you want", "...", "??", "hm?", "fuck you",
             "**WHAT DO YOU WANT**", "mmrp~", "`sudo rm -rf / --no-preserve-root`", "a-are you a femboy..?", "ah~!!", "istg",
            "kyah~!!", "nyah~!", "im gonna fucking touch you", "im gonna rail you"];

        public static string[] Subtexts = ["someone rail me >///<", "i just nut...", "uhhh", "Your device ran into a pro",
            "n-ngh..", "goon"];

        public static string[] CrackSubtexts = ["y-you like it don't you~? >///<", "do you like it?", ">///<", "is ts a sign"];

        public static List<ulong> ChannelsWithSound = [];
        private static Dictionary<string, byte[]> CachedResources = [];

        public static string GetSound(int tildeAmount)
        {
            var tildes = new string('~', tildeAmount);
            return Sounds[Random.Shared.Next(Sounds.Length)] + tildes;
        }

        public static byte[] GetResource(string resName)
        {
            if (CachedResources.TryGetValue(resName, out var result))
                return result;

            var asm = Assembly.GetExecutingAssembly();
            var stream = asm.GetManifestResourceStream(resName);
            var ms = new MemoryStream();

            stream?.CopyTo(ms);
            var array = ms.ToArray();

            CachedResources.Add(resName, array);
            return array;
        }

        public static string GetAttachmentUrl(string fileName)
            => $"attachment://{fileName}";
    }
}
