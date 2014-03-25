using System;
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
    public class QueryRefineModel
    {
        public string source { get; set; }
        public int TimeOut { get; set; }
        public int Offset { get; set; }
        public string Keyword { get; set; }
        public int Resultsperbiz { get; set; }
        public string Ranking { get; set; }
        
        public string ConstructRefinementParam()
        {
            string refinement = string.Empty;
            if (source != string.Empty)
                refinement += ";source:" + source;
            if (TimeOut != 0)
                refinement += (";timeout:" + TimeOut);
            if (Offset != 0)
                refinement += (";offset:" + Offset);
            if (Keyword != string.Empty)
                refinement += ";keyword:" + Keyword;
            if (Resultsperbiz != -1)
                refinement += (";resultsperbiz:" + Resultsperbiz);
            if (Ranking != string.Empty)
                refinement += ";ranking:" + Ranking;
            return refinement.Substring(refinement.IndexOf(';')+1);
        }
    }
}
