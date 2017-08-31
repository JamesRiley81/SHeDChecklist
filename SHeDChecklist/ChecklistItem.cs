using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHeDChecklist
{
    public class ChecklistItem
    {
        public string name;
        public string description;
        public string documentation;
        public string initials;
        public string day;
        public string time;
        public bool complete;
        public ChecklistItem() { name = null; description = null; documentation = null; }
        public ChecklistItem(string n, string d, string doc = "") { name = n; description = d; documentation = doc; }
        public DateTime dateTime;
    }
    public class DailychecklistItem
    {
        public bool c1;
        public bool c2;
        public bool c3;
        public bool c4;
        public bool c5;
        public DailychecklistItem(bool i1, bool i2, bool i3, bool i4, bool i5) { c1 = i1;  c2 = i2;  c3 = i3;  c4 = i4; c5 = i5; }
    }
}
