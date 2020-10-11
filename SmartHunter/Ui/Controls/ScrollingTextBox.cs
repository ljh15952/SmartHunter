using System;
using System.Windows.Controls;

namespace SmartHunter.Ui.Controls
{
    /// <summary>
    /// This textbox will scroll to end when text is changed
    /// </summary>
    public class ScrollingTextBox : TextBox {

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            CaretIndex = Text.Length;
            ScrollToEnd();
        }
    }
}
