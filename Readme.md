<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128641898/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4957)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
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


