using ImageMagick;
using merchantPlatform.IServices;
using OpenCvSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Tesseract;
using Volo.Abp.Application.Services;

namespace merchantPlatform.Services
{
    public class OcrService : ApplicationService, IOcrService
    {
        public OcrService()
        {
            var ghostscriptFilePath = @"C:\gs10.05.1\bin\gswin64c.exe";

            MagickNET.SetGhostscriptDirectory(Path.GetDirectoryName(ghostscriptFilePath));
        }
        public async Task<string> ExtractTextAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("Dosya bulunamadı.");
            }

            var extension = Path.GetExtension(filePath).ToLower();
            string fullText = string.Empty;

            if (extension == ".pdf")
            {
                fullText = await ProcessPdfAsync(filePath);
            }
            else if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".tiff")
            {
                fullText = await ProcessImageAsync(filePath);
            }
            else
            {
                throw new NotSupportedException("Desteklenmeyen dosya formatı.");
            }
            return fullText;
        }

        private async Task<string> ProcessPdfAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                string text = string.Empty;
                var settings = new MagickReadSettings { Density = new Density(300, 300) };

                using (var images = new MagickImageCollection())
                {
                    images.Read(filePath, settings);
                    foreach (var image in images)
                    {
                       
                        var imageBytes = image.ToByteArray(MagickFormat.Png);
                        using var mat = Cv2.ImDecode(imageBytes, ImreadModes.Color);
                        text += ProcessMatForOcr(mat);
                    }
                }
                return text;
            });
        }

        private async Task<string> ProcessImageAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                using var src = Cv2.ImRead(filePath, ImreadModes.Color);
                if (src.Empty())
                {
                    throw new FileNotFoundException("Görüntü dosyası yüklenemedi.");
                }
                return ProcessMatForOcr(src);
            });
        }

        private string ProcessMatForOcr(Mat mat)
        {
            using var denoised = new Mat();
            Cv2.FastNlMeansDenoisingColored(mat, denoised, 10, 10, 7, 21);

            using var gray = new Mat();
            Cv2.CvtColor(denoised, gray, ColorConversionCodes.BGR2GRAY);

            using var thresholded = new Mat();
            Cv2.AdaptiveThreshold(gray, thresholded, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 15, 10);

            // 🟢 Pix nesnesini oluşturmak için ön işlenmiş görüntüyü byte dizisine dönüştür
            var preprocessedBytes = thresholded.ToBytes(".png");
            using var pix = Pix.LoadFromMemory(preprocessedBytes);

            using var engine = new TesseractEngine(@"./tessdata", "tur", EngineMode.LstmOnly);
            using var page = engine.Process(pix);

            return page.GetText();
        }
    }
}