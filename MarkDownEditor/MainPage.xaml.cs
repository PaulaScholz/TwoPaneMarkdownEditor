//***********************************************************************
//
// Copyright (c) 2020 Microsoft Corporation. All rights reserved.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//**********************************************************************​
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MarkDownEditor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

		/// <summary>
		/// If there is a link, launch the web page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void Previewer_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			if (!Uri.TryCreate(e.Link, UriKind.Absolute, out Uri result))
			{
				await new MessageDialog("Masked relative links needs to be manually handled.").ShowAsync();
			}
			else
			{
				await Launcher.LaunchUriAsync(new Uri(e.Link));
			}
		}

		/// <summary>
		/// If there is a linked image, launch the web page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void Previewer_ImageClicked(object sender, LinkClickedEventArgs e)
		{
			if (!Uri.TryCreate(e.Link, UriKind.Absolute, out Uri result))
			{
				await new MessageDialog("Masked relative Images needs to be manually handled.").ShowAsync();
			}
			else
			{
				await Launcher.LaunchUriAsync(new Uri(e.Link));
			}
		}

		/// <summary>
		/// Custom code block renderer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Previewer_CodeBlockResolving(object sender, CodeBlockResolvingEventArgs e)
		{
			if (e.CodeLanguage == "CUSTOM")
			{
				e.Handled = true;
				e.InlineCollection.Add(new Run { Foreground = new SolidColorBrush(Colors.Red), Text = e.Text, FontWeight = FontWeights.Bold });
			}
		}
	}
}
