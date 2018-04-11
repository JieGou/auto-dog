﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestExerciserPro.TEViews.AutoTesting.ViewModels
{
    public class TreeViewModel:DependencyObject
    {
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

        #region 附加属性：是否显示“打开资源管理器对应文件夹”
        public static Visibility GetMenuVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(MenuVisibilityProperty);
        }

        public static void SetMenuVisibility(DependencyObject obj, Enum value)
        {
            obj.SetValue(MenuVisibilityProperty, value);
        }
        #endregion

        public static readonly DependencyProperty ItemImageNameProperty;
        public static readonly DependencyProperty IsLoadingProperty;
        public static readonly DependencyProperty IsLoadedProperty;
        public static readonly DependencyProperty IsCanceledProperty;
        public static readonly DependencyProperty ItemTypeProperty;
        public static readonly DependencyProperty MenuVisibilityProperty;


        static TreeViewModel()
        {
            ItemImageNameProperty = DependencyProperty.RegisterAttached("ItemImageName", typeof(string), typeof(TreeViewModel), new UIPropertyMetadata(string.Empty));

            IsLoadingProperty = DependencyProperty.RegisterAttached("IsLoading",
                                                                    typeof(bool), typeof(TreeViewModel),
                                                                    new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

            IsLoadedProperty = DependencyProperty.RegisterAttached("IsLoaded",
                                                                    typeof(bool), typeof(TreeViewModel),
                                                                    new FrameworkPropertyMetadata(false));

            IsCanceledProperty = DependencyProperty.RegisterAttached("IsCanceled",
                                                                    typeof(bool), typeof(TreeViewModel),
                                                                    new FrameworkPropertyMetadata(false));

            ItemTypeProperty = DependencyProperty.RegisterAttached("ItemTypeName", typeof(string), typeof(TreeViewModel), new UIPropertyMetadata(string.Empty));

            MenuVisibilityProperty = DependencyProperty.Register("MenuVisibility",
                                                                    typeof(Visibility), typeof(TreeViewModel),
                                                                    new FrameworkPropertyMetadata(Visibility.Hidden));
        }
    }
}
