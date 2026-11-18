using merchantPlatform.Dtos;
using merchantPlatform.IServices;
using merchantPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[ApiController]

[Route("api/app/ocr")]
public class OcrController : ControllerBase
{
    private readonly IOcrService _ocrService;
    private readonly IOcrResultService _ocrResultService;
    private readonly DocumentClassificationService _docService;

    public OcrController(IOcrService ocrService, IOcrResultService ocrResultService, DocumentClassificationService docService)
    {
        _ocrService = ocrService ?? throw new ArgumentNullException(nameof(ocrService));
        _ocrResultService = ocrResultService ?? throw new ArgumentNullException(nameof(ocrResultService));
        _docService = docService ?? throw new ArgumentNullException(nameof(docService));
    }

    [HttpPost("upload")]
    [RequestSizeLimit(20_000_000)]
    public async Task<IActionResult> UploadAsync([FromForm] UploadFileDto input)
    {
        if (input?.File == null || input.File.Length == 0)
            return BadRequest("Dosya geçersiz.");

        var tempFileName = Path.GetRandomFileName() + Path.GetExtension(input.File.FileName);
        var tempFilePath = Path.Combine(Path.GetTempPath(), tempFileName);

        try
        {
            await using (var stream = System.IO.File.Create(tempFilePath))
                await input.File.CopyToAsync(stream);

            var ocrText = await _ocrService.ExtractTextAsync(tempFilePath);
            if (string.IsNullOrWhiteSpace(ocrText))
                return BadRequest("OCR sonucu boş.");

            string tcKimlikNo = null;
            var match = Regex.Match(ocrText, @"\b[1-9]{1}[0-9]{9}[02468]{1}\b");
            if (match.Success)
            {
                tcKimlikNo = match.Value;
            }

            var prediction = _docService.Predict(ocrText);

            var createDto = new CreateOcrResultDto
            {
                FileName = input.File.FileName,
                OcrText = ocrText,
                Label = prediction.PredictedLabel,
                Accuracy = prediction.Score.Max(),
                TcKimlikNo = tcKimlikNo
            };

            var created = await _ocrResultService.CreateAsync(createDto);
            return Ok(created);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = new
                {
                    message = "Sunucu tarafında hata oluştu: " + ex.Message,
                    stackTrace = ex.StackTrace
                }
            });
        }
        finally
        {
            if (System.IO.File.Exists(tempFilePath))
            {
                try
                {
                    System.IO.File.Delete(tempFilePath);
                }
                catch
                {
                    // İsteğe bağlı: loglama yapabilirsin
                }
            }
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OcrResultDto>> GetAsync(Guid id)
    {
        var dto = await _ocrResultService.GetAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<OcrResultDto>>> GetListAsync()
    {
        var list = await _ocrResultService.GetListAsync();
        return Ok(list);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OcrResultDto>> UpdateAsync(Guid id, [FromBody] UpdateOcrResultDto input)
    {
        var updated = await _ocrResultService.UpdateAsync(id, input);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _ocrResultService.DeleteAsync(id);
        return NoContent();
    }
}
