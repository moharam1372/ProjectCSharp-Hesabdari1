using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;

namespace MyCom.Object
{
    public partial class optimChart : UserControl
    {
        readonly XYDiagram _xyDiagram = new XYDiagram();

        // private Class_Font _font = new Class_Font(Class_Font.enumFont.samim);
        // Series _series = new Series();
        // readonly SwiftPlotDiagram _seriesLabel = new SideBySideBarSeriesLabel();
        readonly SideBySideBarSeriesLabel _seriesLabel = new SideBySideBarSeriesLabel();
        // DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
        // DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
        // readonly PointSeriesLabel _seriesLabel = new PointSeriesLabel();

        public optimChart()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            InitializeComponent();
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        public class modelSeries
        {
            public string[] nColumn { get; set; }
        }

        public class modelPointSeries
        {
            public Color Color { get; set; }
            public string Title { get; set; }
            public double[] Value { get; set; }
        }

        public void CreatedChart(modelSeries modelSeries, List<modelPointSeries> modelPointSeries, Font font)
        {
            #region Change Font


            chart2.SmallChartText.Font = font;
            chart2.EmptyChartText.Font = font;

            _xyDiagram.AxisX.CrosshairAxisLabelOptions.Font = font;
            _xyDiagram.AxisX.Label.Font = font;
            _xyDiagram.AxisX.Title.Font = font;
            _xyDiagram.AxisY.CrosshairAxisLabelOptions.Font = font;
            // _xyDiagram.AxisY.CrosshairAxisLabelOptions.Font = new Font("Samim FD", 12F,FontStyle.Regular, GraphicsUnit.Point, 0);
            _xyDiagram.AxisY.Title.Font = font;
            _xyDiagram.AxisY.Label.Font = font;

            _seriesLabel.Font = font;
            //  _seriesLabel.Font = new Font("Samim FD", 12F, FontStyle.Regular,GraphicsUnit.Point, 0);

            #endregion
            chart2.Series.Clear();
            // _xyDiagram.AxisX.CrosshairAxisLabelOptions.TextColor = Color.FromArgb(149, 55, 52);
            _xyDiagram.AxisX.GridLines.Visible = true;
            _xyDiagram.AxisX.Label.Angle = 330;
            trackBarControl1.Value = 330;

            _xyDiagram.AxisX.Tickmarks.MinorLength = 3;

            _xyDiagram.AxisX.VisibleInPanesSerializable = "-1";
            _xyDiagram.AxisX.VisualRange.Auto = false;
            _xyDiagram.AxisX.VisualRange.AutoSideMargins = false;
            // _xyDiagram.AxisX.VisualRange.MaxValueSerializable = "Value 5";
            // _xyDiagram.AxisX.VisualRange.MinValueSerializable = "Value 1";
            _xyDiagram.AxisX.VisualRange.SideMarginsValue = 0.5D;
            _xyDiagram.AxisY.MinorCount = 4;
            _xyDiagram.AxisY.VisibleInPanesSerializable = "-1";

            // _seriesLabel.Border.Color = Color.FromArgb(62, 130, 193);
            _seriesLabel.EnableAntialiasing = DefaultBoolean.True;
            _seriesLabel.FillStyle.FillMode = FillMode.Gradient;

            _seriesLabel.Indent = 10;
            _seriesLabel.LineLength = 3;
            _seriesLabel.LineStyle.DashStyle = DashStyle.DashDotDot;
            _seriesLabel.LineStyle.Thickness = 2;
            _seriesLabel.LineVisibility = DefaultBoolean.False;
            _seriesLabel.Position = BarSeriesLabelPosition.Top;
            _seriesLabel.Shadow.Color = Color.FromArgb(219, 229, 241);
            _seriesLabel.Shadow.Size = 1;
            _seriesLabel.Shadow.Visible = true;
            _seriesLabel.TextColor = Color.FromArgb(254, 21, 24);

            // _gradientFillOptions.Color2 = Color.Green;

            for (int i = 0; i < modelPointSeries.Count; i++)
            {
                Series _series = new Series { Name = modelPointSeries[i].Title, LegendText = modelPointSeries[i].Title };
                //  SwiftPlotSeriesView _seriesView = new SwiftPlotSeriesView { PaneName = "SB" + i };
                SideBySideBarSeriesView _seriesView = new SideBySideBarSeriesView { PaneName = "SB" + i };
                RectangleGradientFillOptions _gradientFillOptions = new RectangleGradientFillOptions();
                ((ISupportInitialize)_seriesView).BeginInit();

                _seriesView.Color = modelPointSeries[i].Color;
                // _seriesView.Color = Color.FromArgb(255, 0, 176, 240);
                _seriesView.FillStyle.FillMode = FillMode.Gradient;
                _gradientFillOptions.Color2 = Color.FromArgb(140, 233, 233, 233);
                _seriesView.FillStyle.Options = _gradientFillOptions;
                // _seriesView.Shadow.Color = Color.FromArgb(255, 255, 0);
                ((RectangleGradientFillOptions)_seriesView.FillStyle.Options).GradientMode = RectangleGradientMode.TopRightToBottomLeft;

                _series.View = _seriesView;
                chart2.Series.Add(_series);

                ((ISupportInitialize)_seriesView).EndInit();

                ((ISupportInitialize)_series).BeginInit();
                _series.Label = _seriesLabel;
                ((ISupportInitialize)_series).EndInit();

                _series.LabelsVisibility = DefaultBoolean.True;
                _series.Label.FillStyle.FillMode = FillMode.Gradient;
            }

            int j = 0;

            foreach (Series _series in chart2.Series)
            {
                foreach (var series in modelSeries.nColumn)
                    _series.Points.Add(new SeriesPoint(series));

                for (int i2 = 0; i2 < modelPointSeries[j].Value.Length; i2++)
                {
                    _series.Points[i2].Values = new[] { modelPointSeries[j].Value[i2] };
                }
                j++;
            }

            ((ISupportInitialize)chart2).BeginInit();
            chart2.Diagram = _xyDiagram;
            ((ISupportInitialize)chart2).EndInit();

            chart2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.LeftOutside;
            chart2.Legend.Visibility = DefaultBoolean.True;
            chart2.PaletteBaseColorNumber = 1;

            //chart2.Titles.AddRange(new ChartTitle[] { chartTitle4 });
        }

