using CalcaulatorBackend.Models;
using Microsoft.AspNetCore.Mvc;
using NCalc;

namespace CalcaulatorBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }


        [HttpPost("")]
        public async Task<IActionResult> Calculate([FromBody] ExpressionRequestDto request)
        {
            try
            {
                var inputString = NormalizeInputString(request.Expression);
                var expression = new Expression(inputString);
                var result = expression.Evaluate();


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

        private string NormalizeInputString(string expr)
        {
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