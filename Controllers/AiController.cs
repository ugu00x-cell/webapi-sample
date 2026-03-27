using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    // POST /api/ai/analyze
    [HttpPost("analyze")]
    public IActionResult Analyze([FromBody] AnalyzeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            return BadRequest(new { error = "text は必須です" });

        // モックレスポンス（API呼び出しなし・デモ用）
        var result = $"【モック応答】受信したテキスト：{request.Text}";
        return Ok(new { result });
    }
}

// Flask: request.json["text"] に相当する型定義
public class AnalyzeRequest
{
    public string Text { get; set; } = "";
}
