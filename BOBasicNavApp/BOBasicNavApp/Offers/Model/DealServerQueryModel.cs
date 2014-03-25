﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BOBasicNavApp.Offers.Model
{
    public class DealServerQueryModel
    {
        public QueryRefineModel Refinement { get; set; }
        public int Count { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
