using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlexText
{
    public partial class HiddenLabel : ContentView
    {

        public HiddenLabel()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(HiddenLabel), "", BindingMode.TwoWay);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color ActiveColor { get; set; }

        public static BindableProperty HiddenColorProperty = BindableProperty.Create("HiddenColor", typeof(Color), typeof(HiddenLabel), Color.DarkGray, BindingMode.TwoWay);
        public Color HiddenColor
        {
            get => (Color)GetValue(HiddenColorProperty);
            set => SetValue(HiddenColorProperty, value);
        }

        public static BindableProperty IsHiddenProperty = BindableProperty.Create("IsHidden", typeof(bool), typeof(HiddenLabel), true, BindingMode.TwoWay);
        public bool IsHidden
        {
            get => (bool)GetValue(IsHiddenProperty);
            set => SetValue(IsHiddenProperty, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                HiddenColor = _isActive ? ActiveColor : HiddenColor;
            }
        }

        #endregion
    }
}
