using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace merchantPlatform.Entities
{
    public class OcrResult:Entity<Guid>
    {
        public string FileName { get; set; }
        public string OcrText {  get; set; }
        public string Label { get; set; }
        public float Accuracy { get; set; }
        public DateTime Date { get; set; }

        [StringLength(11)]
        public string TcKimlikNo { get; set; }


        protected OcrResult() { }


        public OcrResult(Guid id,string fileName, string ocrText, string label, float accuracy,string tcKimlikNo)
           : base(id)
        {
            FileName = fileName;
            OcrText = ocrText;
            Label = label;
            Accuracy = accuracy;
            Date = DateTime.Now;
            TcKimlikNo = tcKimlikNo;
        }
    }
}