        private Series _nameSeries;
        private void chart2_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            //  if (e.AdditionalObject != null)
            //  MessageBox.Show(e.AdditionalObject.ToString());
            if (e.Object is Series series)
            {
                _nameSeries = series;

                var selSeries =
                    (RectangleGradientFillOptions)
                    ((SideBySideBarSeriesView)_nameSeries.View).FillStyle.Options;
                colorPickEdit1.Color = selSeries.Color2;


                var selSeries2 = (SideBySideBarSeriesView)_nameSeries.View;
                colorPickEdit2.Color = selSeries2.Color;
            }
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            _xyDiagram.AxisX.Label.Angle = trackBarControl1.Value;
        }

        private void colorPickEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (_nameSeries != null)
            {
                var selSeries =
                    (RectangleGradientFillOptions)
                    ((SideBySideBarSeriesView)_nameSeries.View).FillStyle.Options;
                selSeries.Color2 = colorPickEdit1.Color;
            }
        }

        private void colorPickEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (_nameSeries != null)
            {
                var selSeries = (SideBySideBarSeriesView)_nameSeries.View;
                selSeries.Color = colorPickEdit2.Color;
            }
        }

        #region Function Export
        private void mnuExportDocx_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = @"Document File|*.Docx",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                chart2.ExportToDocx(sfd.FileName, new DocxDocumentOptions
                {

                });
                Process.Start(sfd.FileName);
                //chart2.ExportToDocx();
            }

        }
        private void mnuExportExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = @"Sheet File|*.Excel",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                chart2.ExportToXlsx(sfd.FileName, new XlsxExportOptions(TextExportMode.Text, true, false, true));
                Process.Start(sfd.FileName);
                //chart2.ExportToDocx();
            }
        }

        private void mnuExportImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = @"Image File|*.jpg",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //chart2.ExportToImage("",ImageFormat.Tiff);
              //  var getCloneChart = (ChartControl) chart2.Clone();
                // getCloneChart.Size = new Size(chart2.Width * 2, chart2.Height * 2);
               // getCloneChart.ExportToImage(sfd.FileName, ImageFormat.Bmp);
                chart2.ExportToImage(sfd.FileName, ImageFormat.Bmp);
                //Process.Start(sfd.FileName);
                //chart2.ExportToDocx();
            }
        }

        private void mnuExportPDF_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = @"PDF File|*.pdf",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                chart2.ExportToPdf(sfd.FileName, new PdfExportOptions
                {
                    Compressed = false,
                    ConvertImagesToJpeg = false,
                    ImageQuality = PdfJpegImageQuality.High,
                    DocumentOptions = { Title = "Title", Subject = "Subject", Author = "Author" },
                    //  SignatureOptions = { }
                    PdfACompatibility = PdfACompatibility.None
                });
                Process.Start(sfd.FileName);

            }
        }

        private void mnuExportMHT_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = @"MHT File|*.mht",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                chart2.ExportToMht(sfd.FileName, new MhtExportOptions());
                Process.Start(sfd.FileName);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            mnuChart.ShowPopup(new Point(MousePosition.X, MousePosition.Y));
        }


        #endregion


    }
}
