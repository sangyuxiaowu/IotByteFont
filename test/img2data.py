from PIL import Image
import numpy as np

# 打开图片
image = Image.open('38.png')

# 将图片转换为灰度模式
image = image.convert('L')

# 将图片转换为numpy数组
image_data = np.array(image)

# 将像素值二值化
image_data = np.where(image_data > 128, 1, 0)

# 将numpy数组转换为二进制数据
#bin_data = ''.join([str(bit) for row in image_data for bit in row])
# 将二进制数据转换为16进制的数据
#hex_data = [hex(int(bin_data[i:i+8], 2)) for i in range(0, len(bin_data), 8)]


# 将二维数组转换为一维数组
image_data = image_data.flatten()
# 将每8位二值化的像素值组合成一个字节，并反转像素值
bin_data = [''.join(map(str, image_data[i:i+8][::-1].tolist())) for i in range(0, len(image_data), 8)]
# 将二进制数据转换为16进制的数据
hex_data = [hex(int(b, 2)) for b in bin_data]

print(",".join(hex_data))