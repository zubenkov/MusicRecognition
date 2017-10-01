using System;

namespace MusicRecognition.Business.Helpers
{
    class Complex
    {
        public double Re { get; set; } // the real part
        private double Im { get; set; } // the imaginary part

        // create a new object with the given real and imaginary parts
        public Complex(double real, double imag)
        {
            Re = real;
            Im = imag;
        }

        // return a string representation of the invoking Complex object
        public String ToString()
        {
            if (Im == 0)
                return Re + "";
            if (Re == 0)
                return Im + "i";
            if (Im < 0)
                return Re + " - " + (-Im) + "i";
            return Re + " + " + Im + "i";
        }

        // return abs/modulus/magnitude and angle/phase/argument
        public double Abs()
        {
            return Math.Sqrt(Re * Re + Im * Im);
        } // Math.sqrt(re*re + im*im)

        public double phase()
        {
            return Math.Atan2(Im, Re);
        } // between -pi and pi

        // return a new Complex object whose value is (this + b)
        public Complex Plus(Complex b)
        {
            Complex a = this; // invoking object
            double real = a.Re + b.Re;
            double imag = a.Im + b.Im;
            return new Complex(real, imag);
        }

        // return a new Complex object whose value is (this - b)
        public Complex Minus(Complex b)
        {
            Complex a = this;
            double real = a.Re - b.Re;
            double imag = a.Im - b.Im;
            return new Complex(real, imag);
        }

        // return a new Complex object whose value is (this * b)
        public Complex Times(Complex b)
        {
            Complex a = this;
            double real = a.Re * b.Re - a.Im * b.Im;
            double imag = a.Re * b.Im + a.Im * b.Re;
            return new Complex(real, imag);
        }

        // scalar multiplication
        // return a new object whose value is (this * alpha)
        public Complex Times(double alpha)
        {
            return new Complex(alpha * Re, alpha * Im);
        }

        // return a new Complex object whose value is the conjugate of this
        public Complex Conjugate()
        {
            return new Complex(Re, -Im);
        }

        // return a new Complex object whose value is the reciprocal of this
        public Complex Reciprocal()
        {
            double scale = Re * Re + Im * Im;
            return new Complex(Re / scale, -Im / scale);
        }

        // return the real or imaginary part


        // return a / b
        public Complex Divides(Complex b)
        {
            Complex a = this;
            return a.Times(b.Reciprocal());
        }

        // return a new Complex object whose value is the complex exponential of
        // this
        public Complex Exp()
        {
            return new Complex(Math.Exp(Re) * Math.Cos(Im), Math.Exp(Re)
                    * Math.Sin(Im));
        }

        // return a new Complex object whose value is the complex sine of this
        public Complex Sin()
        {
            return new Complex(Math.Sin(Re) * Math.Cosh(Im), Math.Cos(Re)
                    * Math.Sinh(Im));
        }

        // return a new Complex object whose value is the complex cosine of this
        public Complex Cos()
        {
            return new Complex(Math.Cos(Re) * Math.Cosh(Im), -Math.Sin(Re)
                    * Math.Sinh(Im));
        }

        // return a new Complex object whose value is the complex tangent of this
        public Complex Tan()
        {
            return Sin().Divides(Cos());
        }

        // a static version of plus
        public static Complex Plus(Complex a, Complex b)
        {
            double real = a.Re + b.Re;
            double imag = a.Im + b.Im;
            Complex sum = new Complex(real, imag);
            return sum;
        }
    }
}
