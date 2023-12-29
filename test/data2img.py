from PIL import Image
import numpy as np

# 点阵数据
data = [
    0x80,0x00,0x80,0x00,0x80,0x10,0xFE,0x3F,0x80,0x00,0x80,0x00,0x80,0x20,0xFF,0x7F,
    0x80,0x00,0x40,0x01,0x40,0x01,0x20,0x02,0x20,0x02,0x10,0x0C,0x08,0x70,0x06,0x20
]
data = [
    0x30,0x0 ,0x78,0x0 ,0xcc,0x0 ,0xcc,0x0 ,0x6c,0x0 ,0x38,0x0 ,0x1c,0x3 ,0x36,0x3 ,
    0x62,0x1 ,0xc3,0x1 ,0xc6,0x1 ,0x7c,0x7 ,0x0 ,0x0 ,0x0 ,0x0 ,0x0 ,0x0 ,0x0 ,0x0
]

#bin_data = [bin(d)[2:].zfill(8) for d in data]
# 将16进制的数据转换为二进制的数据，并反转每个字节的位
bin_data = [bin(d)[2:].zfill(8)[::-1] for d in data]


# 将二进制数据转换为numpy数组
image_data = np.array([[int(bit) for bit in d] for d in bin_data])

# 重新整形为16x16的格式
image_data = image_data.reshape((16, 16))

# 创建图像
image = Image.fromarray(image_data.astype(np.uint8)*255)

# 显示图像
image.show()
