using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace FlexText
{
    public partial class FlexTextContentView : ContentView
    {

        public FlexTextContentView()
        {
            InitializeComponent();
            this.GetType();
        }

        #region -- Public properties --

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(FlexTextContentView), "", propertyChanged: HandleBindingPropertyChangedDelegate);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region -- Private helpers --

        public void ParseAndShow(string str)
        {
            LabelContainer.Children.Clear();
            string temp = "";

            foreach (char ch in str)
            {
                if (ch.Equals(' '))
                {
                    Label nextLabel = new Label { Text = temp, HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center };
                    LabelContainer.Children.Add(nextLabel);
                    Label spaceLabel = new Label { Text = " ", HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center };
                    LabelContainer.Children.Add(spaceLabel);
                    temp = "";
                    continue;
                }
                if (ch.Equals('['))
                {
                    Label nextLabel = new Label { Text = temp, HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center };
                    LabelContainer.Children.Add(nextLabel);
                    temp = "";
                    temp += ch;
                    continue;
                }
                if (ch.Equals(']'))
                {
                    temp += ch;
                    HiddenLabel nextLabel = new HiddenLabel { Text = temp, IsHidden = true, HiddenColor = Color.Azure, ActiveColor = Color.Bisque, HorizontalOptions = LayoutOptions.Center };
                    LabelContainer.Children.Add(nextLabel);
                    temp = "";
                    continue;
                }
                if (ch.Equals('\n'))
                {
                    Label nextLabel = new Label { Text = temp, HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center };
                    LabelContainer.Children.Add(nextLabel);
                    Label newLineLabel = new Label { Text = "", FontSize = 0, HeightRequest = 0 };
                    LabelContainer.Children.Add(newLineLabel);
                    FlexLayout.SetBasis(newLineLabel, new FlexBasis(1, true));
                    temp = "";
                    continue;
                }
                temp += ch;
            }

            LabelContainer.Children.Add(new Label { Text = temp, HorizontalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Center });

            foreach (View maybeHiddenLabel in LabelContainer.Children)
            {
                if (maybeHiddenLabel is HiddenLabel)
                {
                    (maybeHiddenLabel as HiddenLabel).IsActive = true;
                    if ((maybeHiddenLabel as HiddenLabel).Text.Last().Equals('\n'))
                        continue;
                    break;
                }
            }

        }

        #endregion

        #region -- Private static methods --

        private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is FlexTextContentView))
                return;
            FlexTextContentView thisControll = bindable as FlexTextContentView;

            thisControll.ParseAndShow(newValue as string);
        }

        #endregion

    }
}
