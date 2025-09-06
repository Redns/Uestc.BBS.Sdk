using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Models
{
    public class Attachment
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("attachment_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [JsonPropertyName("filename")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小
        /// </summary>
        public ulong Size { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Price { get; set; } = 0;

        /// <summary>
        /// 是否为图片
        /// </summary>
        [JsonPropertyName("is_image")]
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool IsImage { get; set; } = false;

        /// <summary>
        /// 图片宽度
        /// </summary>
        [JsonPropertyName("width")]
        public uint ImageWidth { get; set; } = 0;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("thumb")]
        public bool IsThumb { get; set; } = false;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("picid")]
        public int PicId { get; set; } = 0;

        /// <summary>
        /// 文件路径（如：/thumb/data/attachment/forum/202508/12/182636ndhlldrqlhh1z1d1.png?variant=original）
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Remote { get; set; } = 0;

        /// <summary>
        /// 缩略图路径（如：/thumb/data/attachment/forum/202508/12/182636ndhlldrqlhh1z1d1.png）
        /// </summary>
        [JsonPropertyName("thumbnail_url")]
        public string ThuhumbnailUrl { get; set; } = string.Empty;

        /// <summary>
        /// 原始文件路径（如：/data/attachment/forum/202508/12/182636ndhlldrqlhh1z1d1.png）
        /// </summary>
        [JsonPropertyName("raw_url")]
        public string OriginUrl { get; set; } = string.Empty;
    }
}
