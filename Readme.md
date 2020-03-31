# TwoPaneMarkdownEditor

## Windows Developer Incubation and Learning - Paula Scholz

## Introduction

![TwoPaneMarkdownEditor](MarkDownEditor/ReadmeImages/MarkdownEditor.gif)

A Universal Windows Platform sample that illustrates the [TwoPaneView](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/two-pane-view) layout control for [Dual-Screen experiences](https://docs.microsoft.com/en-us/dual-screen/introduction), 

[TwoPaneView](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/two-pane-view) provides two distinct areas of content that can be spanned onto separate screens on dual-screen devices like the [Surface Neo](https://www.microsoft.com/en-us/surface/devices/surface-neo?&OCID=AID2000022_SEM_oCeJqLSf&msclkid=41672d2d892e1554df52734a51ae580b). `TwoPaneView` is the primary layout panel used to support dual-screen development for UWP applications.

While `TwoPaneView` is part of the Windows SDK, Microsoft recommends you use the version inside the [Windows UI](https://microsoft.github.io/microsoft-ui-xaml/) library, which provides updated versions of existing Windows platform controls.

## System Requirements

Development for dual-screen experiences requires
[Windows 10 version 10.0.18362](https://docs.microsoft.com/en-us/windows/uwp/whats-new/windows-10-build-18362) or better, along with its companion [Windows SDK](https://developer.microsoft.com/en-US/windows/downloads/windows-10-sdk/).  You also need [Windows UI](https://microsoft.github.io/microsoft-ui-xaml/) `version 2.4.0-prerelease-200203002` or better, available for installation through NuGet.

## Development Tools

Please refer to the [Windows 10x development tools](https://docs.microsoft.com/en-us/dual-screen/windows/get-dev-tools) guide.

You will need the latest version of [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/). This sample was built with Visual Studio 2019 16.5.

You will also need the [Microsoft Emulator with the Windows 10x Emulator Image](https://blogs.windows.com/windowsdeveloper/2020/03/10/microsoft-emulator-and-windows-10x-emulator-image-preview-build-19578-available-now/) from the Microsoft Store, so that you may run this sample without an actual dual-screen device. 

The sample may also be run on desktop `Windows version 10.0.18362` or better without an emulator. In this case, it will appear as two seperate panes side by side.

## Using the Windows UI Library

To use the [Windows UI library](https://microsoft.github.io/microsoft-ui-xaml/) inside your dual-screen application, you must first install it from NuGet.  Right-click on your project file in the Visual Studio Solution Explorer to launch the NuGet tool.

Make sure you include the prerelease packages by selecting the checkbox.  You will need version `2.4.0-prerelease.20203002` or better.

Then, to use Windows UI Library controls rather than SDK controls, you will need to place a XamlControlsResources reference in your `App.xaml` Resource Dictionary.

For more details, please refer to the [Getting started with the Windows UI Library](https://docs.microsoft.com/en-us/uwp/toolkits/winui/getting-started) guide.


## Configuring your Emulator

You may need to configure your Win10x Emulator image.  Please refer to the [release notes](https://docs.microsoft.com/en-us/dual-screen/windows/release-notes#emulator-app) for the Windows 10x development tools.


## Debugging
You should follow this [guidance](https://docs.microsoft.com/en-us/dual-screen/windows/use-emulator#visual-studio-2019-preview) for debugging your Windows 10x UWP dual-screen apps.  

## Visual Studio Solution

The Visual Studio Solution is shown below:

![VisualStudioSolution](MarkDownEditor/ReadmeImages/MarkdownEditorVisualStudioSolution.png)

`MainPage.xaml` contains the single Page and `TwoPaneView` control and serves as the core of the project.  

The `sample.txt` markdown document is embedded as Content and serves as the document to display in the [MarkdownTextBlock](https://docs.microsoft.com/en-us/windows/communitytoolkit/controls/markdowntextblock) XAML Control from the [Windows Community Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/).

## TwoPaneView
The primary display layout panel for our application is [TwoPaneView](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/two-pane-view). This control provides a separate display surface `Pane` for each screen when the application is `spanned` across screens, and when the application is on a single screen these panes are displayed according to its `PanePriority`, `TallModeConfiguration` and `WideModeConfiguration` properties.  

`MainPage.xaml` is the single page in our solution.  On application startup, it appears like this:

![MainPage](MarkDownEditor/ReadmeImages/MarkdownEditorSingleScreen.png)

At the top of the page, we see a [TextToolbar XAML Control](https://docs.microsoft.com/en-us/windows/communitytoolkit/controls/texttoolbar) from the [Windows Community Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/).  This control works in conjunction with the [RichEditBox control](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.RichEditBox) of the `Windows SDK` to insert `Markdown` tags into the text.

Below this, we show `Pane2` of the `TwoPaneView` control, which is our application's [MarkdownTextBlock XAML Control](https://docs.microsoft.com/en-us/windows/communitytoolkit/controls/markdowntextblock), providing full markdown and parsing capability for UWP apps.

Finally, we have `Pane1` of the `TwoPaneView` control, which contains our [RichEditBox control](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.RichEditBox) where `Markdown` text is entered.  This control is bound to the `MarkdownTextBlock` control so that text changes are immediately reflected in the `MarkDown Preview` area of the app.

When the user spans this application across screens, the panes are displayed like this:

![MainPage Spanned](MarkDownEditor/ReadmeImages/MarkdownEditorSpanned.png)

`Pane1` appears on the left-hand screen, and `Pane2` appears on the right.  The scrollbars of each pane are tied together in the code-behind to enable synchronous scrolling.

The XAML of MainPage looks like this:

```xaml
<Page
    x:Class="MarkDownEditor.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MarkDownEditor"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:converters="using:Microsoft.Toolkit.Uwp.UI.Converters"
    xmlns:winuicontrols="using:Microsoft.UI.Xaml.Controls" 
    xmlns:controls="using:Microsoft.Toolkit.Uwp.UI.Controls"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}"
    xmlns:contract7Present="http://schemas.microsoft.com/winfx/2006/xaml/presentation?IsApiContractPresent(Windows.Foundation.UniversalApiContract,7)" 
    Loaded="Page_Loaded">
    
    <Page.Resources>
		<converters:ToolbarFormatActiveConverter x:Key="IsFormatMarkdown"
                    Format="MarkDown" />
	</Page.Resources>
    
	<Grid x:Name="MainGrid" Margin="12">
		<Grid.RowDefinitions>
			<RowDefinition Height="Auto" />
			<RowDefinition />
		</Grid.RowDefinitions>
        
        <!-- This TextToolbar is used to insert Markdown tags into the EditZome RichEditBox. -->
		<controls:TextToolbar x:Name="Toolbar"
                          Editor="{Binding ElementName=EditZone}"
                          IsEnabled="True"
                          Format="MarkDown"
                          UseURIChecker="True"
                          Background="#4C4F4F4F" />
        
		<!--Pane 1 and Pane 2 take up 50% each of the available real-estate  by setting pane length to 1* Grid length. 
            In tall mode, To show the markdowneditor(pane1) at the bottom, tall mode is set to BottomTop -->
		<winuicontrols:TwoPaneView x:Name="twoPaneViewDemo" x:FieldModifier="public" 
                 Pane1Length="1*"
                 Pane2Length="1*"
                 MinTallModeHeight="500"
                 MinWideModeWidth="700"
                 TallModeConfiguration="BottomTop"
                 WideModeConfiguration="LeftRight" 
                 Grid.Row="1">
            
            <!-- This is the Editor pane where Markdown is entered and edited. -->
			<winuicontrols:TwoPaneView.Pane1>
				<Grid x:Name="MarkDownEditor"
                    Grid.Row="1"
                    Margin="0, 16"
                    Visibility="{Binding Format, ElementName=Toolbar, Mode=OneWay, Converter={StaticResource IsFormatMarkdown}}">
                    
					<Grid.RowDefinitions>
						<RowDefinition Height="Auto" />
						<RowDefinition />
					</Grid.RowDefinitions>
                    
					<TextBlock Foreground="{ThemeResource SystemControlPageTextBaseHighBrush}"
                     Style="{StaticResource SubtitleTextBlockStyle}"
                     Text="MarkDown Editor" />
                    
					<ScrollViewer x:Name="Editor" ViewChanged="MarkEditor_ViewChanged" Grid.Row="1">
						<RichEditBox x:Name="EditZone"                     
                         Margin="0, 4, 10, 0"
                         PlaceholderText="Enter Text Here"
                         TextWrapping="Wrap"
                         TextChanged="EditZone_TextChanged"
                         VerticalContentAlignment="Stretch"
                         Padding="10,3"
                         BorderThickness="1"
                         BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
                         Foreground="{ThemeResource SystemControlForegroundBaseMediumBrush}"
                         contract7Present:SelectionFlyout="{x:Null}"/>
					</ScrollViewer>
				</Grid>
			</winuicontrols:TwoPaneView.Pane1>
            
            <!-- This is the Markdown display pane.  -->
			<winuicontrols:TwoPaneView.Pane2>

				<Grid x:Name="MarkDownPreview"
                  Grid.Row="1"
                  Margin="0, 16"
                  Visibility="{Binding Format, ElementName=Toolbar, Mode=OneWay, Converter={StaticResource IsFormatMarkdown}}">
                    
					<Grid.RowDefinitions>
						<RowDefinition Height="Auto" />
						<RowDefinition />
					</Grid.RowDefinitions>
                    
					<TextBlock Foreground="{ThemeResource SystemControlPageTextBaseHighBrush}"
                     Style="{StaticResource SubtitleTextBlockStyle}" 
                     Text="MarkDown Preview"  Margin="10, 0, 0, 0" />
					<ScrollViewer x:Name="MarkEditor" ViewChanged="MarkEditor_ViewChanged" Grid.Row="1">
						<controls:MarkdownTextBlock x:Name="Previewer"
                                      Background="LightGray"
                                      Padding="10,3"
                                      Margin="10, 4, 0, 0"
                                      Canvas.ZIndex="99"
                                      BorderThickness="1"
                                      BorderBrush="{ThemeResource SystemControlForegroundChromeHighBrush}"
                                      Foreground="{ThemeResource SystemControlPageTextBaseHighBrush}"
                                      ScrollViewer.IsVerticalRailEnabled="False" />
					</ScrollViewer>
				</Grid>                
			</winuicontrols:TwoPaneView.Pane2>  
            
		</winuicontrols:TwoPaneView>
	</Grid>
</Page>
```
Note the `TallModeConfiguration` property of the `TwoPaneView` control is set to `BottomTop`, meaning the pane that has display priority, the `RichTextBox` control, is shown on the bottom in single screen mode.

The code-behind for this page is simple. We rely on the intrinsic capabilities of our [Windows Community Toolkit](https://docs.microsoft.com/en-us/windows/communitytoolkit/) controls and [TwoPaneView](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/two-pane-view) for almost all functionality.

```csharp    
public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

		/// <summary>
		/// If the text in the RichTextBox changes, place it into the MarkdownTextBlock
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditZone_TextChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			string text = Toolbar.Formatter?.Text;
			Previewer.Text = string.IsNullOrWhiteSpace(text) ? "Nothing to Preview" : text;
		}

#pragma warning disable 612, 618
		/// <summary>
		/// If the Markdown Editor window scrolls, scroll the Editor window as well and vice-versa.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// 
		private void MarkEditor_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
		{
			// Note: we are using "obsolete" ScrollToVerticalOffset and ScrollToHorizontalOffset
			// methods by design.

			var scrollViewer = (ScrollViewer)sender;
			if (scrollViewer == Editor)
			{
				MarkEditor.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
				MarkEditor.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);

				// Note: This is the "new" way of scrolling, but with current implementation provides
				// a "jumpy" scrolling experience, thus the "obsolete" methods above. 
				// This is put here for reference in case that problem is fixed in future builds.
				//MarkEditor.ChangeView(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset, null);
			}
			else
			{
				Editor.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
				Editor.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset);

				// Note: This is the "new" way of scrolling, but with current implementation provides
				// a "jumpy" scrolling experience, thus the older "obsolete" methods above. 
				// This is put here for reference in case that problem is fixed in future builds.
				//Editor.ChangeView(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset, null);
			}
		}
#pragma warning restore 612, 618

		/// <summary>
		/// Load our markdown sample text and place it into the RichTextBox control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{

			if (Previewer != null)
			{
				Previewer.LinkClicked += Previewer_LinkClicked;
				Previewer.ImageClicked += Previewer_ImageClicked;
				Previewer.CodeBlockResolving += Previewer_CodeBlockResolving;
			}

			// Load the initial demo data from the file.  Make sure the file properties are set to 
			// Build Action - Content and Copy to Output Directory - Always
			try
			{
				StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///sample.txt"));
				Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
				EditZone.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, fileStream);
			}
			catch (Exception)
			{
				if (EditZone != null)
				{
					EditZone.TextDocument.SetText(TextSetOptions.None, "## Error Loading Content ##");
				}
			}

		}
	}
```

Of particular note is the `MarkdownEditor_ViewChanged` event handler. We have used the [#pragma warning](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives/preprocessor-pragma-warning) compiler pre-processor directive to suppress warning messages for the use of deprecated `ScrollViewer` [ScrollToVerticalOffset](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.scrollviewer.scrolltoverticaloffset) and [ScrollToHorizontalOffset](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.scrollviewer.scrolltohorizontaloffset) methods. These methods, while deprecated, provide superior performance over the new [ChangeView](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.scrollviewer.changeview#Windows_UI_Xaml_Controls_ScrollViewer_ChangeView_Windows_Foundation_IReference_System_Double__Windows_Foundation_IReference_System_Double__Windows_Foundation_IReference_System_Single__) method. `ChangeView` provides jumpy performance under the current implementation.  The deprecated methods may be removed in a future update.



