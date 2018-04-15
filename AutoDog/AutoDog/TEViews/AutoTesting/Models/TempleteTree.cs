using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;


namespace TestExerciserPro.IViews.AutoTesting.Models
{
    public class ProjectType : INotifyPropertyChanged
    {
        private int _projectTypeId;
        private int _templateId;
        private string _title;
        private string _describe;
        private TemplateType _template;

        public int ProjectTypeId
        {
            get { return _projectTypeId; }
            set
            {
                if (value == _projectTypeId) return;
                _projectTypeId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("TemplateType")]
        public int TemplateId
        {
            get { return _templateId; }
            set
            {
                if (value == _templateId) return;
                _templateId = value;
                OnPropertyChanged();
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value == this.isSelected) return;
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Describe
        {
            get { return _describe; }
            set
            {
                if (value == _describe) return;
                _describe = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("ProjectType Art URL")]
        public string ProjectTypeArtUrl { get; set; }


        public virtual TemplateType TemplateType
        {
            get { return _template; }
            set
            {
                if (Equals(value, _template)) return;
                _template = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TemplateType : INotifyPropertyChanged
    {
        private int _templateTypeId;
        private string _name;
        private List<ProjectType> _projectTypes;

        public int TemplateTypeId
        {
            get { return _templateTypeId; }
            set
            {
                if (value == _templateTypeId) return;
                _templateTypeId = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }


        public List<ProjectType> ProjectTypes
        {
            get { return _projectTypes; }
            set
            {
                if (Equals(value, _projectTypes)) return;
                _projectTypes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SolutionType : INotifyPropertyChanged
    {
        private int _solutionTypeId;
        private string _name;
        private string _title;

        public int SolutionTypeId
        {
            get { return _solutionTypeId; }
            set
            {
                if (value == _solutionTypeId) return;
                _solutionTypeId = value;
                OnPropertyChanged();
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value == this.isSelected) return;
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class TempleteTree
    {
        public static List<ProjectType> ProjectTypes { get; set; }
        public static List<TemplateType> TemplateTypes { get; set; }
        public static List<SolutionType> SolutionTypes { get; set; }
        public static List<LocationMap> LocationMaps { get; set; }
        public static void Seed()
        {
            SolutionTypes = new List<SolutionType>
            {
                new SolutionType {Title ="创建新解决方案"},
                new SolutionType {Title ="添加到解决方案"},
                new SolutionType {Title ="在新实例中创建"},
            };

            TemplateTypes = new List<TemplateType>
            {
                new TemplateType { Name = "Win32应用程序" },
                new TemplateType { Name = "Web应用程序" },
                new TemplateType { Name = "移动端应用程序" },
                new TemplateType { Name = "服务器端应用程序" },
                new TemplateType { Name = "其他类型" },
                new TemplateType { Name = "联机模板" },
            };

            int i = 0;
            TemplateTypes.ForEach(x => x.TemplateTypeId = ++i);

            ProjectTypes = new List<ProjectType>
                {
                    new ProjectType {Title = "Python", Describe = "Python测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Win32应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "C#", Describe = "C#测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Win32应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Java", Describe = "Java测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Win32应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Ruby", Describe = "Ruby测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Win32应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Python", Describe = "Python测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Web应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "C#", Describe = "C#测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Web应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Java", Describe = "Java测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Web应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                     new ProjectType {Title = "Ruby", Describe = "Ruby测试工程", TemplateType = TemplateTypes.First(a => a.Name == "Web应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Python", Describe = "Python测试工程", TemplateType = TemplateTypes.First(a => a.Name == "移动端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "C#", Describe = "C#测试工程", TemplateType = TemplateTypes.First(a => a.Name == "移动端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Java", Describe = "Java测试工程", TemplateType = TemplateTypes.First(a => a.Name == "移动端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Ruby", Describe = "Ruby测试工程", TemplateType = TemplateTypes.First(a => a.Name == "移动端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Python", Describe = "Python测试工程", TemplateType = TemplateTypes.First(a => a.Name == "服务器端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "C#", Describe = "C#测试工程", TemplateType = TemplateTypes.First(a => a.Name == "服务器端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                     new ProjectType {Title = "Java", Describe = "Java测试工程", TemplateType = TemplateTypes.First(a => a.Name == "服务器端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Ruby", Describe = "Ruby测试工程", TemplateType = TemplateTypes.First(a => a.Name == "服务器端应用程序"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Python", Describe = "Python测试工程", TemplateType = TemplateTypes.First(a => a.Name == "其他类型"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "C#", Describe = "C#测试工程", TemplateType = TemplateTypes.First(a => a.Name == "其他类型"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Java", Describe = "Java测试工程", TemplateType = TemplateTypes.First(a => a.Name == "其他类型"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Ruby", Describe = "Ruby测试工程", TemplateType = TemplateTypes.First(a => a.Name == "其他类型"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},

                    new ProjectType {Title = "Win32应用程序", Describe = "Win32应用程序", TemplateType = TemplateTypes.First(a => a.Name == "联机模板"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "Web应用程序", Describe = "Web应用程序", TemplateType = TemplateTypes.First(a => a.Name == "联机模板"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "移动端应用程序", Describe = "移动端应用程序", TemplateType = TemplateTypes.First(a => a.Name == "联机模板"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                    new ProjectType {Title = "服务器端应用程序", Describe = "服务器端应用程序", TemplateType = TemplateTypes.First(a => a.Name == "联机模板"), ProjectTypeArtUrl = "/Content/Images/placeholder.gif"},
                };

            var albumsGroupedByTemplate = ProjectTypes.GroupBy(a => a.TemplateType);
            foreach (var grouping in albumsGroupedByTemplate)
            {
                grouping.Key.ProjectTypes = grouping.ToList();
            }
        }
    }
}
