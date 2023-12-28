// See https://aka.ms/new-console-template for more information
using System.IO;
using SkiaSharp;

// 从文件中读取所有字符
var characters = File.ReadAllText(@"G:\csharp\IotFont\data\base.txt");
var fontPath = @"G:\csharp\IotFont\data\ph45.ttf";
var size = 16;
foreach (char character in characters)
{
    // 生成字符的位图
    ConvertFontToBitmap(fontPath, character, size);
}


void ConvertFontToBitmap(string fontPath, char character, int size)
{
    // 创建一个SKTypeface实例
    var typeface = SKTypeface.FromFile(fontPath);
    // 创建一个SKPaint实例
    var paint = new SKPaint
    {
        Typeface = typeface,
        TextSize = size,
        IsAntialias = true,
        Color = SKColors.Black
    };
    // 创建一个足够大的bitmap来容纳字符
    var bitmap = new SKBitmap(size, size);
    // 创建一个SKCanvas实例
    var canvas = new SKCanvas(bitmap);
    // 将canvas的背景填充为白色
    canvas.Clear(SKColors.White);
    // 在bitmap上绘制字符
    canvas.DrawText(character.ToString(), 0, size, paint);
    // 将bitmap保存为png文件
    using (var image = SKImage.FromBitmap(bitmap))
    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
    using (var stream = File.OpenWrite($@"G:\csharp\IotFont\data\{(int)character}.png"))
    {
        data.SaveTo(stream);
    }
}