# ColorFontPickerWPF
Color picker and font picker add-ons built for WPF.     
Includes **ColorDialog**, **ColorPickerControl**, **FontDialog**, **FontPickerControl**.    
Dialogs follow Winform design, controls can be added directly in xaml and support collapsing.     
The interface displays Chinese or English depending on the thread.    

为WPF打造的颜色选择器和字体选择器附加功能。    
包括颜色对话框、颜色选择控件、字体对话框、字体选择控件。    
对话框沿用Winform的设计，控件可以直接在xaml中插入并支持折叠。    
界面根据线程显示中文或英文。      
      
[![release](https://img.shields.io/static/v1?label=release&message=1.0.0&color=green&logo=github)](https://github.com/tp1415926535/ColorFontPickerWPF/releases) 
[![nuget](https://img.shields.io/static/v1?label=nuget&message=1.0.0&color=lightblue&logo=nuget)](https://www.nuget.org/packages/ColorFontPickerWPF) 
[![license](https://img.shields.io/static/v1?label=license&message=MIT&color=silver)](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/LICENSE) 
![C#](https://img.shields.io/github/languages/top/tp1415926535/ColorFontPickerWPF)        

[![Description](https://img.shields.io/static/v1?label=English&message=Description&color=yellow)](https://github.com/tp1415926535/ColorFontPickerWPF#english-description) 
[![说明](https://img.shields.io/static/v1?label=中文&message=说明&color=red)](https://github.com/tp1415926535/ColorFontPickerWPF#中文说明)      
      
# English Description
## Usage   
Download package from Nuget, or using the release Dll.   
  
### Color
#### ColorDialog
Just like winform's way:   
```c#
using ColorFontPickerWPF;

ColorDialog colorDialog = new ColorDialog();
//colorDialog.SelectedColor = ((SolidColorBrush)label.Background).Color; //In need
if (colorDialog.ShowDialog() == true)
    label.Background = new SolidColorBrush(colorDialog.SelectedColor);
```

#### ColorPickerControl  
Additional Property **"SelectedColor"** can be get and set.    
Additional Property **"WithoutColorCells"** can collapse the left interface when True.   

* The xaml way     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:ColorPickerControl Width="auto" Height="auto" SelectedColor="Blue" WithoutColorCells="False"/>
```

* The C# way     
```c#
using ColorFontPickerWPF;

var colorPicker = new ColorPickerControl();
//colorPicker.SelectedColor = Colors.Red; //In need
grid.Children.Add(colorPicker);
```

### Font
#### FontDialog
To make it easier to use, methods to get and set fonts are provided:
```c#
using ColorFontPickerWPF;

FontDialog fontDialog = new FontDialog();
//fontDialog.GetFont(textBlock); //In need
if (fontDialog.ShowDialog() == true)
    fontDialog.SetFont(textBlock);
```

#### FontPickerControl  
Additional Property **"SelectedFont"** can be get and set.    
Additional Property **"WithoutDecorations"** and **"WithoutPreviewRow"** can collapse corresponding area when True.    

* The xaml way     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:FontPickerControl Width="auto" Height="auto" WithoutDecorations="False" WithoutPreviewRow="False"/>
```

* The C# way     
```c#
using ColorFontPickerWPF;

var fontPicker = new FontPickerControl();
/*//In need
fontPicker.SelectedFont = new Font()
{ 
    FontFamily = new FontFamily("Microsoft YaHei UI"), 
    FamilyTypeface = new FamilyTypeface(),
    FontSize = 12
}; 
//fontPicker.Get(textBlock);// or get font from control
*/
grid.Children.Add(fontPicker);
```


## Additional information
* ColorPicker reference Winform design, in addition to RGB and HSL, **HEX** format has been added, and added a **full screen color picking** function.
* **SelectedFont** is an instance of the **"Font"** class. It is structured as follows:
```c#
public class Font
{
    public FontFamily FontFamily { get; set;}
    public FamilyTypeface FamilyTypeface { get; set;}       
    public TextDecorationType TextDecorationType { get; set;}
    public double FontSize { get; set;}
}

public enum TextDecorationType
{
    None,
    OverLine,
    Strikethrough,
    Baseline,
    Underline,
}
``` 
You can handle **SelectedFont** yourself, or use a wrapped method to "fontDialog.GetFont()", "fontPickerControl.GetFont()",
or "fontDialog.SetFont()", "fontPickerControl.SetFont()" directly.
   
     
---
   
# 中文说明

## 使用   
从Nuget下载包，或者引用Release中的dll。   

### 颜色
#### 颜色对话框（ColorDialog）

跟 Winform 一样调用:
```c#
using ColorFontPickerWPF;

ColorDialog colorDialog = new ColorDialog();
//colorDialog.SelectedColor = ((SolidColorBrush)label.Background).Color; //In need
if (colorDialog.ShowDialog() == true)
    label.Background = new SolidColorBrush(colorDialog.SelectedColor);
```

#### 颜色选择控件（ColorPickerControl） 
附加属性“SelectedColor”（选择的颜色）可以设置和获取。   
附加属性“WithoutColorCells”（不要颜色格子）为True的时候可以折叠左边部分。    

* 用 xaml 的方式     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:ColorPickerControl Width="auto" Height="auto" SelectedColor="Blue" WithoutColorCells="False"/>
```

* 用 C# 的方式     
```c#
using ColorFontPickerWPF;

var colorPicker = new ColorPickerControl();
//colorPicker.SelectedColor = Colors.Red; //In need
grid.Children.Add(colorPicker);
```

### 字体
#### 字体对话框
为了更便于使用，字体提供了获取和设置的封装方法：
```c#
using ColorFontPickerWPF;

FontDialog fontDialog = new FontDialog();
//fontDialog.GetFont(textBlock); //In need
if (fontDialog.ShowDialog() == true)
    fontDialog.SetFont(textBlock);
```

#### 字体选择控件（FontPickerControl） 
附加属性 “SelectedFont”（选择的字体）可以获取和设置。    
附加属性 “WithoutDecorations”（不要装饰线设置）和 “WithoutPreviewRow” （不要预览行）值为True的时候可以折叠对应区域。     

* 用 xaml 的方式      
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:FontPickerControl Width="auto" Height="auto" WithoutDecorations="False" WithoutPreviewRow="False"/>
```

* 用 C# 的方式     
```c#
using ColorFontPickerWPF;

var fontPicker = new FontPickerControl();
/*//In need
fontPicker.SelectedFont = new Font()
{ 
    FontFamily = new FontFamily("Microsoft YaHei UI"), 
    FamilyTypeface = new FamilyTypeface(),
    FontSize = 12
}; 
//fontPicker.Get(textBlock);// or get font from control
*/
grid.Children.Add(fontPicker);
```


## 补充说明
* 颜色选择器沿用Winform的设计，处理RGB、HSL之外还增加了HEX格式，另外还有全屏取色的功能。
* “SelectedFont” （选择的字体）是 “Font” 类的实例，它的结构如下：
```c#
public class Font
{
    public FontFamily FontFamily { get; set;}
    public FamilyTypeface FamilyTypeface { get; set;}       
    public TextDecorationType TextDecorationType { get; set;}
    public double FontSize { get; set;}
}

public enum TextDecorationType
{
    None,
    OverLine,
    Strikethrough,
    Baseline,
    Underline,
}
``` 
你可以自行处理 “SelectedFont” 的值，或者直接使用封装的方法"fontDialog.GetFont()", "fontPickerControl.GetFont()",
or "fontDialog.SetFont()", "fontPickerControl.SetFont()"。