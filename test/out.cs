/// <summary>
/// 16x16 font sample
/// </summary>
public class IotByteFont : IFont
{
    private static readonly byte[][] _fontTable =
    {
        new byte [] {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00},  // space
        new byte [] {0x00,0x00,0x00,0x00,0xC0,0x03,0x60,0x06,0x30,0x0C,0x30,0x0C,0x60,0x06,0xC0,0x03,0x60,0x06,0x30,0x0C,0x30,0x0C,0x30,0x0C,0x60,0x06,0xC0,0x03,0x00,0x00,0x00,0x00},  // 8 [0xFF18]
    };

    public override byte Width { get => 16; }
    public override byte Height { get => 16; }

    public override byte[] this[char character]
    {
        get
        {
            switch ((byte)character)
            {
                case 24:
                    return _fontTable[0];
                case 25:
                    return _fontTable[1];
                case 33:
                    return _fontTable[2];
                case 34:
                    return _fontTable[3];
                case 159:
                    return _fontTable[4];
                case 43:
                    return _fontTable[5];
                default:
                    return _fontTable[6];
            }
        }
    }
}
