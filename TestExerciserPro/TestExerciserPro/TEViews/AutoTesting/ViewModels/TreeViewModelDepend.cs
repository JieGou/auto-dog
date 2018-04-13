using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    public class TreeViewModelDepend
    {
        public static readonly DependencyProperty ItemImageNameProperty = DependencyProperty.RegisterAttached("ItemImageName", typeof(string), typeof(TreeViewModelDepend), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.RegisterAttached("IsLoading",
                                                                    typeof(bool), typeof(TreeViewModelDepend),
                                                                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty IsLoadedProperty = DependencyProperty.RegisterAttached("IsLoaded",
                                                                    typeof(bool), typeof(TreeViewModelDepend),
                                                                    new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty IsCanceledProperty = DependencyProperty.RegisterAttached("IsCanceled",
                                                                    typeof(bool), typeof(TreeViewModelDepend),
                                                                    new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty ItemTypeProperty = DependencyProperty.RegisterAttached("ItemTypeName", typeof(string), typeof(TreeViewModelDepend), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.RegisterAttached("IsInEditMode",
                                                                    typeof(bool), typeof(TreeViewModelDepend),
                                                                    new FrameworkPropertyMetadata(false));


        #region 附加属性：节点图片
        public static string GetItemImageName(DependencyObject obj)
        {
            return (string)obj.GetValue(ItemImageNameProperty);
        }

        public static void SetItemImageName(DependencyObject obj, string value)
        {
            obj.SetValue(ItemImageNameProperty, value);
        }
        #endregion

        #region 附加属性：是否正在加载
        public static bool GetIsLoading(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLoadingProperty);
        }

        public static void SetIsLoading(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLoadingProperty, value);
        }
        #endregion

        #region 附加属性：是否已加载
        public static bool GetIsLoaded(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLoadedProperty);
        }

        public static void SetIsLoaded(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLoadedProperty, value);
        }
        #endregion

        #region 附加属性：是否已取消加载
        public static bool GetIsCanceled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCanceledProperty);
        }

        public static void SetIsCanceled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCanceledProperty, value);
        }
        #endregion

        #region 附加属性：树节点类型
        public static string GetItemTypeName(DependencyObject obj)
        {
            return (string)obj.GetValue(ItemTypeProperty);
        }

        public static void SetItemTypeName(DependencyObject obj, string value)
        {
            obj.SetValue(ItemTypeProperty, value);
        }
        #endregion

        #region 附加属性：树节点中的文件节点是否被选中
        public static bool GetIsInEditMode(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsInEditModeProperty);
        }

        public static void SetIsInEditMode(DependencyObject obj, bool value)
        {
            obj.SetValue(IsInEditModeProperty, value);
        }
        #endregion

    }
}
