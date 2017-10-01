using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRecognition.Business.Helpers
{
    class FFT
    {
        // compute the FFT of x[], assuming its length is a power of 2
        public static Complex[] Fft(Complex[] x)
        {
            int N = x.Length;

            // base case
            if (N == 1)
                return new Complex[] { x[0] };

            // radix 2 Cooley-Tukey FFT
            if (N % 2 != 0)
            {
                throw new Exception("N is not a power of 2");
            }

            // fft of even terms
            Complex[] even = new Complex[N / 2];
            for (int k = 0; k < N / 2; k++)
            {
                even[k] = x[2 * k];
            }
            Complex[] q = Fft(even);

            // fft of odd terms
            Complex[] odd = even; // reuse the array
            for (int k = 0; k < N / 2; k++)
            {
                odd[k] = x[2 * k + 1];
            }
            Complex[] r = Fft(odd);

            // combine
            Complex[] y = new Complex[N];
            for (int k = 0; k < N / 2; k++)
            {
                double kth = -2 * k * Math.PI / N;
                Complex wk = new Complex(Math.Cos(kth), Math.Sin(kth));
                y[k] = q[k].Plus(wk.Times(r[k]));
                y[k + N / 2] = q[k].Minus(wk.Times(r[k]));
            }
            return y;
        }

        // compute the inverse FFT of x[], assuming its length is a power of 2
        public static Complex[] Ifft(Complex[] x)
        {
            int N = x.Length;
            Complex[] y = new Complex[N];

            // take conjugate
            for (int i = 0; i < N; i++)
            {
                y[i] = x[i].Conjugate();
            }

            // compute forward FFT
            y = Fft(y);

            // take conjugate again
            for (int i = 0; i < N; i++)
            {
                y[i] = y[i].Conjugate();
            }

            // divide by N
            for (int i = 0; i < N; i++)
            {
                y[i] = y[i].Times(1.0 / N);
            }

            return y;

        }

        // compute the circular convolution of x and y
        public static Complex[] Cconvolve(Complex[] x, Complex[] y)
        {

            // should probably pad x and y with 0s so that they have same length
            // and are powers of 2
            if (x.Length != y.Length)
            {
                throw new Exception("Dimensions don't agree");
            }

            int N = x.Length;

            // compute FFT of each sequence
            Complex[] a = Fft(x);
            Complex[] b = Fft(y);

            // point-wise multiply
            Complex[] c = new Complex[N];
            for (int i = 0; i < N; i++)
            {
                c[i] = a[i].Times(b[i]);
            }

            // compute inverse FFT
            return Ifft(c);
        }

        // compute the linear convolution of x and y
        public static Complex[] Convolve(Complex[] x, Complex[] y)
        {
            Complex ZERO = new Complex(0, 0);

            Complex[] a = new Complex[2 * x.Length];
            for (int i = 0; i < x.Length; i++)
                a[i] = x[i];
            for (int i = x.Length; i < 2 * x.Length; i++)
                a[i] = ZERO;

            Complex[] b = new Complex[2 * y.Length];
            for (int i = 0; i < y.Length; i++)
                b[i] = y[i];
            for (int i = y.Length; i < 2 * y.Length; i++)
                b[i] = ZERO;

            return Cconvolve(a, b);
        }
    }
}
