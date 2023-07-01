
using CrossChecker;
using System.IO;
using UndertaleModLib;
using UndertaleModLib.Models;



UndertaleData LoadData(string path) => UndertaleIO.Read(File.OpenRead(path), null);
async Task<UndertaleData> LoadDataAsync(string path)
{
    return await Task.Run(() =>
    {
        UndertaleData data = LoadData(path);
        Console.WriteLine($"Done reading {path}");
        return data;
    });
}

Console.WriteLine("Starting");
UndertaleData original = LoadData(@"G:\SteamLibrary\steamapps\common\Pizza Tower\data_original.win");
Console.WriteLine($"Done reading original");
UndertaleData modified = LoadData(@"G:\SteamLibrary\steamapps\common\Pizza Tower\data.win");
Console.WriteLine($"Done reading modified");

Console.WriteLine("Finding Scripts...");
List<UndertaleRoom> rooms = new();

List<string> orignalLevel = original.Rooms.Select(room => room.Name.Content).ToList();

var discRoom = modified.Rooms.Where(room => !orignalLevel.Contains(room.Name.Content)); //Codes that don't exist in the original
var sharedRooms = modified.Rooms //Codes that exist in both
    .Where(code => original.Rooms.Any(originalRoom => originalRoom.Name.Content == code.Name.Content));

Parallel.ForEach(sharedRooms, modifiedRoom =>
{
    UndertaleRoom originalRoom = original.Rooms.First(room => room.Name.Content == modifiedRoom.Name.Content);
    if (DifferenceChecker.DifferentRoom(originalRoom, modifiedRoom))
        rooms.Add(modifiedRoom);
});

rooms.AddRange(discRoom);

foreach (var code in rooms)
{
    Console.WriteLine(code.Name.Content);
}
Console.WriteLine("Done finding scripts");

#region Scripts
//Console.WriteLine("Finding Scripts...");
//List<UndertaleCode> codes = new();

//List<string> orignalCodeNames = original.Code.Select(code => code.Name.Content).ToList();

//var disc = modified.Code.Where(code => !orignalCodeNames.Contains(code.Name.Content)); //Codes that don't exist in the original
//var sharedScripts = modified.Code //Codes that exist in both
//    .Where(code => original.Code.Any(originalCode => originalCode.Name.Content == code.Name.Content));

//Parallel.ForEach(sharedScripts, modifiedCode =>
//{
//    UndertaleCode originalCode = original.Code.First(code => code.Name.Content == modifiedCode.Name.Content);
//    if (DifferenceChecker.DifferentCodes(originalCode, modifiedCode))
//        codes.Add(modifiedCode);
//});

//codes.AddRange(disc);

//foreach (var code in codes)
//{
//    Console.WriteLine(code.Name.Content);
//}
//Console.WriteLine("Done finding scripts");
#endregion