using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace FlexText
{
    public partial class DotedViewBox : SKCanvasView
    {
        public DotedViewBox()
        {
            InitializeComponent();
            PaintSurface += DotedViewBox_OnPaintSurface;
        }

        #region -- Public properties --

        public static BindableProperty DotedColorProperty = BindableProperty.Create("DotedColor", typeof(Color), typeof(DotedViewBox), Color.Gray);
        public Color DottedColor
        {
            get => (Color)GetValue(DotedColorProperty);
            set => SetValue(DotedColorProperty, value);
        }

        public static BindableProperty ColorProperty = BindableProperty.Create("Color", typeof(Color), typeof(DotedViewBox), Color.White);
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private void DotedViewBox_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint dotsPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = DottedColor.ToSKColor(),
                StrokeWidth = 10,
                StrokeCap = SKStrokeCap.Butt,
                PathEffect = SKPathEffect.CreateDash(new float[] { 10, 10 }, 20)
            };

            SKPaint viwBoxPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.ToSKColor()
            };

            SKPath path = new SKPath();
            path.MoveTo(0, 0);
            path.LineTo(0, info.Height);
            path.LineTo(info.Width, info.Height);
            path.LineTo(info.Width, 0);
            path.LineTo(0, 0);


            canvas.DrawPath(path, viwBoxPaint);
            canvas.DrawPath(path, dotsPaint);
        }

        #endregion

    }
}
