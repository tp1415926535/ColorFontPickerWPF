# ColorFontPickerWPF
Color picker and font picker add-ons built for WPF.     
Includes **ColorDialog**, **ColorPickerControl**, **ColorPickerPopup**, **FontDialog**, **FontPickerControl**, **FontPickerPopup** .      
Dialogs follow Winform layout, controls can be added directly in xaml and support collapsing.      
The interface automatically displays the corresponding language according to the threads, the localization includes 14 languages.     

为WPF打造的颜色选择器和字体选择器附加功能。    
包括颜色对话框、颜色选择控件、颜色选择下拉、字体对话框、字体选择控件、字体选择下拉。    
对话框沿用Winform的布局，控件可以直接在xaml中插入并支持折叠。    
界面根据线程自动显示对应语言，本地化包括14种语言。   
    
ColorDialog:    
![ColorDialog](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorDialog.jpg)       
     
ColorPickerPopup:      
![ColorPickerPopup](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerPopup2.jpg)      
     
FontDialog:     
![FontDialog](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontDialog.jpg)      
     
FontPickerPopup:     
![FontPickerPopup](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerPopup1.jpg)      
      
[![release](https://img.shields.io/github/v/release/tp1415926535/ColorFontPickerWPF?color=green&logo=github)](https://github.com/tp1415926535/ColorFontPickerWPF/releases) 
[![nuget](https://img.shields.io/nuget/v/ColorFontPickerWPF?color=lightblue&logo=nuget)](https://www.nuget.org/packages/ColorFontPickerWPF) 
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
ColorPickerControl-Style1:    
![ColorPickerControl](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerControl.jpg)  
     
ColorPickerControl-Style2:    
![ColorPickerControl2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerControl2.jpg)      

Additional Property **`SelectedColor`** can be get and set.    
Additional Property **`WithoutColorCells`** can collapse the left interface when True.   

* The xaml way     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:ColorPickerControl Width="auto" Height="auto" SelectedColor="Blue" WithoutColorCells="False"/>
```
v2.0 support for `ValueChanged` value change events, variable bindings, and Command (triggered on value change) bindings.
```xml
<cf:ColorPickerControl Width="auto" Height="auto" 
    SelectedColor="{Binding SelectedColor}" 
    ValueChanged="colorPicker_ValueChanged" 
    Command="{Binding ColorChangeCommand}"/>
```
* The C# way     
```c#
using ColorFontPickerWPF;

var colorPicker = new ColorPickerControl();
//colorPicker.SelectedColor = Colors.Red; //In need
grid.Children.Add(colorPicker);
```

#### ColorPickerPopup  

![ColorPickerPopup](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerPopup.jpg)  
     
Additional Property **`SelectedColor`** can be get and set.    
Additional Property **`ColorText`** can show current color value , support 'None','RGB','HEX','HSL' .      

ColorText = "RGB":    
![ColorPickerPopup-text1](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerPopup1.jpg)     

ColorText = "HEX":    
![ColorPickerPopup-text2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerPopup2.jpg)     

ColorText = "HSL":      
![ColorPickerPopup-text3](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/ColorPickerPopup3.jpg)     
       

* The xaml way     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:ColorPickerPopup SelectedColor="Yellow" ColorText="RGB"/>
```

v2.0 support for `ValueChanged` value change events, variable bindings, and Command (triggered on value change) bindings.

```xml
<cf:ColorPickerPopup SelectedColor="{Binding SelectedColor}" 
    ValueChanged="colorPicker_ValueChanged" 
    Command="{Binding ColorChangeCommand}"/>
```

* The C# way     

```c#
using ColorFontPickerWPF;

var colorPicker = new ColorPickerPopup();
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
FontPickerControl-style1:     
![FontPickerControl](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerControl.jpg)  
     
FontPickerControl-style2:     
![FontPickerControl2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerControl2.jpg)  
     
FontPickerControl-style3:     
![FontPickerControl3](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerControl3.jpg)     

Additional Property **`SelectedFont`** can be get and set.    
Additional Property **`WithoutDecorations`** and **`WithoutPreviewRow`** can collapse corresponding area when True.    

* The xaml way     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:FontPickerControl Width="auto" Height="auto" WithoutDecorations="False" WithoutPreviewRow="False"/>
```
v2.0 support for `ValueChanged` value change events, variable bindings, and Command (triggered on value change) bindings.
```xml
<cf:FontPickerControl Width="auto" Height="auto" 
    SelectedFont="{Binding SelectedFont}"
    ValueChanged="fontPicker_ValueChanged"
    Command="{Binding FontChangeCommand}" />
```
* The C# way  
```c#
using ColorFontPickerWPF;

var fontPicker = new FontPickerControl();
/*//In need
fontPicker.SelectedFont = new Font()
{ 
    FontFamily = new FontFamily("Microsoft YaHei UI"),
    FontSize = 12
}; 
//fontPicker.Get(textBlock);//Or use a wrapped method to get the control's font directly
*/
grid.Children.Add(fontPicker);
```

#### FontPickerPopup  
![FontPickerPopup](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerPopup.jpg)  
     
Additional Property **`SelectedFont`** can be get and set.    
Additional Property **`FontText`** can view current font style, default value is '`FontSize: {fontsize}px`'. You can write something else, and '`{fontsize}`' will be replace to font size value.    

FontText="":    
![ColorPickerPopup-text1](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerPopup1.jpg)     

FontText="Font Example":    
![ColorPickerPopup-text2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerPopup2.jpg)     

FontText="Size:{fontsize}":       
![ColorPickerPopup-text3](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/FontPickerPopup3.jpg)     

* The xaml way     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:FontPickerPopup FontText=""/>
```
v2.0 support for `ValueChanged` value change events, variable bindings, and Command (triggered on value change) bindings.
```xml
<cf:FontPickerPopup Width="auto" Height="auto" 
    SelectedFont="{Binding SelectedFont}"
    ValueChanged="fontPicker_ValueChanged"
    Command="{Binding FontChangeCommand}" />
```

* The C# way     
```c#
using ColorFontPickerWPF;

var fontPicker = new FontPickerPopup();
/*//In need
fontPicker.SelectedFont = new Font()
{ 
    FontFamily = new FontFamily("Microsoft YaHei UI"), 
    FontSize = 12
}; 
//fontPicker.Get(textBlock);// or get font from control
*/
grid.Children.Add(fontPicker);
```


## Additional information
* ColorPicker reference Winform design, in addition to RGB and HSL, **HEX** format has been added, and added a **full screen color picking** function.
* “`SelectedFont` is an instance of the `Font` class. v2.0 onwards the entity structure has been simplified and the latest structure is as follows:

```c#
public class Font
{
    public FontFamily FontFamily { get; set;}
    public FontStyle FontStyle { get; set;}
    public FontWeight FontWeight { get; set;}
    public FontStretch FontStretch { get; set;}
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
> [!TIP]
> * Setting with xaml
> 
> In the first version you needed to bind each property manually, now v2.0 provides an additional property to quickly set or bind to the font class:   
> For example, bind to FontPickerControl:
> ```xml
> <TextBlock cf:FontExtension.FontData="{Binding SelectedFont,ElementName=fontPicker,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}" />
> ```
> Or bind to the Font entity of the ViewModel:
> ```xml
> <TextBlock cf:FontExtension.FontData="{Binding SelectedFont}"/>
> ```
> * The C# way    
> 
> provides wrapped methods `fontDialog.GetFont()`, `fontPickerControl.GetFont()`, `fontPickerPopup.GetFont()`
> and `fontDialog.SetFont()`, `fontPickerControl.SetFont()`, `fontPickerPopup.SetFont()`.   

* The interface language is displayed automatically according to the current thread, or you can specify the interface **language manually**:
```c#
using ColorFontPickerWPF;

PickerLanguageManager.settings.UIculture = new CultureInfo("en-US");
```
14 languages are currently supported, including: Chinese, English, Arabic, Czech, German, Spanish, French, Hungarian, Italian, Japanese, Portuguese, Romanian, Russian, and Swedish.    

## Version   
* v2.0.0 2025/1/3 Optimize the default layout of the color picker, reorganize and optimize the code; add ValueChange event and Command command, attribute default bidirectional binding; simplify the splitting of the font entity, and turn it into json for easy preservation; generalize the conversion of fonts, and provide additional attributes for fast binding.
* v1.0.4 2022/12/25 Fix the color dialog language, optimize the method of switching language, no longer force to change to current language before loading.
* v1.0.3 2022/10/17 Add popup control, optimize color control's slider visual effect.     
* v1.0.2 2022/10/15 Fix the bug that some parts are not updated after the assignment.    
* v1.0.1 2022/10/14 Removed the use of color library, color conversion was changed to self-implemented. Fixed bugs in font dialog. added multi-language support, expanded from 2 to 14 languages.    
* v1.0.0 2022/10/13 Basic features. Dialogs and selection controls.   
---

# 中文说明   
颜色对话框：     
![颜色对话框](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E5%AF%B9%E8%AF%9D%E6%A1%86.jpg)       
      
颜色选择下拉：      
![颜色选择下拉](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%892.jpg)       
      
字体对话框：     
![字体对话框](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E5%AF%B9%E8%AF%9D%E6%A1%86.jpg)      
     
字体选择下拉：     
![字体选择下拉](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%893.jpg)      
      
## 使用   
从Nuget下载包，或者引用Release中的dll。   

### 颜色
#### 颜色对话框（ColorDialog）

跟 Winform 一样调用:
```c#
using ColorFontPickerWPF;

ColorDialog colorDialog = new ColorDialog();
//colorDialog.SelectedColor = ((SolidColorBrush)label.Background).Color; //如果需要显示当前值
if (colorDialog.ShowDialog() == true)
    label.Background = new SolidColorBrush(colorDialog.SelectedColor);
```

#### 颜色选择控件（ColorPickerControl） 
颜色选择控件-样式1：           
![颜色选择控件1](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E6%8E%A7%E4%BB%B6.jpg)  
          
颜色选择控件-样式2：           
![颜色选择控件2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E6%8E%A7%E4%BB%B62.jpg)      

附加属性 **`SelectedColor`**（选择的颜色）可以设置和获取。   
附加属性 **`WithoutColorCells`**（不要颜色格子）为True的时候可以折叠左边部分。    

* 用 xaml 的方式     
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:ColorPickerControl Width="auto" Height="auto" SelectedColor="Blue" WithoutColorCells="False"/>
```
v2.0开始支持`ValueChanged`值变更事件、变量绑定、Command（值变更时触发）绑定
```xml
<cf:ColorPickerControl Width="auto" Height="auto" 
    SelectedColor="{Binding SelectedColor}" 
    ValueChanged="colorPicker_ValueChanged" 
    Command="{Binding ColorChangeCommand}"/>
```

* 用 C# 的方式     
```c#
using ColorFontPickerWPF;

var colorPicker = new ColorPickerControl();
//colorPicker.SelectedColor = Colors.Red; //如果需要显示当前值
grid.Children.Add(colorPicker);
```

#### 颜色选择下拉（ColorPickerPopup）  

![颜色选择下拉](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%89.jpg)  
     
附加属性 **`SelectedColor`**（选择的颜色）可以设置和获取。   
附加属性 **`ColorText`** （颜色文本）可以显示当前颜色值，支持颜色模型："None"，"RGB"，"HEX"，"HSL"。      

ColorText = "RGB":    
![颜色选择下拉-文本1](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%891.jpg)     

ColorText = "HEX":    
![颜色选择下拉-文本2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%892.jpg)     

ColorText = "HSL":      
![颜色选择下拉-文本3](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E9%A2%9C%E8%89%B2%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%893.jpg)     
       
* 用 xaml 的方式       
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:ColorPickerPopup SelectedColor="Yellow" ColorText="RGB"/>
```

v2.0开始支持`ValueChanged`值变更事件、变量绑定、Command（值变更时触发）绑定

```xml
<cf:ColorPickerPopup SelectedColor="{Binding SelectedColor}" 
    ValueChanged="colorPicker_ValueChanged" 
    Command="{Binding ColorChangeCommand}"/>
```

* 用 C# 的方式     

```c#
using ColorFontPickerWPF;

var colorPicker = new ColorPickerPopup();
//colorPicker.SelectedColor = Colors.Red; //如果需要显示当前值
grid.Children.Add(colorPicker);
```


### 字体
#### 字体对话框（FontDialog）
为了更便于使用，字体提供了获取和设置的封装方法：
```c#
using ColorFontPickerWPF;

FontDialog fontDialog = new FontDialog();
//fontDialog.GetFont(textBlock); //如果需要显示当前值
if (fontDialog.ShowDialog() == true)
    fontDialog.SetFont(textBlock);
```

#### 字体选择控件（FontPickerControl） 
字体选择控件-样式1：    
![字体选择控件1](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E6%8E%A7%E4%BB%B6.jpg)  
            
字体选择控件-样式2：    
![字体选择控件2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E6%8E%A7%E4%BB%B62.jpg)  
             
字体选择控件-样式3：    
![字体选择控件3](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E6%8E%A7%E4%BB%B63.jpg)     

附加属性 **`SelectedFont`**（选择的字体）可以获取和设置。    
附加属性 **`WithoutDecorations`** （不要装饰线设置）和 **`WithoutPreviewRow`** （不要预览行）值为True的时候可以折叠对应区域。     

* 用 xaml 的方式      
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:FontPickerControl Width="auto" Height="auto" WithoutDecorations="False" WithoutPreviewRow="False"/>
```
v2.0开始支持`ValueChanged`值变更事件、变量绑定、Command（值变更时触发）绑定
```xml
<cf:FontPickerControl Width="auto" Height="auto" 
    SelectedFont="{Binding SelectedFont}"
    ValueChanged="fontPicker_ValueChanged"
    Command="{Binding FontChangeCommand}" />
```

* 用 C# 的方式     
```c#
using ColorFontPickerWPF;

var fontPicker = new FontPickerControl();
/*//如果需要显示当前值
fontPicker.SelectedFont = new Font()
{ 
    FontFamily = new FontFamily("Microsoft YaHei UI"), 
    FontSize = 12
}; 
//fontPicker.Get(textBlock);//或者用封装的方法直接获取控件字体
*/
grid.Children.Add(fontPicker);
```


#### 字体选择下拉（FontPickerPopup）  
![字体选择下拉](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%89.jpg)  
     
附加属性 **`SelectedFont`**（选择的字体）可以获取和设置。     
附加属性 **`FontText`**（字体文本） 可以显示当前字体样式，默认显示 'FontSize: `{fontsize}`px'。可以自定义任何内容，并且“`{fontsize}`”可以作为变量显示当前字号。    

FontText="":    
![字体选择下拉-文本1](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%891.jpg)     

FontText="字体预览":    
![字体选择下拉-文本2](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%892.jpg)     

FontText="字号：{fontsize}px":       
![字体选择下拉-文本3](https://github.com/tp1415926535/ColorFontPickerWPF/blob/master/ScreenShots/%E5%AD%97%E4%BD%93%E9%80%89%E6%8B%A9%E4%B8%8B%E6%8B%893.jpg)     

* 用 xaml 的方式         
```xml
xmlns:cf="clr-namespace:ColorFontPickerWPF;assembly=ColorFontPickerWPF"
        
<cf:FontPickerPopup FontText=""/>
```
v2.0开始支持`ValueChanged`值变更事件、变量绑定、Command（值变更时触发）绑定
```xml
<cf:FontPickerPopup Width="auto" Height="auto" 
    SelectedFont="{Binding SelectedFont}"
    ValueChanged="fontPicker_ValueChanged"
    Command="{Binding FontChangeCommand}" />
```

* 用 C# 的方式     
```c#
using ColorFontPickerWPF;

var fontPicker = new FontPickerPopup();
/*//如果需要显示当前值
fontPicker.SelectedFont = new Font()
{ 
    FontFamily = new FontFamily("Microsoft YaHei UI"), 
    FontSize = 12
}; 
//fontPicker.Get(textBlock);//或者用封装的方法直接获取控件字体
*/
grid.Children.Add(fontPicker);
```


## 补充说明
* 颜色选择器沿用Winform的布局，除了RGB、HSL之外还增加了**HEX**格式，另外还有**全屏取色**的功能。
* “`SelectedFont`” （选择的字体）是 “`Font`” 类的实例。v2.0之后实体结构简化，最新结构如下：
```c#
public class Font
{
    public FontFamily FontFamily { get; set;}
    public FontStyle FontStyle { get; set;}
    public FontWeight FontWeight { get; set;}
    public FontStretch FontStretch { get; set;}
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

> [!TIP]
> * 设置用 xaml 的方式
> 
> 在一开始的版本你需要手动绑定每个属性，现在 v2.0 提供了一个附加属性来快速设置或者绑定font类：  
> 
> 比如绑定到 FontPickerControl：
> ```xml
> <TextBlock  cf:FontExtension.FontData="{Binding SelectedFont,ElementName=fontPicker,Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}"/>
> ```
> 或者绑定到 ViewModel 的 Font 实体：
> ```xml
> <TextBlock cf:FontExtension.FontData="{Binding SelectedFont}"/>
> ```
> * 用 C# 的方式    
> 
> 提供了封装的方法"`fontDialog.GetFont()`", "`fontPickerControl.GetFont()`", "`fontPickerPopup.GetFont()`"
> 以及 "`fontDialog.SetFont()`", "`fontPickerControl.SetFont()`", "`fontPickerPopup.SetFont()`"。   

* 界面语言根据当前线程自动显示，你也可以手动指定界面语言：
```c#
using ColorFontPickerWPF;

PickerLanguageManager.settings.UIculture = new CultureInfo("en-US");
```
目前支持14种语言，包括：中文，英语，阿拉伯语，捷克语，德语，西班牙语，法语，匈牙利语，意大利语，日语，葡萄牙语，罗马尼亚语，俄语，瑞典语。    

## 版本   
* v2.0.0 2025/1/3 优化颜色选择器默认布局，重新整理并优化代码；增加ValueChange事件和Command命令，属性默认双向绑定；字体实体简化拆分，转json便于保存；字体转换通用化，提供附加属性快速绑定
* v1.0.4 2022/12/25 修复颜色对话框语言，优化切换语言方法，不再加载前强制改为当前语言。
* v1.0.3 2022/10/17 添加下拉控件，优化颜色控件的滑动条视觉效果。     
* v1.0.2 2022/10/15 修复赋值之后部分不更新的bug。 
* v1.0.1 2022/10/14 移除了对颜色库的使用，颜色转换改为自行实现。字体对话框修复bug。新增多语言支持，从2种语言扩展到14种。    
* v1.0.0 2022/10/13 基本功能。对话框和选择控件。    
