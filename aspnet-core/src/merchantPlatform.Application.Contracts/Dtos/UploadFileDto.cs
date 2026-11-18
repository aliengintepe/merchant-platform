using Microsoft.AspNetCore.Http;
using System;

namespace merchantPlatform.Dtos
{
    public class UploadFileDto
    {
        public IFormFile File { get; set; }
    }

    public class OcrResultDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string OcrText { get; set; }
        public string Label { get; set; }
        public float Accuracy { get; set; }
        public DateTime Date { get; set; }
        public string TcKimlikNo { get; set; }
    }

    public class CreateOcrResultDto
    {
        public string FileName { get; set; }
        public string OcrText { get; set; }
        public string Label { get; set; }
        public float Accuracy { get; set; }
        public string TcKimlikNo { get; set; }
    }

    public class UpdateOcrResultDto
    {
        public string Label { get; set; }
    }
}
