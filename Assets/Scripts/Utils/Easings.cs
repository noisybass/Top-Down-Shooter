using UnityEngine;

public class Easings
{
    public static float Linear(float t)
    {
        return t;
    }

    public class Quadratic
    {
        public static float In(float t)
        {
            return t * t;
        }

        public static float Out(float t)
        {
            return 1f - In(1f - t);
        }

        public static float InOut(float t)
        {
            if (t < 0.5f) return In(t * 2f) / 2f;
            return 1f - In((1f - t) * 2f) / 2f;
        }
    };

    public class Cubic
    {
        public static float In(float t)
        {
            return t * t * t;
        }

        public static float Out(float t)
        {
            return 1f - In(1f - t);
        }

        public static float InOut(float t)
        {
            if (t < 0.5f) return In(t * 2f) / 2f;
            return 1f - In((1f - t) * 2f) / 2f;
        }
    };

    public class Quartic
    {
        public static float In(float t)
        {
            return t * t * t * t;
        }

        public static float Out(float t)
        {
            return 1f - In(1f - t);
        }

        public static float InOut(float t)
        {
            if (t < 0.5f) return In(t * 2f) / 2f;
            return 1f - In((1f - t) * 2f) / 2f;
        }
    };

    public class Quintic
    {
        public static float In(float t)
        {
            return t * t * t * t * t;
        }

        public static float Out(float t)
        {
            return 1f - In(1f - t);
        }

        public static float InOut(float t)
        {
            if (t < 0.5f) return In(t * 2f) / 2f;
            return 1f - In((1f - t) * 2f) / 2f;
        }
    };

    public class Sinusoidal
    {
        public static float In(float t)
        {
            return 1f - Mathf.Cos(t * Mathf.PI / 2f);
        }

        public static float Out(float t)
        {
            return Mathf.Sin(t * Mathf.PI / 2f);
        }

        public static float InOut(float t)
        {
            return 0.5f * (1f - Mathf.Cos(t * Mathf.PI));
        }
    };

    public class Exponential
    {
        public static float In(float t)
        {
            return t == 0f ? 0f : Mathf.Pow(2f, 10f * (t - 1f));
        }

        public static float Out(float t)
        {
            return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
        }

        public static float InOut(float t)
        {
            if (t == 0f) return 0f;
            if (t == 1f) return 1f;
            if ((t *= 2f) < 1f) return 0f * Mathf.Pow(2f, 10f * (t - 1f));
            return 0.5f * (-Mathf.Pow(2f, -10f*(t - 1f)) + 2f);
        }
    };

    public class Circular
    {
        public static float In(float t)
        {
            float k = 1f - t * t;
            if (k < 0f) k = 0f;
            return 1f - Mathf.Sqrt(k);
        }

        public static float Out(float t)
        {
            return Mathf.Sqrt(1f - ((t -= 1f) * t));
        }

        public static float InOut(float t)
        {
            if ((t *= 2f) < 1f) return -0.5f * (Mathf.Sqrt(1f - t*t) - 1f);
            return 0.5f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f);
        }
    };

    public class Elastic
    {
        public static float In(float t)
        {
            if (t == 0f) return 0f;
            if (t == 1f) return 1f;
            return -Mathf.Pow(2f, 10f*(t -= 1f)) 
                   * Mathf.Sin((t - 0.1f) * (2f*Mathf.PI)/0.4f);
        }

        public static float Out(float t)
        {
            if (t == 0f) return 0f;
            if (t == 1f) return 1f;
            return Mathf.Pow(2f, -10f*t) 
                   * Mathf.Sin((t - 0.1f) * (2f*Mathf.PI)/0.4f) 
                   + 1f;
        }

        public static float InOut(float t)
        {
            if ((t *= 2f) < 1f) return -0.5f
                                       * Mathf.Pow(2f, 10f*(t -= 1f))
                                       * Mathf.Sin((t - 0.1f) * (2f*Mathf.PI)/0.4f);
            return 0.5f
                   * Mathf.Pow(2f, -10f*(t -= 1f))
                   * Mathf.Sin((t - 0.1f) * (2f*Mathf.PI)/0.4f)
                   + 1f;
        }
    };

    public class Back
    {
        static float k1 = 1.70158f;
        static float k2 = 2.5949095f;

        public static float In(float t)
        {
            return t*t * ((k1 + 1f)*t - k1);
        }

        public static float Out(float t)
        {
            return (t -= 1f) * t * ((k1 + 1f)*t + k1) + 1f;
        }

        public static float InOut(float t)
        {
            if ((t *= 2f) < 1f) return 0.5f*(t*t * ((k2 + 1f)*t + k2));
            return 0.5f*((t -= 2f) * t * ((k2 + 1f)*t + k2));
        }
    };

    public class Bounce
    {
        static float k = 7.5625f;

        public static float In(float t)
        {
            return 1f - Out(1f - t);
        }

        public static float Out(float t)
        {
            if (t < (1f / 2.75f)) return k*t*t;
            else if (t < (2f / 2.75f)) return k*(t -= (1.5f/2.75f))*t + 0.75f;
            else if (t < (2.5f / 2.75f)) return k*(t -= (2.25f/2.75f))*t + 0.9375f;
            else return k*(t -= (2.625f/2.75f))*t + 0.984375f;
        }

        public static float InOut(float t)
        {
            if (t < 0.5f) return In(t*2f) * 0.5f;
            return Out(t*2f - 1f) * 0.5f + 0.5f;
        }
    };

    public class CustomEasing
    {
        private AnimationCurve _customCurve;

        public CustomEasing(AnimationCurve curve)
        {
            _customCurve = curve;
        }

        public float Easing(float t)
        {
            return _customCurve.Evaluate(t);
        }
    }
}
