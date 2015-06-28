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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordChemHelp.Core
{
    public class FormatHelper
    {

        public FormatString FormatInput(string input)
        {
            Regex checkCharge = new Regex("^([^\\+\\- ]*)(\\+\\d?|\\-\\d?)?");

            Match m = checkCharge.Match(input);
            string rawFormula = m.Groups[1].Value;
            int rawFormulaLength = m.Groups[1].Value.Length;
            int chargeLength = 0;

            if(m.Groups[2].Success)
                chargeLength = m.Groups[2].Value.Length;

            FormatString result = new FormatString(input);
            if (chargeLength > 0)
                result.SetFlagAt(rawFormulaLength, chargeLength, FormatFlags.Superscript);

            FormatCompounds(rawFormula, result);

            Regex swapCharge = new Regex("^([^\\+\\- ]*)(([\\+\\-])(\\d))?");
            string newContent = swapCharge.Replace(result.Content, "$1$4$3");
            return new FormatString(newContent, result.FormatMask);
        }

        private static int FormatCompounds(string input, FormatString result, int textPointer = 0)
        {
            Regex findCompondsAndGroups = new Regex("([A-Z][a-z]?\\d*)|(\\([^()]*(?:\\(.*\\))?[^()]*\\)\\d+)");
            
            MatchCollection matches = findCompondsAndGroups.Matches(input);

            foreach (Match m2 in matches)
            {
                if (m2.Groups[1].Success) //Compound
                {
                    foreach (char c in m2.Groups[1].Value.ToCharArray())
                    {
                        if (Char.IsNumber(c))
                            result.SetFlagAt(textPointer, FormatFlags.Subscript);
                        textPointer++;
                    }
                }
                else if (m2.Groups[2].Success) //Group
                {
                    Regex extractQtyFromInnerText = new Regex("\\((.*)\\)(\\d*)");
                    Match m3 = extractQtyFromInnerText.Match(m2.Groups[2].Value);
                    string innerText = m3.Groups[1].Value;
                    string qty = "";
                    if(m3.Groups[2].Success)
                        qty = m3.Groups[2].Value;

                    textPointer++; //to skip "("
                    textPointer = FormatCompounds(innerText, result, textPointer);
                    textPointer++; //to skip ")"

                    for (int i = 0; i < qty.Length; i++)
                    {
                        result.SetFlagAt(textPointer, FormatFlags.Subscript);
                        textPointer++;
                    }
                }
            }
            return textPointer;
        }

    }
}
