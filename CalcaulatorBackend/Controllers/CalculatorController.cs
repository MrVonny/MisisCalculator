using CalcaulatorBackend.Infrastrucure;
using CalcaulatorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCalc;
using Shared.Models;

namespace CalcaulatorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }


        [HttpPost("")]
        public async Task<IActionResult> Calculate([FromBody] ExpressionRequestDto request)
        {
            try
            {
                var inputString = NormalizeInputString(request.Expression);
                _logger.LogInformation("Normalized: {expr}",  inputString);
                var expression = new Expression(inputString);
                var result = expression.Evaluate();

                _appDbContext.Histories.Add(new History()
                {
                    DeviceName = request.DeviceName,
                    Expressions = request.Expression,
                    CreatedAt = DateTime.UtcNow
                });

                await _appDbContext.SaveChangesAsync();

                return Ok(new ExpressionResponseDto()
                {
                    Result = double.Parse(result?.ToString()),
                    Success = true
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "");
                return BadRequest(new ExpressionResponseDto()
                {
                    Success = false
                });
            }

        }


        [HttpPost("history")]
        public async Task<IActionResult> History([FromBody] HistoryRequestDto request)
        {
            _logger.LogInformation("Device Name: {deviceName}", request.DeviceName);
            
            var history = await _appDbContext.Histories
                .Where(x => x.DeviceName == request.DeviceName).ToListAsync();
            
            return Ok(new HistoryResponseDto()
            {
                History = history
            });

        }

        private string NormalizeInputString(string expr)
        {
            expr = expr.ToUpper();
            Dictionary<string, string> _opMapper = new()
            {
                {"×", "*"},
                {"÷", "/"},
                {"SIN", "Sin"},
                {"COS", "Cos"},
                {"TAN", "Tan"},
                {"ASIN", "Asin"},
                {"ACOS", "Acos"},
                {"ATAN", "Atan"},
                {"LOG", "Log"},
                {"EXP", "Exp"},
                {"LOG10", "Log10"},
                {"POW", "Pow"},
                {"SQRT", "Sqrt"},
                {"ABS", "Abs"},
            };

            return _opMapper.Keys.Aggregate(expr, (current, key) => current.Replace(key, _opMapper[key]));
        }
    }
}