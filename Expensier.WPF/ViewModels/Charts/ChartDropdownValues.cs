﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.ViewModels.Charts
{
    public class ChartDropdownValues
    {
        public enum ChartFrequency
        {
            Monthly,
            Yearly
        }

        public enum SortingFunctions
        {
            Date,
            Amount,
            Asceding,
            Descending
        }
    }
}
