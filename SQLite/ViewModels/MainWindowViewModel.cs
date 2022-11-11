using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using MaterialDesignThemes.Wpf;
using System.Linq;
using SQLiteTool.Views;

namespace SQLiteTool.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
    {
        ToolItems = new ObservableCollection<ToolItem>();
        foreach (var item in GenerateToolItems(snackbarMessageQueue).OrderBy(i => i.Name))
        {
            ToolItems.Add(item);
        }
        SelectedItem = ToolItems.First();
        _toolItemsView = CollectionViewSource.GetDefaultView(ToolItems);


        HomeCommand = new AnotherCommandImplementation(
            _ =>
            {
                SelectedIndex = 0;
            });

        MovePrevCommand = new AnotherCommandImplementation(
            _ =>
            {
                SelectedIndex--;
            },
            _ => SelectedIndex > 0);

        MoveNextCommand = new AnotherCommandImplementation(
           _ =>
           {
               SelectedIndex++;
           },
           _ => SelectedIndex < ToolItems.Count - 1);
    }
    private readonly ICollectionView _toolItemsView;
    private ToolItem? _selectedItem;
    private int _selectedIndex;
    private bool _controlsEnabled = true;
    public ObservableCollection<ToolItem> ToolItems { get; }


    public ToolItem? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
    }

    public bool ControlsEnabled
    {
        get => _controlsEnabled;
        set => SetProperty(ref _controlsEnabled, value);
    }
    public AnotherCommandImplementation HomeCommand { get; }
    public AnotherCommandImplementation MovePrevCommand { get; }
    public AnotherCommandImplementation MoveNextCommand { get; }


    private static IEnumerable<ToolItem> GenerateToolItems(ISnackbarMessageQueue snackbarMessageQueue)
    {
        if (snackbarMessageQueue is null)
            throw new ArgumentNullException(nameof(snackbarMessageQueue));



        yield return new ToolItem(
            "Color Tool",
            typeof(ColorTool),
            null
            );
        yield return new ToolItem(
            "CMSTool",
            typeof(CMSTool),
            null
            );
        yield return new ToolItem(
            "BMSTool",
            typeof(BMSTool),
            null
            );
        yield return new ToolItem(
            "TMSTool",
            typeof(TMSTool),
            null
            );

    }

}
