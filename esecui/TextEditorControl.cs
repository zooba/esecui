using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#if MONO

namespace ICSharpCode.TextEditor
{
    public class TextMarker { }

    public interface ISelection
    {
        int Offset { get; }
        int Length { get; }
    }

    public class TextAreaControl
    {
        public TextAreaControl(TextEditorControl owner)
        {
            Owner = owner;
            Caret = new CaretManager(owner);
        }
        
        public void CenterViewOn(int line, int tolerance)
        {
            Owner.ScrollToCaret();
        }

        public TextEditorControl Owner;
        public CaretManager Caret;
        public CaretManager SelectionManager { get { return Caret; } }

        public class CaretManager
        {
            public CaretManager(TextEditorControl owner)
            {
                Owner = owner;
            }

            private readonly TextEditorControl Owner;

            public void SetSelection(ISelection selection)
            {
                Owner.SelectionStart = selection.Offset;
                Owner.SelectionLength = selection.Length;
            }

            public void SetSelection(TextLocation start, TextLocation end)
            {
                Owner.SelectionStart = Owner.GetFirstCharIndexFromLine(start.Line) + start.Column;
                Owner.SelectionLength = Owner.GetFirstCharIndexFromLine(end.Line) + end.Column - Owner.SelectionStart;
            }
            
            public TextLocation Position
            {
                get
                {
                    int line = Owner.GetLineFromCharIndex(Owner.SelectionStart);
                    return new TextLocation(Owner.SelectionStart - Owner.GetFirstCharIndexFromLine(line), line);
                }
                set
                {
                    Owner.SelectionStart = Owner.GetFirstCharIndexFromLine(value.Line) + value.Column;
                    Owner.SelectionLength = 0;
                }
            }
        }
    }

    public class TextEditorControl : TextBox
    {
        public Document.TextDocument Document { get; private set; }
        public TextAreaControl ActiveTextAreaControl { get; private set; }

        public TextEditorControl()
        {
            Document = new Document.TextDocument(this);
            ActiveTextAreaControl = new TextAreaControl(this);
            Multiline = true;
            ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        }

        public bool ConvertTabsToSpaces { get; set; }
        public bool EnableFolding { get; set; }
        public bool IsReadOnly
        {
            get { return ReadOnly; }
            set { ReadOnly = value; }
        }
        public bool ShowVRuler { get; set; }
        public bool ShowLineNumbers { get; set; }
        public Document.IndentStyle IndentStyle { get; set; }

        public void SetHighlighting(string name) { }
        
        public void BeginUpdate() { }
        public void EndUpdate() { }

        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value.Replace("\n", "\r\n").Replace("\r\r\n", "\r\n"); }
        }
    }

    public class TextLocation
    {
        public int Line;
        public int Column;
        public TextLocation(int column, int line)
        {
            Line = line;
            Column = column;
        }
    }

}

namespace ICSharpCode.TextEditor.Document
{
    public class TextDocument
    {
        private readonly TextEditorControl Owner;

        public event DocumentEventHandler DocumentChanged;

        public TextDocument(TextEditorControl owner)
        {
            Owner = owner;
            Owner.TextChanged += new EventHandler(Owner_TextChanged);
            MarkerStrategy = new List<object>();
        }
        
        public List<object> MarkerStrategy { get; private set; }

        void Owner_TextChanged(object sender, EventArgs e)
        {
            var temp = DocumentChanged;
            if (temp != null)
            {
                temp(this, new DocumentEventArgs(this));
            }
        }

        public string TextContent
        {
            get { return Owner.Text; }
            set { Owner.Text = value; }
        }

        public void Insert(int offset, string text)
        {
            var selStart = Owner.SelectionStart;
            var selLength = Owner.SelectionLength;
            Owner.SelectionStart = offset;
            Owner.SelectionLength = 0;
            Owner.SelectedText = text;
            Owner.SelectionStart = selStart + (selStart > offset ? text.Length : 0);
            Owner.SelectionLength = selLength;
        }

        public int GetLineNumberForOffset(int offset)
        {
            return Owner.GetLineFromCharIndex(offset);
        }

        public string GetLineSegment(int line)
        {
            return Owner.Lines[line];
        }

        public int PositionToOffset(TextLocation position)
        {
            return Owner.GetFirstCharIndexFromLine(position.Line) + position.Column;
        }

        public string GetText(int offset, int length)
        {
            return Owner.Text.Substring(offset, length);
        }

        public int TextLength
        {
            get { return Owner.TextLength; }
        }
    }

    public delegate void DocumentEventHandler(object sender, DocumentEventArgs e);
    public class DocumentEventArgs : EventArgs
    {
        internal DocumentEventArgs(TextDocument document)
        {
            Document = document;
        }

        public TextDocument Document { get; set; }
    }


    public enum IndentStyle
    {
        None
    }
}
#endif
