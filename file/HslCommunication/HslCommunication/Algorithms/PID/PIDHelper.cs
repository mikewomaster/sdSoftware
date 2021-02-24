namespace HslCommunication.Algorithms.PID
{
    using System;

    public class PIDHelper
    {
        private double deadband;
        private double err;
        private double err_last;
        private double err_next;
        private int index;
        private double MAXLIM;
        private double MINLIM;
        private double prakd;
        private double praki;
        private double prakp;
        private double prvalue;
        private double setValue;
        private int UMAX;
        private int UMIN;

        public PIDHelper()
        {
            this.PidInit();
        }

        public double PidCalculate()
        {
            this.err_next = this.err_last;
            this.err_last = this.err;
            this.err = this.SetValue - this.prvalue;
            this.prvalue += this.prakp * (((this.err - this.err_last) + (this.praki * this.err)) + (this.prakd * ((this.err - (2.0 * this.err_last)) + this.err_next)));
            if (this.prvalue > this.MAXLIM)
            {
                this.prvalue = this.MAXLIM;
            }
            if (this.prvalue < this.MINLIM)
            {
                this.prvalue = this.MINLIM;
            }
            return this.prvalue;
        }

        private void PidInit()
        {
            this.prakp = 0.0;
            this.praki = 0.0;
            this.prakd = 0.0;
            this.prvalue = 0.0;
            this.err = 0.0;
            this.err_last = 0.0;
            this.err_next = 0.0;
            this.MAXLIM = double.MaxValue;
            this.MINLIM = double.MinValue;
            this.UMAX = 310;
            this.UMIN = -100;
            this.deadband = 2.0;
        }

        public double DeadBand
        {
            get
            {
                return this.deadband;
            }
            set
            {
                this.deadband = value;
            }
        }

        public double Kd
        {
            get
            {
                return this.prakd;
            }
            set
            {
                this.prakd = value;
            }
        }

        public double Ki
        {
            get
            {
                return this.praki;
            }
            set
            {
                this.praki = value;
            }
        }

        public double Kp
        {
            get
            {
                return this.prakp;
            }
            set
            {
                this.prakp = value;
            }
        }

        public double MaxLimit
        {
            get
            {
                return this.MAXLIM;
            }
            set
            {
                this.MAXLIM = value;
            }
        }

        public double MinLimit
        {
            get
            {
                return this.MINLIM;
            }
            set
            {
                this.MINLIM = value;
            }
        }

        public double SetValue
        {
            get
            {
                return this.setValue;
            }
            set
            {
                this.setValue = value;
            }
        }
    }
}

