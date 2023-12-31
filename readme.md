# IotByteFont: .NET Dot Matrix Font Creator Tool

IotByteFont is a .NET dot matrix font creation tool. It can load your favorite font files and generate custom dot matrix font code. This tool is especially useful for IoT projects that require custom font rendering, and it is applicable to .NET nanoFramework.

English | [简体中文](./readme_zh.md)

## Installation

Install the IotByteFont tool with the following command:

```bash
dotnet tool install -g IotByteFont
```

## Usage

Here are the command-line options for the IotByteFont tool:

```
IotByteFont 1.0.0
Copyright (C) 2023 SangSQ

  -f, --font         (Default: Microsoft YaHei UI) Font file path or font name.

  -c, --char         Required. (Default: chars.txt) Char file path or char string.

  -s, --size         (Default: 16) Font size.

  -w, --width        (Default: 0) Font width. 0 means same as font size.

  -y, --yoffset      (Default: 0.75) Font y offset. size * y. Not recommended to adjust. Adjust with debug mode.

  -t, --threshold    (Default: 128) Threshold for binarization.

  -n, --name         (Default: IotByteFont) Output class name.

  -d, --debug        (Default: false) Debug mode. Print debug info and bitmap.

  --help             Display this help screen.

  --version          Display version information.
```

### General Use

To create a font, you need to specify the font file or font name, the characters to include in the font, and the size of the font. You can also specify the width of the font, the y offset, and the name of the output class. Turning on debug mode can print debug information and the final font bitmap.

```bash
IotByteFont --char "abcde" --size 8
IotByteFont --font ms.ttf --char chars.txt --yoffset 0.6 --name MyFont --debug
```

### Special Use

Due to the current limitations of `nanoFramework.Iot.Device.Ssd13xx`, the font size width must be 8 or 16. So if you need to use other sizes of fonts, you need to use the following method to display:

```cs
public static void DarwString(Ssd13xx device, int x, int y, string str, byte size = 1)
{
    int inx = 0;
    int fontWidth = device.Font.Width;
    int fontHeight = device.Font.Height;

    int fontWidthTimesSize = fontWidth * size;
    int fontArea = fontWidth * fontHeight;

    byte[] bitMap = new byte[fontArea];

    foreach (char c in str)
    {
        // Font data, hexadecimal data of device.Font.Width * device.Font.Height
        byte[] charBytes = device.Font[c];

        for (int i = 0; i < charBytes.Length; i++)
        {
            byte b = charBytes[i];
            int baseIndex = i * 8;
            for (int j = 0; j < 8; j++)
            {
                // Get binary bit
                int bit = (b >> j) & 1;
                // Store binary bit to bitmap array
                bitMap[baseIndex + j] = (byte)bit;
            }
        }

        // Draw the bitmap from left to right, from top to bottom according to the font size
        int baseX = x + fontWidthTimesSize * inx;
        for (int i = 0; i < fontHeight; i++)
        {
            int baseY = y + i * size;
            for (int j = 0; j < fontWidth; j++)
            {
                // Get binary bit
                int bit = bitMap[i * fontWidth + j];
                // Draw pixel or fill rectangle according to size
                if (size == 1)
                {
                    device.DrawPixel(baseX + j * size, baseY, bit == 1);
                }
                else
                {
                    device.DrawFilledRectangle((baseX + j * size), baseY, size, size, bit == 1);
                }
            }
        }
        inx++;
    }
}
```

This function allows you to display fonts of any size with a width*height that is a multiple of 8. Here is an example of how to call the function:

```cs
using Ssd1306 device = new Ssd1306(I2cDevice.Create(new I2cConnectionSettings(1, Ssd1306.DefaultI2cAddress)), Ssd13xx.DisplayResolution.OLED128x64);
device.ClearScreen();
device.Font = new IotByteFont();
DarwString(device, 2, 32, "桑榆肖物", 2);
DarwString(device, 0, 0, "IotByteFont", 1);
device.Display();
```

## Preset chars and Font Recommendations

There are some useful resources in the data directory, including a font and several chars files.

Here we recommend the open source dot matrix font [unifont](https://unifoundry.com/unifont/index.html) and the [Alibaba PuHuiTi 45 Light](./data/ph45.ttf) in the directory.

## Advanced Parameters and Debugging

Generally, we only need one parameter `-c` to specify the chars file in Windows. If you don't need to adjust more font display effects, then you can use the following parameters:

- `-y` Adjust the y offset of the font, the default is 0.75 times the font size. If the font display is not complete, you can adjust this parameter.
- `-t` Adjust the threshold for font binarization, the default is 128. If the binarization display of the font image is not clear, you can adjust this parameter.

When adjusting the effect, it is recommended to turn on debug mode. You can enable debug mode with the `--debug` option. This will print debug information and the final font bitmap.

![font](./doc/IotByteFont.png)

## Contributing

Contributions to IotByteFont are welcome. Please submit pull requests or create issues on the GitHub repository.

## License

IotByteFont is licensed under the MIT License. For more information, please see the LICENSE file.