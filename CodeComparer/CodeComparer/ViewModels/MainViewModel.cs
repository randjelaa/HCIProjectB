using CodeComparer.Models;
using CommunityToolkit.Mvvm.Input;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace CodeComparer.ViewModels
{
    public class CodeLineDiff
    {
        public string Text { get; set; }
        public ChangeType ChangeType { get; set; }
    }

    public class MainViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
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

        private int _sameCount;
        public int SameCount { get => _sameCount; set => SetProperty(ref _sameCount, value); }

        private int _insertedCount;
        public int InsertedCount { get => _insertedCount; set => SetProperty(ref _insertedCount, value); }

        private int _deletedCount;
        public int DeletedCount { get => _deletedCount; set => SetProperty(ref _deletedCount, value); }

        private int _modifiedCount;
        public int ModifiedCount { get => _modifiedCount; set => SetProperty(ref _modifiedCount, value); }

        public ObservableCollection<CodeLineComparison> LineComparisons { get; set; } = new();

        public ICommand CompareCommand { get; }
        public IRelayCommand ImportLeftCommand { get; }
        public IRelayCommand ImportRightCommand { get; }

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel created");
            CompareCommand = new RelayCommand(CompareCodes);
            ImportLeftCommand = new RelayCommand(ImportLeftFile);
            ImportRightCommand = new RelayCommand(ImportRightFile);
        }

        private void CompareCodes()
        {
            LineComparisons.Clear();

            var builder = new SideBySideDiffBuilder(new DiffPlex.Differ());
            var diff = builder.BuildDiffModel(LeftCode ?? "", RightCode ?? "");

            int maxLines = Math.Max(diff.OldText.Lines.Count, diff.NewText.Lines.Count);

            SameCount = InsertedCount = DeletedCount = ModifiedCount = 0;

            for (int i = 0; i < maxLines; i++)
            {
                var oldLine = i < diff.OldText.Lines.Count ? diff.OldText.Lines[i] : null;
                var newLine = i < diff.NewText.Lines.Count ? diff.NewText.Lines[i] : null;

                if (oldLine?.Type == ChangeType.Unchanged && newLine?.Type == ChangeType.Unchanged)
                    SameCount++;
                else
                {
                    if (oldLine?.Type == ChangeType.Deleted)
                        DeletedCount++;
                    if (newLine?.Type == ChangeType.Inserted)
                        InsertedCount++;
                    if (newLine?.Type == ChangeType.Modified)
                        ModifiedCount++;
                }

                LineComparisons.Add(new CodeLineComparison
                {
                    LeftText = oldLine?.Text ?? "",
                    RightText = newLine?.Text ?? "",
                    LeftChangeType = oldLine?.Type ?? ChangeType.Imaginary,
                    RightChangeType = newLine?.Type ?? ChangeType.Imaginary
                });
            }
        }

        private void ImportLeftFile()
        {
            var content = OpenFile();
            if (content != null)
                LeftCode = content;
        }

        private void ImportRightFile()
        {
            var content = OpenFile();
            if (content != null)
                RightCode = content;
        }

        private string? OpenFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Code files (*.cs;*.txt;*.java;*.py)|*.cs;*.txt;*.java;*.py|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    return File.ReadAllText(dialog.FileName);
                }
                catch (Exception ex)
                {
                    // optionally handle errors (e.g., show a MessageBox)
                }
            }

            return null;
        }
    }
}
