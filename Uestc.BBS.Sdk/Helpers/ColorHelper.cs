using System.Drawing;

namespace Uestc.BBS.Sdk.Helpers
{
    public static class ColorHelper
    {
        /// <summary>
        /// 将 HEX 颜色字符串转换为 Color 对象
        /// </summary>
        /// <param name="hex">示例：#FFFFFF</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Color FromHex(string hex)
        {
            if (!hex.StartsWith('#'))
            {
                throw new ArgumentException("Invalid hex color string");
            }

            hex = hex[1..];
            var value = Convert.ToInt32(hex, 16);

            return hex.Length is 8
                ? Color.FromArgb(value)
                : Color.FromArgb((int)(value | 0xFF000000));
        }
    }
}
