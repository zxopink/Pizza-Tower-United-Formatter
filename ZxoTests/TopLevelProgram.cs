using UndertaleModLib;
using UndertaleModLib.Models;
using UndertaleModLib.Util;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Nodes;

string filePath = @"G:\SteamLibrary\steamapps\common\Pizza Tower\data.win";
FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
UndertaleData data = UndertaleIO.Read(fs, null);
fs.Close();
Console.WriteLine(data.Sprites.Count);

string output = @"G:\SteamLibrary\steamapps\common\Pizza Tower\output\";

int total = data.Sprites.Count;
int current = 0;
TextureWorker worker = new TextureWorker();

JsonArray allFiles = new JsonArray();
async Task DumpSprites()
{
    var sprites = data.Sprites.Where(sprite => sprite.Name.Content.StartsWith("spr_player_") || sprite.Name.Content.StartsWith("spr_playerN_"));
    total = sprites.Count();
    await Task.Run(() => Parallel.ForEach(sprites, DumpSprite));
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

void DumpSprite(UndertaleSprite sprite)
{
    List<Bitmap> bitmaps = new();
    string sprFileName = sprite.Name.Content + ".png";
    var info = new {
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
            var tex = worker.GetTextureFor(sprite.Textures[i].Texture, sprite.Name.Content + "_" + i + ".png", true); // Include padding to make sprites look neat!
            bitmaps.Add(tex);
        }
    var appended = AppendBitmaps(bitmaps);
    string fPath = Path.Combine(output, sprFileName);
    if (appended != null)
        appended.Save(fPath);

    lock (allFiles)
        allFiles.Add(info);
    Interlocked.Increment(ref current);
    Console.WriteLine($"Dumped {sprite.Name.Content} ({current}/{total})");
}
await DumpSprites();

using (fs = new FileStream(output + "settings.json", FileMode.Create, FileAccess.Write))
    await JsonSerializer.SerializeAsync(fs, allFiles, new JsonSerializerOptions() { WriteIndented = true });