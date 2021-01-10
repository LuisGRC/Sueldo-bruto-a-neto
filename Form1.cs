using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sueldo_bruto_a_neto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            double sueldoBruto = Double.Parse(this.maskedTextBox1.Text);
            double limiteInferior = CalculaLimiteInferior(sueldoBruto);
            double excedente = sueldoBruto - limiteInferior;
            double porcentaje = ObtenPorcentaje(limiteInferior);
            double impuestoMarginal = CalculaImpuestoMarginal(excedente, porcentaje);
            double cuotaFija = ObtenCuotaFija(limiteInferior);
            double isr = impuestoMarginal + cuotaFija;
            double imss = 0.0;
            if (checkBox1.Checked)
            {
                imss = CalculaImss(sueldoBruto);
            }
            double sueldoNeto = CalculaSueldoNeto(isr, imss, sueldoBruto);

            this.label3.Text = "$ " + limiteInferior.ToString();
            this.label4.Text = "$ " + excedente;
            this.label6.Text = porcentaje + "%";
            this.label8.Text = "$ " + impuestoMarginal;
            this.label10.Text = "$ " + cuotaFija;
            this.label12.Text = "$ " + isr;
            this.label14.Text = "$ " + imss;
            this.label18.Text = "$ " + sueldoNeto;
        }

        private double CalculaSueldoNeto(double isr, double imss, double sueldoBruto)
        {
            return sueldoBruto - (isr + imss);
        }

        public double CalculaLimiteInferior(double sueldoBruto)
        {
            double limiteInferiorCalculado = 0;
            double[] limiteInferior = {0.01, 644.60, 5470.93, 9614.66, 11176.63, 13381.49, 26988.51, 42537.59, 81211.26, 108281.68, 324845.02};
            for(int i = 0; sueldoBruto > limiteInferior[i]; i++)
            {
                limiteInferiorCalculado = limiteInferior[i];
            }
            return limiteInferiorCalculado;
        }

        public double ObtenPorcentaje(double limiteInferior)
        {
            Dictionary<double, double> diccionario = new Dictionary<double, double>
            {
                { 0.01, 1.92 },
                { 644.60, 6.4 },
                { 5470.93, 10.88 },
                { 9614.66, 16 },
                { 11176.63, 17.92 },
                { 13381.49, 21.36 },
                { 26988.51, 23.52 },
                { 42537.59, 30 },
                { 81211.26, 32 },
                { 108281.68, 34 },
                { 324845.02, 35 }
            };
            if (diccionario.ContainsKey(limiteInferior)){
                return diccionario[limiteInferior];
            }
            else
            {
                return 0.0;
            }
        }

        public double CalculaImpuestoMarginal(double excedente, double porcentaje)
        {
            return Math.Round(((excedente * porcentaje) / 100), 2);
        }

        public double ObtenCuotaFija(double limiteInferior)
        {
            Dictionary<double, double> diccionario = new Dictionary<double, double>
            {
                { 0.01, 0 },
                { 644.60, 12.38 },
                { 5470.93, 321.26 },
                { 9614.66, 772.10 },
                { 11176.63, 1022.01 },
                { 13381.49, 1417.12 },
                { 26988.51, 4323.59 },
                { 42537.59, 7980.72 },
                { 81211.26, 19582.83 },
                { 108281.68, 28245.36 },
                { 324845.02, 101876.90 }
            };
            if (diccionario.ContainsKey(limiteInferior))
            {
                return diccionario[limiteInferior];
            }
            else
            {
                return 0.0;
            }
        }

        public double CalculaImss(double sueldoBruto)
        {
            return Math.Round(((sueldoBruto * 2.82) / 100), 2);
        }
    }
}
