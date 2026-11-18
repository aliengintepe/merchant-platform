using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace merchantPlatform.ML.Models
{
    public class DocumentFeatures
    {

        public float Keyword_Kimlik { get; set; }
        public float Keyword_Vergi { get; set; }
        public float Keyword_TicaretSicil { get; set; }
        public float HasTcKimlikNo { get; set; }


        public float CharLength { get; set; }
        public float DigitRatio { get; set; }
        public float DatePatternCount { get; set; }
        public float NumberPatternCount { get; set; }
        public string Text { get;  set; }
        public string Label { get;  set; }
    }
}
