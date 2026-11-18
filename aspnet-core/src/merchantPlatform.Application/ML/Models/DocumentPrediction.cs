using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace merchantPlatform.ML.Models
{
    public class DocumentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel { get; set; }

       
        public float[] Score { get; set; }
    }
}
