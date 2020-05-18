/*  CTRADER GURU --> Indicator Template 1.0.8

    Homepage    : https://ctrader.guru/
    Telegram    : https://t.me/ctraderguru
    Twitter     : https://twitter.com/cTraderGURU/
    Facebook    : https://www.facebook.com/ctrader.guru/
    YouTube     : https://www.youtube.com/channel/UCKkgbw09Fifj65W5t5lHeCQ
    GitHub      : https://github.com/ctrader-guru

*/

using System;
using cAlgo.API;
using cAlgo.API.Internals;

namespace cAlgo
{

    // --> Estensioni che rendono il codice più leggibile
    #region Extensions

    /// <summary>
    /// Estensione che fornisce metodi aggiuntivi per il simbolo
    /// </summary>
    public static class SymbolExtensions
    {

        /// <summary>
        /// Converte il numero di pips corrente da digits a double
        /// </summary>
        /// <param name="Pips">Il numero di pips nel formato Digits</param>
        /// <returns></returns>
        public static double DigitsToPips(this Symbol MySymbol, double Pips)
        {

            return Math.Round(Pips / MySymbol.PipSize, 2);

        }

        /// <summary>
        /// Converte il numero di pips corrente da double a digits
        /// </summary>
        /// <param name="Pips">Il numero di pips nel formato Double (2)</param>
        /// <returns></returns>
        public static double PipsToDigits(this Symbol MySymbol, double Pips)
        {

            return Math.Round(Pips * MySymbol.PipSize, MySymbol.Digits);

        }

    }

    /// <summary>
    /// Estensione che fornisce metodi aggiuntivi per le Bars
    /// </summary>
    public static class BarsExtensions
    {

        /// <summary>
        /// Converte l'indice di una bar partendo dalla data di apertura
        /// </summary>
        /// <param name="MyTime">La data e l'ora di apertura della candela</param>
        /// <returns></returns>
        public static int GetIndexByDate(this Bars MyBars, DateTime MyTime)
        {

            for (int i = MyBars.ClosePrices.Count - 1; i >= 0; i--)
            {

                if (MyTime == MyBars.OpenTimes[i]) return i;

            }

            return -1;

        }

    }

    #endregion

