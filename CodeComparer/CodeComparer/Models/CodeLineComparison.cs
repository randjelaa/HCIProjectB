using DiffPlex.DiffBuilder.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeComparer.Models
{
    public partial class CodeLineComparison : ObservableObject
    {
        [ObservableProperty]
        private string leftText;

        [ObservableProperty]
        private string rightText;

        [ObservableProperty]
        private ChangeType leftChangeType;

        [ObservableProperty]
        private ChangeType rightChangeType;

        [ObservableProperty]
        private bool isMatch;
    }
}
