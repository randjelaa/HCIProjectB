using CodeComparer.Models;
using CommunityToolkit.Mvvm.Input;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CodeComparer.ViewModels
{
    public class CodeLineDiff
    {
        public string Text { get; set; }
        public ChangeType ChangeType { get; set; }
    }

    public class MainViewModel : BaseViewModel
    {
        private string _leftCode;
        public string LeftCode
        {
            get => _leftCode;
            set { _leftCode = value; OnPropertyChanged(); }
        }

        private string _rightCode;
        public string RightCode
        {
            get => _rightCode;
            set { _rightCode = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CodeLineComparison> LineComparisons { get; set; } = new();

        public ICommand CompareCommand { get; }

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel created");
            CompareCommand = new RelayCommand(CompareCodes);
        }

        private void CompareCodes()
        {
            LineComparisons.Clear();

            var builder = new SideBySideDiffBuilder(new DiffPlex.Differ());
            var diff = builder.BuildDiffModel(LeftCode ?? "", RightCode ?? "");

            int maxLines = Math.Max(diff.OldText.Lines.Count, diff.NewText.Lines.Count);

            for (int i = 0; i < maxLines; i++)
            {
                var oldLine = i < diff.OldText.Lines.Count ? diff.OldText.Lines[i] : null;
                var newLine = i < diff.NewText.Lines.Count ? diff.NewText.Lines[i] : null;

                LineComparisons.Add(new CodeLineComparison
                {
                    LeftText = oldLine?.Text ?? "",
                    RightText = newLine?.Text ?? "",
                    LeftChangeType = oldLine?.Type ?? ChangeType.Imaginary,
                    RightChangeType = newLine?.Type ?? ChangeType.Imaginary
                });
            }
        }

    }
}
