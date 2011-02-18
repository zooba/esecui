using System;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using Microsoft.Scripting.Interpreter;

namespace esecui
{
    [Serializable]
    public sealed class ErrorItem : ListViewItem, IComparable, IDisposable, ISelection
    {
        public readonly TextEditorControl Source;

        public int Line { get; private set; }
        public int Column { get; private set; }
        public int EndLine { get; private set; }
        public int EndColumn { get; private set; }

        public readonly string Message;
        public readonly string Code;
        public readonly bool IsWarning;

        private TextMarker Marker;

        public ErrorItem(TextEditorControl source, int line, int column, int endLine, int endColumn, string message, string code, bool isWarning)
        {
            Source = source;
            Source.Document.DocumentAboutToBeChanged += new DocumentEventHandler(Document_DocumentAboutToBeChanged);
            Source.Document.LineCountChanged += new EventHandler<LineCountChangeEventArgs>(Document_LineCountChanged);
            Source.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);

            Line = line;
            Column = column;
            EndLine = endLine;
            EndColumn = endColumn;

            Message = message;
            Code = code;
            IsWarning = isWarning;

            Marker = null;

            this.Tag = this;
            this.ForeColor = IsWarning ? SystemColors.WindowText : Color.DarkRed;
            this.UseItemStyleForSubItems = true;
            this.SubItems.Add(Code);
            this.SubItems.Add(Message);

            Update();
        }

        private ErrorItem(System.Runtime.Serialization.SerializationInfo info, 
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }

        private void Update()
        {
            if (Marker == null)
            {
                Marker = new TextMarker(Source.Document.PositionToOffset(StartPosition), Length,
                    TextMarkerType.WaveLine, IsWarning ? Color.Green : Color.Red);
                Source.Document.MarkerStrategy.AddMarker(Marker);
            }
            else
            {
                Marker.Offset = Offset;
                Marker.Length = Length;
            }

            Name = string.Format("{0}:{1}:{2}", Line, Column, Code);
            Text = string.Format("{0}:{1}", Line + 1, Column + 1);
        }

        public static ErrorItem FromIronPythonException(TextEditorControl source, Exception error,
            int adjustLine = 0, int adjustCol = 0)
        {
            var ifi = error.Data[typeof(InterpretedFrameInfo)] as InterpretedFrameInfo[];
            var se = error as Microsoft.Scripting.SyntaxErrorException;
            if (ifi != null)
            {
                var offset = ifi[0].DebugInfo.Index;
                var line = source.Document.GetLineNumberForOffset(offset);
                return new ErrorItem(source,
                    line + adjustLine, adjustCol,
                    line + adjustLine, source.Document.GetLineSegment(line).Length + adjustCol,
                    error.Message, "", false);
            }
            else if (se != null)
            {
                return new ErrorItem(source,
                    se.Line - 1 + adjustLine, se.Column - 1 + adjustCol,
                    se.Line - 1 + adjustLine, se.Column - 1 + adjustCol + se.RawSpan.Length,
                    se.Message, se.ErrorCode.ToString(), se.Severity == Microsoft.Scripting.Severity.Warning);
            }
            else
            {
                return new ErrorItem(source,
                    0, 0, 0, 0,
                    error.Message, "", false);
            }
        }

        public static ErrorItem FromEsdlcException(TextEditorControl source, dynamic error)
        {
            return new ErrorItem(source,
                error.line - 1, error.col - 1, error.line - 1, error.col - 1 + error.length,
                error.message, error.code, error.iswarning);
        }

        #region Error Location Tracking

        void Document_DocumentAboutToBeChanged(object sender, DocumentEventArgs e)
        {
            var pos = e.Document.OffsetToPosition(e.Offset);

            if (e.Length == 2 && e.Document.GetText(e.Offset, e.Length) == "\r\n")
            {
                // Deleting a line
                if (pos.Line < Line || (pos.Line == Line && pos.Column < Column))
                {
                    Line -= 1;
                    Column += e.Document.GetLineSegment(Line).Length;
                }
                if (pos.Line < EndLine || (pos.Line == EndLine && pos.Column < EndColumn))
                {
                    EndLine -= 1;
                    EndColumn += e.Document.GetLineSegment(EndLine).Length;
                }
            }
            else if (e.Text == "\r\n")
            {
                // Inserting a line
                if (pos.Line < Line || (pos.Line == Line && pos.Column <= Column))
                {
                    Line += 1;
                    Column -= pos.Column;
                }
                if (pos.Line < EndLine || (pos.Line == EndLine && pos.Column < EndColumn))
                {
                    EndLine += 1;
                    EndColumn -= pos.Column;
                }
            }
            else
            {
                // Inserting characters on the same line
                if (pos.Line == Line && pos.Column <= Column)
                {
                    Column -= e.Length;
                }
                if (pos.Line == EndLine && pos.Column < EndColumn)
                {
                    EndColumn -= e.Length;
                }
            }
        }

        void Document_DocumentChanged(object sender, DocumentEventArgs e)
        {
            Update();
        }

        void Document_LineCountChanged(object sender, LineCountChangeEventArgs e)
        {
        }

        #endregion

        #region ISelection Members

        public bool ContainsOffset(int offset)
        {
            return Offset <= offset && offset <= EndOffset;
        }

        public bool ContainsPosition(TextLocation position)
        {
            return ContainsOffset(Source.Document.PositionToOffset(position));
        }

        public int EndOffset
        {
            get { return Source.Document.PositionToOffset(EndPosition); }
        }

        public TextLocation EndPosition
        {
            get
            {
                return new TextLocation(EndColumn, EndLine);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public bool IsEmpty
        {
            get { return EndOffset == Offset; }
        }

        public bool IsRectangularSelection
        {
            get { return false; }
        }

        public int Length
        {
            get { return EndOffset - Offset; }
        }

        public int Offset
        {
            get { return Source.Document.PositionToOffset(StartPosition); }
        }

        public string SelectedText
        {
            get { return Source.Document.GetText(this.Offset, this.Length); }
        }

        public TextLocation StartPosition
        {
            get
            {
                return new TextLocation(Column, Line);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var other = (ErrorItem)obj;

            int cmp = Offset.CompareTo(other.Offset);
            if (cmp == 0) cmp = Code.CompareTo(other.Code);
            return cmp;
        }

        #endregion

        public void Dispose()
        {
            if (Source != null && !Source.IsDisposed)
            {
                Source.Document.MarkerStrategy.RemoveMarker(Marker);
                Source.Document.DocumentChanged -= Document_DocumentChanged;
                Source.Document.LineCountChanged -= Document_LineCountChanged;
            }
        }
    }
}
