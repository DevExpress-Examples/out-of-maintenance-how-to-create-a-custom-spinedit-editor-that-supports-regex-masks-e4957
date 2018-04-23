using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using DevExpress.Xpf.Editors;
using System.Threading;

namespace CustomSpinEdit {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {
        public MainWindow() {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            DoubleValue = 112.25;
            DataContext = this;
            InitializeComponent();
        }
        double _DoubleValue;
        public double DoubleValue { get { return _DoubleValue; } set { _DoubleValue = value; Debug.WriteLine(value); } }
        int _IntegerValue;
        public int IntegerValue { get { return _IntegerValue; } set { _IntegerValue = value; Debug.WriteLine(value); } }
    }

    public class MySpinEdit: ButtonEdit {
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(double), typeof(MySpinEdit), new PropertyMetadata(1.0));
        public static readonly DependencyProperty DecimalValueProperty =
            DependencyProperty.Register("DecimalValue", typeof(object), typeof(MySpinEdit), new PropertyMetadata(null, OnDecimalValuePropertyChanged));
        public static readonly DependencyProperty ValueTypeProperty =
            DependencyProperty.Register("ValueType", typeof(Type), typeof(MySpinEdit), new PropertyMetadata(typeof(double)));

        public Type ValueType {
            get { return (Type)GetValue(ValueTypeProperty); }
            set { SetValue(ValueTypeProperty, value); }
        }

        static MySpinEdit() {
            MaskTypeProperty.OverrideMetadata(typeof(MySpinEdit), new FrameworkPropertyMetadata(MaskType.RegEx));
            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(MySpinEdit), new FrameworkPropertyMetadata(HorizontalAlignment.Right));
            MaskUseAsDisplayFormatProperty.OverrideMetadata(typeof(MySpinEdit), new FrameworkPropertyMetadata(true));
            AllowDefaultButtonProperty.OverrideMetadata(typeof(MySpinEdit), new FrameworkPropertyMetadata(false));
            EditValueProperty.OverrideMetadata(typeof(MySpinEdit), new FrameworkPropertyMetadata(OnEditValuePropertyChanged));
        }
        static void OnDecimalValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((MySpinEdit)d).OnDecimalValueChanged(e);
        }
        static void OnEditValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((MySpinEdit)d).OnEditValueChanged(e);
        }

        public double Increment {
            get { return (double)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public object DecimalValue {
            get { return (object)GetValue(DecimalValueProperty); }
            set { SetValue(DecimalValueProperty, value); }
        }

        public MySpinEdit() {
            SpinButtonInfo buttons = new SpinButtonInfo();
            buttons.SpinUpClick += OnSpinUpClick;
            buttons.SpinDownClick += OnSpinDownClick;
            Buttons.Add(buttons);
        }
        void OnSpinUpClick(object sender, RoutedEventArgs e) {
            Increase();
        }
        void OnSpinDownClick(object sender, RoutedEventArgs e) {
            Decrease();
        }
        protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up)
                Increase();
            else if (e.Key == System.Windows.Input.Key.Down)
                Decrease();
            else
                base.OnPreviewKeyDown(e);
        }
        public void Increase() {
            Debug.WriteLine("Up");
            SpinCore(true);
        }
        public void Decrease() {
            Debug.WriteLine("Down");
            SpinCore(false);
        }
        void SpinCore(bool up) {
            if (ValueType == typeof(int)) {
                int integerIncrement = Convert.ToInt32(Increment);
                DecimalValue = up ? (int)DecimalValue + integerIncrement : (int)DecimalValue - integerIncrement;
            }
            if (ValueType == typeof(double))
                DecimalValue = up ? (double)DecimalValue + Increment : (double)DecimalValue - Increment;
        }
        void OnDecimalValueChanged(DependencyPropertyChangedEventArgs e) {
            if (DecimalValue is int)
                EditValue = ((int)DecimalValue).ToString(Thread.CurrentThread.CurrentUICulture);
            else if (DecimalValue is double)
                EditValue = ((double)DecimalValue).ToString(Thread.CurrentThread.CurrentUICulture);
            else
                EditValue = DecimalValue == null ? null : DecimalValue.ToString();
        }
        void OnEditValueChanged(DependencyPropertyChangedEventArgs e) {
            if (!string.IsNullOrEmpty(EditValue as string)) {
                if (ValueType == typeof(int)) {
                    try {
                        int value = Convert.ToInt32(EditValue, Thread.CurrentThread.CurrentUICulture);
                        if (!value.Equals(DecimalValue))
                            DecimalValue = value;
                        return;
                    }
                    catch { }
                }
                if (ValueType == typeof(double)) {
                    try {
                        double value = Convert.ToDouble(EditValue, Thread.CurrentThread.CurrentUICulture);
                        if (!value.Equals(DecimalValue))
                            DecimalValue = value;
                        return;
                    }
                    catch { }
                }
            } DecimalValue = null;
        }
    }
}