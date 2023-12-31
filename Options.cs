using CommandLine;

/// <summary>
/// 启动选项设置。
/// </summary>
public class Options
{
    /// <summary>
    /// 字体设置，可以是字体文件路径，也可以是字体名称。
    /// </summary>
    [Option('f', "font", Default ="Microsoft YaHei UI", HelpText = "Font file path or font name.")]
    public string? Font { get; set; }

    /// <summary>
    /// 字符设置，用于指定要生成的字符或字符文件。
    /// </summary>
    [Option('c', "char",Required = true, Default = "chars.txt", HelpText = "Char file path or char string.")]
    public string? Char { get; set; }

    /// <summary>
    /// 字体大小。
    /// </summary>
    [Option('s', "size", Default = 16, HelpText = "Font size.")]
    public int Size { get; set; }

    /// <summary>
    /// 字体宽度。0 表示和字体大小相同。
    /// </summary>
    [Option('w', "width", Default = 0, HelpText = "Font width. 0 means same as font size.")]
    public int Width { get; set; }

    /// <summary>
    /// 输出字体Y轴的偏移系数，size * y。
    /// 不建议调整，调整时建议打开调试模式，核查点阵图片是否正确。
    /// </summary>
    [Option('y', "yoffset", Default = 0.75f, HelpText = "Font y offset. size * y. Not recommended to adjust. Adjust with debug mode.")]
    public float YOffset { get; set; }

    /// <summary>
    /// 二值化阈值。
    /// </summary>
    [Option('t', "threshold", Default = 128, HelpText = "Threshold for binarization.")]
    public int Threshold { get; set; }
    

    /// <summary>
    /// 输出的类名。
    /// </summary>
    [Option('n', "name", Default = "IotByteFont", HelpText = "Output class name.")]
    public string? Name { get; set; }

    /// <summary>
    /// Debug 模式，输出字符调试信息和位图。
    /// </summary>
    [Option('d', "debug", Default = false, HelpText = "Debug mode. Print debug info and bitmap.")]
    public bool Debug { get; set; }
}
