using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;
using AutoDog.ViewModels;

namespace AutoDog
{
    class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }


        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }
        public DataTemplate APIFileViewTemplate
        {
            get;
            set;
        }

        public DataTemplate FileStatsViewTemplate
        {
            get;
            set;
        }

        public DataTemplate SolutionViewTemplate
        {
            get;
            set;
        }

        public DataTemplate ClassViewTemplate
        {
            get;
            set;
        }

        public DataTemplate ErrorListViewTemplate
        {
            get;
            set;
        }

        public DataTemplate OutPutViewTemplate
        {
            get;
            set;
        }

        public DataTemplate ResourcesViewTemplate
        {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is FileViewModel)
                return FileViewTemplate;

            if (item is FileStatsViewModel)
                return FileStatsViewTemplate;

            if (item is SolutionViewModel)
                return SolutionViewTemplate;

            if (item is ClassViewModel)
                return ClassViewTemplate;

            if (item is OutPutViewModel)
                return OutPutViewTemplate;

            if (item is ErrorListViewModel)
                return ErrorListViewTemplate;

            if (item is ResourcesViewModel)
                return ResourcesViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
