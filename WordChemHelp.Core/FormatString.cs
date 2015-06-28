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
using System.Threading.Tasks;

namespace WordChemHelp.Core
{
    public class FormatString
    {
        public string Content { get; private set; }
        public IList<FormatFlags> FormatMask { get; private set; }

        public FormatString(string content)
        {
            Content = content;
            FormatMask = new FormatFlags[content.Length];
        }

        public FormatString(string content, IList<FormatFlags> formatMask)
        {
            Content = content;
            FormatMask = formatMask;
        }

        public FormatString(string content, int[] formatMask)
            : this(content, formatMask.Select(x => (FormatFlags)x).ToArray())
        {
        }

        public FormatString(string content, string formatMask)
            : this(content, formatMask.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray())
        { 
        }

        public void SetFlagAt(int index, FormatFlags flag)
        {
            FormatMask[index] = flag;
        }

        public void SetFlagAt(int startIndex, int length, FormatFlags flag)
        {
            for (int i = startIndex; i < startIndex + length && i < FormatMask.Count; i++)
                SetFlagAt(i, flag);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if(base.Equals(obj))
                return true;

            FormatString a = obj as FormatString;
            if (a == null)
                return false;

            if (a.Content != this.Content)
                return false;

            if (a.FormatMask.Count != this.FormatMask.Count)
                return false;

            for (int i = 0; i < a.FormatMask.Count; i++)
                if (a.FormatMask[i] != this.FormatMask[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode() ^ FormatMask.GetHashCode();
        }
    }
}
