// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text;
using SkiaSharp;

// 从文件中读取所有字符
var characters = File.ReadAllText(@"G:\csharp\IotFont\data\gb2312.txt");
var fontPath = @"G:\csharp\IotFont\data\ph45.ttf";
var size = 16;
foreach (char character in characters)
{
    // 生成字符的位图
    ConvertFontToBitmap(fontPath, character, size);
}

float GetBrightness(SKColor color)
{
    float r = color.Red / 255f;
    float g = color.Green / 255f;
    float b = color.Blue / 255f;

    float max = Math.Max(r, Math.Max(g, b));
    float min = Math.Min(r, Math.Min(g, b));

    return (max + min) / 2;
}

void ConvertFontToBitmap(string fontPath, char character, int size)
{
    // 创建一个SKTypeface实例
    var typeface = SKTypeface.FromFile(fontPath);
    //SKTypeface.FromFamilyName("Microsoft YaHei UI");
    // 创建一个SKPaint实例
    var paint = new SKPaint
    {
        Typeface = typeface,
        TextSize = size,
        IsAntialias = true,
        Color = SKColors.White,
    };

    // 创建一个足够大的bitmap来容纳字符
    var bitmap = new SKBitmap(size, size);
    // 创建一个SKCanvas实例
    var canvas = new SKCanvas(bitmap);
    // 将canvas的背景填充为白色
    canvas.Clear(SKColors.Black);
    // 在bitmap上绘制字符

    // 字符 3/4 大小防止超界
    canvas.DrawText(character.ToString(), 0, size * 3 / 4, paint);

    
    List<string> hexData = new List<string>();
    for (int y = 0; y < bitmap.Height; y++)
    {
        for (int x = 0; x < bitmap.Width; x += 8)
        {
            int value = 0;
            for (int bit = 0; bit < 8; bit++)
            {
                SKColor color = bitmap.GetPixel(x + bit, y);
                // 将像素值二值化
                int binary = color.Red > 128 ? 1 : 0;
                // 将二值化的像素值组合成一个字节，并反转像素值
                value |= binary << (7 - bit);
            }
            // 将二进制数据转换为16进制的数据
            hexData.Add(Convert.ToString(value, 16));
        }
    }

    // 将16进制数据转换为C#代码
    StringBuilder csCode = new StringBuilder();
    csCode.Append("new byte[] { ");
    foreach (var hex in hexData)
    {
        csCode.Append(("0x" + hex).PadRight(4, '0') + ", ");
    }
    csCode.Remove(csCode.Length - 2, 2); // 移除最后的逗号和空格
    csCode.Append(" }");

    // 输出字符和C#代码

    Console.WriteLine($"char: {character}, code: {csCode}");


    // 将bitmap保存为png文件
    using (var image = SKImage.FromBitmap(bitmap))
    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
    using (var stream = File.OpenWrite($@"G:\csharp\IotFont\tmp\{(int)character}.png"))
    {
        data.SaveTo(stream);
    }
}