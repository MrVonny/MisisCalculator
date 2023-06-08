using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models;

public class ExpressionRequestDto
{
    public string DeviceName { get; set; }
    public string Expression { get; set; }

}

public class HistoryRequestDto
{
    public string DeviceName { get; set; }
}

public class HistoryResponseDto
{
    public List<History> History { get; set; }
}

public class History
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string Expressions { get; set; }
    public DateTime CreatedAt { get; set; }
}