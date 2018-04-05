using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    class TreeViewModel
    {
        public string Icon { get; set; }

        public string EditIcon { get; set; }

        public string DisplayName { get; set; }

        public string Text { get; set; }

        public bool IsModify { get; set; }

        public bool IsChecked { get; set; }

        public string Tag { get; set; }

        public string IsDeleted { get; set; }

        public string FileType { get; set; }

        public string FolderPath { get; set; }
        public TreeViewItem Child { get; set; }

        public TreeViewModel()
        {
            
        }


        private ObservableCollection<TreeViewModel> _children;
        public ObservableCollection<TreeViewModel> Children
        {
            get { return (_children ?? (_children = new ObservableCollection<TreeViewModel>())); }
        }
    }

    public static class TreeViewItemProps
    {
        public static string GetItemImageName(DependencyObject obj)
        {
            return (string)obj.GetValue(ItemImageNameProperty);
        }

        public static void SetItemImageName(DependencyObject obj, string value)
        {
            obj.SetValue(ItemImageNameProperty, value);
        }

        public static bool GetIsLoading(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLoadingProperty);
        }

        public static void SetIsLoading(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLoadingProperty, value);
        }

        public static bool GetIsLoaded(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLoadedProperty);
        }

        public static void SetIsLoaded(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLoadedProperty, value);
        }

        public static bool GetIsCanceled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCanceledProperty);
        }

        public static void SetIsCanceled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCanceledProperty, value);
        }

        public static readonly DependencyProperty ItemImageNameProperty;
        public static readonly DependencyProperty IsLoadingProperty;
        public static readonly DependencyProperty IsLoadedProperty;
        public static readonly DependencyProperty IsCanceledProperty;

        static TreeViewItemProps()
        {
            ItemImageNameProperty = DependencyProperty.RegisterAttached("ItemImageName", typeof(string), typeof(TreeViewItemProps), new UIPropertyMetadata(string.Empty));

            IsLoadingProperty = DependencyProperty.RegisterAttached("IsLoading",
                                                                    typeof(bool), typeof(TreeViewItemProps),
                                                                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

            IsLoadedProperty = DependencyProperty.RegisterAttached("IsLoaded",
                                                                    typeof(bool), typeof(TreeViewItemProps),
                                                                    new FrameworkPropertyMetadata(false));

            IsCanceledProperty = DependencyProperty.RegisterAttached("IsCanceled",
                                                                    typeof(bool), typeof(TreeViewItemProps),
                                                                    new FrameworkPropertyMetadata(false));
        }
    }
}