    [Indicator(IsOverlay = true, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class CenterofGravity : Indicator
    {

        #region Enums

        public enum MyColors
        {

            AliceBlue,
            AntiqueWhite,
            Aqua,
            Aquamarine,
            Azure,
            Beige,
            Bisque,
            Black,
            BlanchedAlmond,
            Blue,
            BlueViolet,
            Brown,
            BurlyWood,
            CadetBlue,
            Chartreuse,
            Chocolate,
            Coral,
            CornflowerBlue,
            Cornsilk,
            Crimson,
            Cyan,
            DarkBlue,
            DarkCyan,
            DarkGoldenrod,
            DarkGray,
            DarkGreen,
            DarkKhaki,
            DarkMagenta,
            DarkOliveGreen,
            DarkOrange,
            DarkOrchid,
            DarkRed,
            DarkSalmon,
            DarkSeaGreen,
            DarkSlateBlue,
            DarkSlateGray,
            DarkTurquoise,
            DarkViolet,
            DeepPink,
            DeepSkyBlue,
            DimGray,
            DodgerBlue,
            Firebrick,
            FloralWhite,
            ForestGreen,
            Fuchsia,
            Gainsboro,
            GhostWhite,
            Gold,
            Goldenrod,
            Gray,
            Green,
            GreenYellow,
            Honeydew,
            HotPink,
            IndianRed,
            Indigo,
            Ivory,
            Khaki,
            Lavender,
            LavenderBlush,
            LawnGreen,
            LemonChiffon,
            LightBlue,
            LightCoral,
            LightCyan,
            LightGoldenrodYellow,
            LightGray,
            LightGreen,
            LightPink,
            LightSalmon,
            LightSeaGreen,
            LightSkyBlue,
            LightSlateGray,
            LightSteelBlue,
            LightYellow,
            Lime,
            LimeGreen,
            Linen,
            Magenta,
            Maroon,
            MediumAquamarine,
            MediumBlue,
            MediumOrchid,
            MediumPurple,
            MediumSeaGreen,
            MediumSlateBlue,
            MediumSpringGreen,
            MediumTurquoise,
            MediumVioletRed,
            MidnightBlue,
            MintCream,
            MistyRose,
            Moccasin,
            NavajoWhite,
            Navy,
            OldLace,
            Olive,
            OliveDrab,
            Orange,
            OrangeRed,
            Orchid,
            PaleGoldenrod,
            PaleGreen,
            PaleTurquoise,
            PaleVioletRed,
            PapayaWhip,
            PeachPuff,
            Peru,
            Pink,
            Plum,
            PowderBlue,
            Purple,
            Red,
            RosyBrown,
            RoyalBlue,
            SaddleBrown,
            Salmon,
            SandyBrown,
            SeaGreen,
            SeaShell,
            Sienna,
            Silver,
            SkyBlue,
            SlateBlue,
            SlateGray,
            Snow,
            SpringGreen,
            SteelBlue,
            Tan,
            Teal,
            Thistle,
            Tomato,
            Transparent,
            Turquoise,
            Violet,
            Wheat,
            White,
            WhiteSmoke,
            Yellow,
            YellowGreen

        }

        #endregion

        #region Identity

        /// <summary>
        /// Nome del prodotto, identificativo, da modificare con il nome della propria creazione
        /// </summary>
        public const string NAME = "Center of Gravity";

        /// <summary>
        /// La versione del prodotto, progressivo, utilie per controllare gli aggiornamenti se viene reso disponibile sul sito ctrader.guru
        /// </summary>
        public const string VERSION = "1.0.1";

        #endregion

        #region Params

        /// <summary>
        /// Identità del prodotto nel contesto di ctrader.guru
        /// </summary>
        [Parameter(NAME + " " + VERSION, Group = "Identity", DefaultValue = "https://ctrader.guru/product/center-of-gravity/")]
        public string ProductInfo { get; set; }

        [Parameter("Degree", Group = "Params", DefaultValue = 3.0, MinValue = 1, MaxValue = 4)]
        public int Degree { get; set; }

        [Parameter("Period", Group = "Params", DefaultValue = 120)]
        public int Period { get; set; }

        [Parameter("Standard Deviation 1", Group = "Params", DefaultValue = 1.3)]
        public double StrdDev { get; set; }

        [Parameter("Standard Deviation 2", Group = "Params", DefaultValue = 2.3)]
        public double StrdDev2 { get; set; }

        [Parameter("Standard Deviation 3", Group = "Params", DefaultValue = 3.3)]
        public double StrdDev3 { get; set; }

        [Output("Center", LineColor = "Gray", LineStyle = LineStyle.Lines)]
        public IndicatorDataSeries Prc { get; set; }

        [Output("Support 1", LineColor = "Blue", LineStyle = LineStyle.DotsRare)]
        public IndicatorDataSeries Sql { get; set; }

        [Output("Support 2", LineColor = "Blue", LineStyle = LineStyle.Lines)]
        public IndicatorDataSeries Sql2 { get; set; }

        [Output("Support 3", LineColor = "Blue", LineStyle = LineStyle.Solid)]
        public IndicatorDataSeries Sql3 { get; set; }

        [Output("Resistance 1", LineColor = "Red", LineStyle = LineStyle.DotsRare)]
        public IndicatorDataSeries Sqh { get; set; }

        [Output("Resistance 2", LineColor = "Red", LineStyle = LineStyle.Lines)]
        public IndicatorDataSeries Sqh2 { get; set; }

        [Output("Resistance 3", LineColor = "Red", LineStyle = LineStyle.Solid)]
        public IndicatorDataSeries Sqh3 { get; set; }

        #endregion

        #region Property

        public int Ix { get; set; }

        private readonly double[,] ai = new double[10, 10];
        private readonly double[] b = new double[10];
        private readonly double[] x = new double[10];
        private readonly double[] sx = new double[10];
        private double sum;
        private int ip;
        private int p;
        private int n;
        private double qq;
        private double mm;
        private double tt;
        private int ii;
        private int jj;
        private int kk;
        private int ll;
        private int nn;
        private double sq;
        private double sq2;
        private double sq3;
        private readonly int i0 = 0;
        private int mi;

        #endregion

        #region Indicator Events

        /// <summary>
        /// Viene generato all'avvio dell'indicatore, si inizializza l'indicatore
        /// </summary>
        protected override void Initialize()
        {

            // --> Stampo nei log la versione corrente
            Print("{0} : {1}", NAME, VERSION);

        }

        /// <summary>
        /// Generato ad ogni tick, vengono effettuati i calcoli dell'indicatore
        /// </summary>
        /// <param name="index">L'indice della candela in elaborazione</param>
        public override void Calculate(int index)
        {

            if (!IsLastBar || index < Period)
                return;

            int i = index;
            Ix = i;
            ip = Period;
            p = ip;
            sx[1] = p + 1;
            nn = Degree + 1;

            //--> sx
            for (mi = 1; mi <= nn * 2 - 2; mi++)
            {
                sum = 0;
                for (n = i0; n <= i0 + p; n++)
                {
                    sum += Math.Pow(n, mi);
                }
                sx[mi + 1] = sum;
            }

            //--> syx
            for (mi = 1; mi <= nn; mi++)
            {
                sum = 0.0;
                for (n = i0; n <= i0 + p; n++)
                {
                    if (mi == 1)
                        sum += Bars.ClosePrices[index - n];
                    else
                        sum += Bars.ClosePrices[index - n] * Math.Pow(n, mi - 1);
                }
                b[mi] = sum;
            }

            //--> Matrix
            for (jj = 1; jj <= nn; jj++)
            {
                for (ii = 1; ii <= nn; ii++)
                {
                    kk = ii + jj - 1;
                    ai[ii, jj] = sx[kk];
                }
            }

            //--> Gauss
            for (kk = 1; kk <= nn - 1; kk++)
            {
                ll = 0;
                mm = 0;
                for (ii = kk; ii <= nn; ii++)
                {
                    if (Math.Abs(ai[ii, kk]) > mm)
                    {
                        mm = Math.Abs(ai[ii, kk]);
                        ll = ii;
                    }
                }
                if (ll == 0)
                    return;
                if (ll != kk)
                {
                    for (jj = 1; jj <= nn; jj++)
                    {
                        tt = ai[kk, jj];
                        ai[kk, jj] = ai[ll, jj];
                        ai[ll, jj] = tt;
                    }
                    tt = b[kk];
                    b[kk] = b[ll];
                    b[ll] = tt;
                }
                for (ii = kk + 1; ii <= nn; ii++)
                {
                    qq = ai[ii, kk] / ai[kk, kk];
                    for (jj = 1; jj <= nn; jj++)
                    {
                        if (jj == kk)
                            ai[ii, jj] = 0;
                        else
                            ai[ii, jj] = ai[ii, jj] - qq * ai[kk, jj];
                    }
                    b[ii] = b[ii] - qq * b[kk];
                }
            }

            x[nn] = b[nn] / ai[nn, nn];
            for (ii = nn - 1; ii >= 1; ii--)
            {
                tt = 0;
                for (jj = 1; jj <= nn - ii; jj++)
                {
                    tt = tt + ai[ii, ii + jj] * x[ii + jj];
                    x[ii] = (1 / ai[ii, ii]) * (b[ii] - tt);
                }
            }
            sq = 0.0;
            sq2 = 0.0;
            sq3 = 0.0;
            for (n = i0; n <= i0 + p; n++)
            {
                sum = 0;
                for (kk = 1; kk <= Degree; kk++)
                {
                    sum += x[kk + 1] * Math.Pow(n, kk);
                }

                Prc[index - n] = (x[1] + sum);
                sq += Math.Pow(Bars.ClosePrices[index - n] - Prc[index - n], 2);
                sq2 = sq;
                sq3 = sq;
            }

            sq = Math.Sqrt(sq / (p + 1)) * StrdDev;
            sq2 = Math.Sqrt(sq2 / (p + 1)) * StrdDev2;
            sq3 = Math.Sqrt(sq3 / (p + 1)) * StrdDev3;
            for (n = 0; n <= Period; n++)
            {

                Sqh[index - n] = Prc[index - n] + sq;
                Sqh2[index - n] = Prc[index - n] + sq2;
                Sqh3[index - n] = Prc[index - n] + sq3;
                Sql[index - n] = Prc[index - n] - sq;
                Sql2[index - n] = Prc[index - n] - sq2;
                Sql3[index - n] = Prc[index - n] - sq3;
            }

            Prc[index - Period] = double.NaN;
            Sqh[index - Period] = double.NaN;
            Sqh2[index - Period] = double.NaN;
            Sqh3[index - Period] = double.NaN;
            Sql[index - Period] = double.NaN;
            Sql2[index - Period] = double.NaN;
            Sql3[index - Period] = double.NaN;
        }

    }

    #endregion

    #region Private Methods

    // --> Seguiamo la signature con underscore "_mioMetodo()"

    #endregion

}