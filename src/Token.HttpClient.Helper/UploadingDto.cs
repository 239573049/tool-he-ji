using System.IO;

namespace Token.HttpClientHelper
{
    /// <summary>
    /// 上传文件dto
    /// </summary>
    public class UploadingDto
    {
        /// <summary>
        /// 文件流
        /// </summary>
        public Stream? Stream { get; set; }

        /// <summary>
        /// 接口对应参数
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string? FileName { get; set; }
    }
}
