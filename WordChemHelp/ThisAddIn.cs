/*******************************************************************
ChemHelp Word ADD-IN
Copyright (C) 2015 Michael Riedmann <michael_riedmann(AT)live.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using WordChemHelp.Core;

namespace WordChemHelp
{
    public partial class ThisAddIn
    {
        private static FormatHelper helper = new FormatHelper();

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void FormatFormula(bool formatAll = false)
        {
            this.Application.Selection.ShrinkDiscontiguousSelection();
            var currRange = this.Application.Selection.Range;
            var currText = currRange.Text.Trim();

            

            if (formatAll)
            {
                object findText = currText;
                Word.Range rng = this.Application.ActiveDocument.Content;
                Word.Find rngFind = rng.Find;
                rngFind.Forward = true;

                do
                {
                    rngFind.ClearFormatting();
                    if (rngFind.Execute(FindText: ref findText))
                    {
                        if(!FormatFormulaAt(rng.Start, currText))
                            return;
                        rng.SetRange(rng.End, this.Application.ActiveDocument.Content.End);
                    } 
                }
                while (rngFind.Found);
            }
            else
            {
                FormatFormulaAt(currRange.Start, currText);
            }
        }

        private bool FormatFormulaAt(int rStart, string text)
        {
            var objUndo = this.Application.UndoRecord;
            objUndo.StartCustomRecord("Format Formula"); 

            try
            {
                FormatString fstring = helper.FormatInput(text);

                for (int i = 0; i < fstring.FormatMask.Count; i++)
                {
                    Word.Range rng = this.Application.ActiveDocument.Range(rStart + i, rStart + i + 1);

                    rng.Text = fstring.Content[i].ToString();

                    FormatFlags x = fstring.FormatMask[i];
                    switch (x)
                    {
                        case FormatFlags.None:
                            break;
                        case FormatFlags.Subscript:
                            rng.Font.Subscript = 1;
                            break;
                        case FormatFlags.Superscript:
                            rng.Font.Superscript = 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                objUndo.EndCustomRecord();
            }
            return true;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
