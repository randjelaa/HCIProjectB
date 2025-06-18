using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeComparer.Models
{
    public class CodeLineComparison
    {
        public string LeftText { get; set; }
        public string RightText { get; set; }

        public ChangeType LeftChangeType { get; set; }
        public ChangeType RightChangeType { get; set; }
    }
}
