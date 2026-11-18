using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace merchantPlatform.Helpers
{
    
        public static class ImagePreprocessor
        {
            public static string Preprocess(string inputPath)
            {
                var src = Cv2.ImRead(inputPath, ImreadModes.Grayscale);

                // Gürültü azalt
                Cv2.GaussianBlur(src, src, new Size(3, 3), 0);

                // Uyarlamalı eşikleme
                var processed = new Mat();
                Cv2.AdaptiveThreshold(src, processed, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 11, 2);

                // İşlenmiş dosya yolu
                var processedPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + "_processed.png");

                Cv2.ImWrite(processedPath, processed);
                return processedPath;
            }
        }
    }

