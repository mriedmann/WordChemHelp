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
using Microsoft.Office.Tools.Ribbon;
using WordChemHelp.Core;

namespace WordChemHelp
{
    public partial class Ribbon
    {
        private static FormatHelper helper = new FormatHelper();

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var currRange = Globals.ThisAddIn.Application.Selection.Range;
                int rStart = currRange.Start;

                string text = currRange.Text.Trim();
                FormatString fstring = helper.FormatInput(text);

                for (int i = 0; i < fstring.FormatMask.Count; i++)
                {
                    Globals.ThisAddIn.Application.Selection.SetRange(rStart + i, rStart + i + 1);

                    Globals.ThisAddIn.Application.Selection.Text = fstring.Content[i].ToString();

                    FormatFlags x = fstring.FormatMask[i];
                    switch (x)
                    {
                        case FormatFlags.None:
                            break;
                        case FormatFlags.Subscript:
                            Globals.ThisAddIn.Application.Selection.Font.Subscript = 1;
                            break;
                        case FormatFlags.Superscript:
                            Globals.ThisAddIn.Application.Selection.Font.Superscript = 1;
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
