using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using UndertaleModLib;
using UndertaleModLib.Models;
using UndertaleModLib.Util;
using static System.Net.Mime.MediaTypeNames;

namespace ZxoTests
{
    public class SpriteExtractor
    {
        public UndertaleData Data { get; private set; } = default!;
        public int Progress => (int)(((float)current / Total) * 100);
        public int Total { get; private set; }
        public int Current => current;
        public int current = 0;
        public string Output { get; init; }
        /// <summary>What should the sprite start with</summary>
        public string Filter { get; init; }
        
        
        private TextureWorker Worker { get; init; }

        private JsonArray SpritesInfo { get; init; } = new();
        public bool SpriteSplit { get; init; }

        private static UndertaleData Read(string dataPath)
        {
            FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
            UndertaleData data = UndertaleIO.Read(fs, null);
            fs.Close();
            return data;
        }

        private string DataPath;
        public SpriteExtractor(string dataPath, string output, string filter, bool spriteSplit = false) : this(default(UndertaleData), output, filter, spriteSplit)
        {
            DataPath = dataPath;
        }
        public SpriteExtractor(UndertaleData data, string output, string filter, bool spriteSplit = false)
        {
            SpriteSplit = spriteSplit;
            Data = data;
            Total = 1;
            Worker = new TextureWorker();
            Output = output;
            Filter = filter;
            Directory.CreateDirectory(output);
        }


        //private ZipArchive Zip = default!;
        public async Task Start()
        {
            if (Data == null)
                await Task.Run(() => Data = Read(DataPath));

            //Zip = new ZipArchive(fs, ZipArchiveMode.Create);
            await DumpSprites();
            //var infoEntry = Zip.CreateEntry("config.json");
            //using (var infoStream = infoEntry.Open())
            using (var fs = File.Create(Path.Combine(Output, "config.json")))
                await JsonSerializer.SerializeAsync(fs, SpritesInfo, new JsonSerializerOptions() { WriteIndented = true });
        }

        async Task DumpSprites()
        {
            var sprites = !string.IsNullOrEmpty(Filter) ?
                Data.Sprites.Where(sprite => sprite.Name.Content.StartsWith(Filter)) :
                Data.Sprites.Where(sprite => sprite.Name.Content.StartsWith("spr_player_") || sprite.Name.Content.StartsWith("spr_playerN_"));
            Total = sprites.Count();
            await Task.Run(() => Parallel.ForEach(sprites, DumpSprite));
        }

        void DumpSprite(UndertaleSprite sprite)
        {
            List<Bitmap> bitmaps = new();
            string sprFileName = sprite.Name.Content + ".png";
            var info = new
            {
                Name = sprite.Name.Content,
                FilePath = sprFileName,
                Width = sprite.Width,
                Height = sprite.Height,
                Margin_Left = sprite.MarginLeft,
                Margin_Right = sprite.MarginRight,
                Margin_Bottom = sprite.MarginBottom,
                Margin_Top = sprite.MarginTop,
                Origin_X = sprite.OriginX,
                Origin_Y = sprite.OriginY,
                TextureCount = sprite.Textures.Count,
                IsSpecialType = sprite.IsSpecialType,
                PlaybackSpeed = sprite.GMS2PlaybackSpeed,
                PlaybackSpeedType = sprite.GMS2PlaybackSpeedType,
            };

            for (int i = 0; i < sprite.Textures.Count; i++)
                if (sprite.Textures[i]?.Texture != null)
                {
                    //worker.ExportAsPNG(sprite.Textures[i].Texture, output + sprite.Name.Content + "_" + i + ".png", null, true); // Include padding to make sprites look neat!
                    var tex = Worker.GetTextureFor(sprite.Textures[i].Texture, sprite.Name.Content + "_" + i + ".png", true); // Include padding to make sprites look neat!
                    bitmaps.Add(tex);
                }
            
            if (bitmaps.Count > 0)
            {
                if (SpriteSplit)
                {
                    var appended = AppendBitmaps(bitmaps);
                    string fPath = Path.Combine(Output, sprFileName);
                    //var entry = Zip.CreateEntry(fPath);
                    //using (var stream = entry.Open())
                    //    appended.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    appended.Save(fPath);
                }
                else
                {
                    string fPath = Path.Combine(Output, sprite.Name.Content);
                    Directory.CreateDirectory(fPath);
                    for (int i = 0; i < bitmaps.Count; i++)
                    {
                        string filePath = Path.Combine(fPath, sprite.Name.Content + "_" + i + ".png");
                        bitmaps[i].Save(filePath);
                    }
                }
            }

            lock (SpritesInfo)
                SpritesInfo.Add(info);
            Interlocked.Increment(ref current);
        }

        Bitmap? AppendBitmaps(IList<Bitmap> bitmaps)
        {
            if (bitmaps.Count == 0)
                return null;
            int width = 0;
            int height = 0;
            foreach (Bitmap bitmap in bitmaps)
            {
                width += bitmap.Width;
                height = Math.Max(height, bitmap.Height);
            }
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                int x = 0;
                foreach (Bitmap bitmap in bitmaps)
                {
                    g.DrawImage(bitmap, x, 0);
                    x += bitmap.Width;
                }
            }
            return result;
        }
    }
}
