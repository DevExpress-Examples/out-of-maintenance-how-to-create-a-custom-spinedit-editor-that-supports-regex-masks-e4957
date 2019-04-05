<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/CustomSpinEdit/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/CustomSpinEdit/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/CustomSpinEdit/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/CustomSpinEdit/MainWindow.xaml.vb))
<!-- default file list end -->
# How to create a custom SpinEdit editor that supports RegEx masks


<p>Currently, our numeric masks do not provide the capability to hide a decimal point even if your editor's value is integer. This example demonstrates how to create a custom SpinEdit editor that uses <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument1501"><u>RegEx</u></a> masks instead of Numeric and provides the following capabilities:</p><br />
<p>1. You can hide a decimal point if you like. For this, don't specify it in your mask.</p><p>2. If you set the editor's Mask property accordingly, it will accept only number values. To learn more about RegEx masks, refer to the <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument1501"><u>Mask Type: Extended Regular Expressions</u></a> help topic.</p><p>3. It's also possible to configure max and min values. Set the editor's Mask property as required.</p><p>4. You can use the editor's buttons and Up/Down keys to increase/decrease the editor's value.</p><br />
<p>To make this editor work, you will need to set its Mask property so that a decimal point symbol will be the same as in your system settings.</p>

<br/>


