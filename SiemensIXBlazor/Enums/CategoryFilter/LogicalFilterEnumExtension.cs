using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensIXBlazor.Enums.CategoryFilter
{
    public static class LogicalFilterEnumExtension
    {
        public static string ToEnumString(this LogicalFilterOperator filter)
        {
            return filter switch
            {
                LogicalFilterOperator.Equal => "Equal",
                LogicalFilterOperator.NotEqual => "Not equal",
                _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
            };
        }
    }
}
