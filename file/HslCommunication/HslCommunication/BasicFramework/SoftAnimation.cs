namespace HslCommunication.BasicFramework
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class SoftAnimation
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static int <TimeFragment>k__BackingField = 20;

        public static void BeginBackcolorAnimation(Control control, Color color, int time)
        {
            if (control.BackColor != color)
            {
                Func<Control, Color> func = m => m.BackColor;
                Action<Control, Color> action = (m, n) => m.BackColor = n;
                object[] state = new object[] { control, color, time, func, action };
                ThreadPool.QueueUserWorkItem(new WaitCallback(SoftAnimation.ThreadPoolColorAnimation), state);
            }
        }

        private static byte GetValue(byte Start, byte End, int i, int count)
        {
            if (Start == End)
            {
                return Start;
            }
            return (byte) ((((End - Start) * i) / count) + Start);
        }

        private static float GetValue(float Start, float End, int i, int count)
        {
            if (Start == End)
            {
                return Start;
            }
            return ((((End - Start) * i) / ((float) count)) + Start);
        }

        private static void ThreadPoolColorAnimation(object obj)
        {
            object[] objArray = obj as object[];
            Control control = objArray[0] as Control;
            Color color = (Color) objArray[1];
            int num = (int) objArray[2];
            Func<Control, Color> func = (Func<Control, Color>) objArray[3];
            Action<Control, Color> setcolor = (Action<Control, Color>) objArray[4];
            int count = ((num + TimeFragment) - 1) / TimeFragment;
            Color color_old = func(control);
            try
            {
                int num2;
                Control expressionStack_F2_0;
                for (int i = 0; i < count; i = num2 + 1)
                {
                    control.Invoke(delegate {
                        byte red = GetValue(color_old.R, color.R, i, count);
                        byte green = GetValue(color_old.G, color.G, i, count);
                        setcolor(control, Color.FromArgb(red, green, GetValue(color_old.B, color.B, i, count)));
                    });
                    Thread.Sleep(TimeFragment);
                    num2 = i;
                }
                if (control != null)
                {
                    expressionStack_F2_0 = control;
                    goto Label_00F2;
                }
                else
                {
                    Control expressionStack_EF_0 = control;
                }
                return;
            Label_00F2:
                expressionStack_F2_0.Invoke(() => setcolor(control, color));
            }
            catch
            {
            }
        }

        private static void ThreadPoolFloatAnimation(object obj)
        {
            object[] objArray = obj as object[];
            Control control = objArray[0] as Control;
            Control control = control;
            lock (control)
            {
                int num2;
                float value = (float) objArray[1];
                int num = (int) objArray[2];
                Func<Control, float> func = (Func<Control, float>) objArray[3];
                Action<Control, float> setValue = (Action<Control, float>) objArray[4];
                int count = ((num + TimeFragment) - 1) / TimeFragment;
                float value_old = func(control);
                for (int i = 0; i < count; i = num2 + 1)
                {
                    if (control.IsHandleCreated && !control.IsDisposed)
                    {
                        control.Invoke(() => setValue(control, GetValue(value_old, value, i, count)));
                    }
                    else
                    {
                        goto Label_01B1;
                    }
                    Thread.Sleep(TimeFragment);
                    num2 = i;
                }
                if (control.IsHandleCreated && !control.IsDisposed)
                {
                    control.Invoke(() => setValue(control, value));
                }
            Label_01B1:;
            }
        }

        private static int TimeFragment
        {
            [CompilerGenerated]
            get
            {
                return <TimeFragment>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <TimeFragment>k__BackingField = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SoftAnimation.<>c <>9 = new SoftAnimation.<>c();
            public static Func<Control, Color> <>9__4_0;
            public static Action<Control, Color> <>9__4_1;

            internal Color <BeginBackcolorAnimation>b__4_0(Control m)
            {
                return m.BackColor;
            }

            internal void <BeginBackcolorAnimation>b__4_1(Control m, Color n)
            {
                m.BackColor = n;
            }
        }
    }
}

