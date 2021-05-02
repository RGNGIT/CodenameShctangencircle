/*
internal class GaugeCalculations
{
    // Fields
    private const double E = 1E-06;
    public const int MINIMAL_VALUE = 0x19;
    public const int MAXIMAL_VALUE = 0x70;
    public const int GROUP_COUNT = 5;
    public static int N = 0x53;
    private static double A0 = 0.5;
    private static double Amax = 100.0;
    public static double[] Delta = new double[] { 0.005, 0.01, 0.1, 0.5, 10.0 };
    public static double R2 = 0.0;
    public static double R2prev = 0.0;
    public static double F = 0.0;
    public static double Fprev = 0.0;
    public static List<int[]> FSets = new List<int[]>();
    public static List<int[]> RSets = new List<int[]>();
    public static List<int[]> Sets = new List<int[]>();
    public static double[] Sizes;
    public static double[] Sizes2;
    public static List<CompositeBlock> CompositeBlocks = new List<CompositeBlock>();
    public static long CurrentStep = 0L;
    public static string Name;
    public static long MaximalStep = 0L;
    public static double CompositeStep = 0.005;
    public static int SequenceCount = 0;
    public static int SequenceCount2 = 0;
    public static double SequenceBegin = 0.0;
    public static double SequenceBegin2 = 0.0;
    public static double SequenceEnd = 0.0;
    public static double SequenceEnd2 = 0.0;
    public static double KE = 0.0;
    public static double KE2 = 0.0;
    public static double SummarySizes = 0.0;
    public static double SummarySizes2 = 0.0;
    public static double SummaryCount = 0.0;
    public static double SummaryCount2 = 0.0;
    public static TimeSpan SummaryTime;
    public static ProgressBar pBar;
    public static Label lblOperation;
    public static Label lblStatus;
    public static List<string> TempFiles = new List<string>();

    // Methods
    private static IEnumerable<List<CompositeBlock>> AdditionalGenerateComposite()
    {
        List<CompositeBlock> iteratorVariable0 = new List<CompositeBlock>(((int) Math.Pow((double) N, 3.0)) + 1);
        byte iteratorVariable1 = 0;
    Label_PostSwitchInIterator:;
        if (iteratorVariable1 < N)
        {
            byte index = (byte) (iteratorVariable1 + 1);
            while (index < N)
            {
                byte num2 = (byte) (index + 1);
                while (true)
                {
                    if (num2 >= N)
                    {
                        index = (byte) (index + 1);
                        break;
                    }
                    byte num3 = (byte) (num2 + 1);
                    while (true)
                    {
                        if (num3 >= N)
                        {
                            num2 = (byte) (num2 + 1);
                            break;
                        }
                        iteratorVariable0.Add(new CompositeBlock(Math.Round((double) (((Sizes2[iteratorVariable1] + Sizes2[index]) + Sizes2[num2]) + Sizes2[num3]), 4), (byte) (iteratorVariable1 + 1), (byte) (index + 1), (byte) (num2 + 1), (byte) (num3 + 1), 0));
                        num3 = (byte) (num3 + 1);
                    }
                }
            }
            pBar.Value = iteratorVariable1 + 1;
            yield return iteratorVariable0;
            iteratorVariable0.Clear();
            GC.Collect();
            iteratorVariable1 = (byte) (iteratorVariable1 + 1);
            goto Label_PostSwitchInIterator;
        }
        byte iteratorVariable2 = 0;
        while (true)
        {
            byte iteratorVariable3;
            while (true)
            {
                if (iteratorVariable2 < N)
                {
                    iteratorVariable3 = (byte) (iteratorVariable2 + 1);
                }
                else
                {
                    break;
                }
                break;
            }
            while (iteratorVariable3 < N)
            {
                byte index = (byte) (iteratorVariable3 + 1);
                while (index < N)
                {
                    byte num5 = (byte) (index + 1);
                    while (true)
                    {
                        if (num5 >= N)
                        {
                            index = (byte) (index + 1);
                            break;
                        }
                        byte num6 = (byte) (num5 + 1);
                        while (true)
                        {
                            if (num6 >= N)
                            {
                                num5 = (byte) (num5 + 1);
                                break;
                            }
                            iteratorVariable0.Add(new CompositeBlock(Math.Round((double) ((((Sizes2[iteratorVariable2] + Sizes2[iteratorVariable3]) + Sizes2[index]) + Sizes2[num5]) + Sizes2[num6]), 4), (byte) (iteratorVariable2 + 1), (byte) (iteratorVariable3 + 1), (byte) (index + 1), (byte) (num5 + 1), (byte) (num6 + 1)));
                            num6 = (byte) (num6 + 1);
                        }
                    }
                }
                pBar.Value = iteratorVariable2 + 1;
                yield return iteratorVariable0;
                iteratorVariable0.Clear();
                GC.Collect();
                iteratorVariable3 = (byte) (iteratorVariable3 + 1);
            }
            iteratorVariable2 = (byte) (iteratorVariable2 + 1);
        }
    }

    private static IEnumerable<List<CompositeBlock>> AverageGenerateComposite(int p)
    {
        byte iteratorVariable3;
        byte iteratorVariable2;
        List<CompositeBlock> iteratorVariable0 = new List<CompositeBlock>(((int) (Math.Pow((double) N, 4.0) / ((double) p))) + 1);
        byte iteratorVariable1 = 0;
        goto TR_0028;
    TR_0003:
        iteratorVariable3 = (byte) (iteratorVariable3 + 1);
    TR_0012:
        while (true)
        {
            if (iteratorVariable3 >= N)
            {
                pBar.Value = iteratorVariable2 + 1;
                iteratorVariable2 = (byte) (iteratorVariable2 + 1);
                break;
            }
            byte index = (byte) (iteratorVariable3 + 1);
            while (true)
            {
                if (index >= N)
                {
                    if (iteratorVariable0.Count > (iteratorVariable0.Capacity / 2))
                    {
                        yield return iteratorVariable0;
                        iteratorVariable0.Clear();
                        GC.Collect();
                    }
                    break;
                }
                byte num5 = (byte) (index + 1);
                while (true)
                {
                    if (num5 >= N)
                    {
                        index = (byte) (index + 1);
                        break;
                    }
                    byte num6 = (byte) (num5 + 1);
                    while (true)
                    {
                        if (num6 >= N)
                        {
                            num5 = (byte) (num5 + 1);
                            break;
                        }
                        iteratorVariable0.Add(new CompositeBlock(Math.Round((double) ((((Sizes2[iteratorVariable2] + Sizes2[iteratorVariable3]) + Sizes2[index]) + Sizes2[num5]) + Sizes2[num6]), 4), (byte) (iteratorVariable2 + 1), (byte) (iteratorVariable3 + 1), (byte) (index + 1), (byte) (num5 + 1), (byte) (num6 + 1)));
                        num6 = (byte) (num6 + 1);
                    }
                }
            }
            goto TR_0003;
        }
    TR_0015:
        while (true)
        {
            if (iteratorVariable2 < N)
            {
                iteratorVariable3 = (byte) (iteratorVariable2 + 1);
            }
            else
            {
                goto TR_0003;
            }
            break;
        }
        goto TR_0012;
    TR_0028:
        while (true)
        {
            if (iteratorVariable1 < N)
            {
                byte index = (byte) (iteratorVariable1 + 1);
                while (true)
                {
                    if (index >= N)
                    {
                        pBar.Value = iteratorVariable1 + 1;
                        if (iteratorVariable0.Count > (iteratorVariable0.Capacity / 2))
                        {
                            yield return iteratorVariable0;
                            iteratorVariable0.Clear();
                            GC.Collect();
                        }
                        break;
                    }
                    byte num2 = (byte) (index + 1);
                    while (true)
                    {
                        if (num2 >= N)
                        {
                            index = (byte) (index + 1);
                            break;
                        }
                        byte num3 = (byte) (num2 + 1);
                        while (true)
                        {
                            if (num3 >= N)
                            {
                                num2 = (byte) (num2 + 1);
                                break;
                            }
                            iteratorVariable0.Add(new CompositeBlock(Math.Round((double) (((Sizes2[iteratorVariable1] + Sizes2[index]) + Sizes2[num2]) + Sizes2[num3]), 4), (byte) (iteratorVariable1 + 1), (byte) (index + 1), (byte) (num2 + 1), (byte) (num3 + 1), 0));
                            num3 = (byte) (num3 + 1);
                        }
                    }
                }
            }
            else
            {
                iteratorVariable2 = 0;
                goto TR_0015;
            }
            break;
        }
        iteratorVariable1 = (byte) (iteratorVariable1 + 1);
        goto TR_0028;
    }

    private static IEnumerable<List<CompositeBlock>> BigGenerateComposite()
    {
        byte iteratorVariable1;
        List<CompositeBlock> iteratorVariable0 = new List<CompositeBlock>();
        byte index = 0;
        while (index < N)
        {
            byte num2 = (byte) (index + 1);
            while (true)
            {
                if (num2 >= N)
                {
                    pBar.Value = index + 1;
                    index = (byte) (index + 1);
                    break;
                }
                byte num3 = (byte) (num2 + 1);
                while (true)
                {
                    if (num3 >= N)
                    {
                        num2 = (byte) (num2 + 1);
                        break;
                    }
                    byte num4 = (byte) (num3 + 1);
                    while (true)
                    {
                        if (num4 >= N)
                        {
                            num3 = (byte) (num3 + 1);
                            break;
                        }
                        iteratorVariable0.Add(new CompositeBlock(Math.Round((double) (((Sizes2[index] + Sizes2[num2]) + Sizes2[num3]) + Sizes2[num4]), 4), (byte) (index + 1), (byte) (num2 + 1), (byte) (num3 + 1), (byte) (num4 + 1), 0));
                        num4 = (byte) (num4 + 1);
                    }
                }
            }
        }
        yield return iteratorVariable0;
        iteratorVariable0.Clear();
        GC.Collect();
        iteratorVariable1 = 0;
        while (true)
        {
            if (iteratorVariable1 >= N)
            {
                yield break;
            }
            byte index = (byte) (iteratorVariable1 + 1);
            while (index < N)
            {
                byte num6 = (byte) (index + 1);
                while (true)
                {
                    if (num6 >= N)
                    {
                        index = (byte) (index + 1);
                        break;
                    }
                    byte num7 = (byte) (num6 + 1);
                    while (true)
                    {
                        if (num7 >= N)
                        {
                            num6 = (byte) (num6 + 1);
                            break;
                        }
                        byte num8 = (byte) (num7 + 1);
                        while (true)
                        {
                            if (num8 >= N)
                            {
                                num7 = (byte) (num7 + 1);
                                break;
                            }
                            iteratorVariable0.Add(new CompositeBlock(Math.Round((double) ((((Sizes2[iteratorVariable1] + Sizes2[index]) + Sizes2[num6]) + Sizes2[num7]) + Sizes2[num8]), 4), (byte) (iteratorVariable1 + 1), (byte) (index + 1), (byte) (num6 + 1), (byte) (num7 + 1), (byte) (num8 + 1)));
                            num8 = (byte) (num8 + 1);
                        }
                    }
                }
            }
            pBar.Value = iteratorVariable1 + 1;
            yield return iteratorVariable0;
            iteratorVariable0.Clear();
            GC.Collect();
            iteratorVariable1 = (byte) (iteratorVariable1 + 1);
        }
    }

    public static double DefineEquationCoefficients(int[] set)
    {
        double[] coefficients = new double[2];
        double num2 = 0.0;
        double num3 = set.Sum();
        double num4 = 0.0;
        double num5 = 0.0;
        double num6 = 0.0;
        for (int j = 0; j < set.Length; j++)
        {
            num4 += set[j] * (j + 1);
            num2 += j + 1;
            num5 += (j + 1) * (j + 1);
            num6 += set[j] * set[j];
        }
        coefficients[0] = ((set.Length * num4) - (num2 * num3)) / ((set.Length * num5) - (num2 * num2));
        coefficients[1] = (num3 - (coefficients[0] * num2)) / ((double) set.Length);
        return (1.0 - (set.Select<int, double>((t, i) => Math.Pow(t - ((coefficients[0] * (i + 1)) + coefficients[1]), 2.0)).Sum() / (num6 - ((num3 * num3) / ((double) set.Length)))));
    }

    public static void DefineParameters(ProgressBar pBar1, Label lblStatus1, Label lblOperation1, byte num, int count)
    {
        object[] objArray = new object[] { "Генерация составленных мер для набора ", num, " из ", count };
        Name = string.Concat(objArray);
        lblOperation = lblOperation1;
        lblStatus = lblStatus1;
        pBar = pBar1;
        ProgressIdentifier(1, N, Name);
        SequenceCount2 = 0;
        SequenceBegin2 = 0.0;
        SequenceEnd2 = 0.0;
        KE2 = 0.0;
        SummaryCount2 = 0.0;
        SummarySizes2 = Sizes2.Sum();
        CurrentStep = 0L;
        GenerateComposite();
        SummaryCount2 = CompositeBlocks.Count;
        SearchMaxSequence(CompositeBlocks);
        KE2 = (((double) SequenceCount2) / SummarySizes2) * (SequenceEnd2 - SequenceBegin2);
    }

    public static double[] DefineSizes(int[] set)
    {
        double num = A0;
        double[] numArray = new double[set.Sum()];
        int index = 0;
        int num3 = 0;
        while (num3 < 5)
        {
            int num4 = 0;
            while (true)
            {
                if (num4 >= set[num3])
                {
                    if (num3 != 4)
                    {
                        num = numArray[index - 1] + Delta[num3 + 1];
                    }
                    num3++;
                    break;
                }
                numArray[index] = num + (num4 * Delta[num3]);
                index++;
                num4++;
            }
        }
        return numArray;
    }

    public static void DefineVariables(int n, double a0, double amax, double[] d, double step)
    {
        if (n < 0x19)
        {
            throw new MyException("Число мер ниже предельного значения, измените значение N на большее!");
        }
        if (n > 0x7a)
        {
            throw new MyException("Число мер выше предельного значения, измените значение N на меньшее!");
        }
        if (a0 < 0.5)
        {
            throw new MyException("Размер наименьшей меры меньше предельного значения, измените значение меры на большее!");
        }
        if ((d[0] >= d[1]) || ((d[1] >= d[2]) || ((d[2] >= d[3]) || (d[3] >= d[4]))))
        {
            throw new MyException("Размер шагов в группах не удовлетворяют условию, измените их значения, чтобы сохранялся порядок возрастания!");
        }
        N = n;
        A0 = a0;
        Amax = amax;
        for (int i = 0; i < Delta.Length; i++)
        {
            Delta[i] = d[i];
        }
        CompositeStep = step;
    }

    public static void DefineVariables1(int n, double a0, double amax, double[] d, double step, bool complete)
    {
        N = n;
        A0 = a0;
        Amax = amax;
        for (int i = 0; i < Delta.Length; i++)
        {
            Delta[i] = d[i];
        }
        CompositeStep = step;
        if (complete)
        {
            F = F2(Sets.Last<int[]>());
        }
    }

    public static double F2(int[] gaugeSet) => 
        (Math.Pow(((((((((5.0 * A0) / Delta[0]) - 4.0) + (5 * gaugeSet[0])) + (((5 * gaugeSet[1]) * Delta[1]) / Delta[0])) + (((5 * gaugeSet[2]) * Delta[2]) / Delta[0])) + (((5 * gaugeSet[3]) * Delta[3]) / Delta[0])) + (((5 * gaugeSet[4]) * Delta[4]) / Delta[0])) - ((10.0 * Delta[4]) / Delta[0]), 2.0) * Delta[0]) / (((((((A0 * ((((gaugeSet[0] + gaugeSet[1]) + gaugeSet[2]) + gaugeSet[3]) + gaugeSet[4])) + (((gaugeSet[0] - 1) * (((((0.5 * gaugeSet[0]) + gaugeSet[1]) + gaugeSet[2]) + gaugeSet[3]) + gaugeSet[4])) * Delta[0])) + ((gaugeSet[1] * ((((0.5 * gaugeSet[1]) + gaugeSet[2]) + gaugeSet[3]) + gaugeSet[4])) * Delta[1])) + ((gaugeSet[2] * (((0.5 * gaugeSet[2]) + gaugeSet[3]) + gaugeSet[4])) * Delta[2])) + ((gaugeSet[3] * ((0.5 * gaugeSet[3]) + gaugeSet[4])) * Delta[3])) + (((0.5 * gaugeSet[4]) * gaugeSet[4]) * Delta[4])) + (0.5 * ((((Delta[1] * gaugeSet[1]) + (Delta[2] * gaugeSet[2])) + (Delta[3] * gaugeSet[3])) + (Delta[4] * gaugeSet[4]))));

    public static string FindCompositeBlock(double i)
    {
        if (CompositeBlocks.Count == 0)
        {
            return "Составные меры еще не сгенерированы, массив пуст.";
        }
        CompositeBlock block = CompositeBlocks.FirstOrDefault<CompositeBlock>(item => Math.Abs((double) (item.sum - i)) < 1E-06);
        if (block.Equals(CompositeBlocks.ElementAtOrDefault<CompositeBlock>(-1)))
        {
            return "Меры такой длины не существует!";
        }
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0}/{1}", CompositeBlocks.IndexOf(block), CompositeBlocks.Count);
        builder.AppendFormat("\r\n{0:0.####} (", block.sum);
        builder.AppendFormat((block.set1 == 0) ? "" : (" " + Sizes[block.set1 - 1]), new object[0]);
        builder.AppendFormat((block.set2 == 0) ? "" : (" " + Sizes[block.set2 - 1]), new object[0]);
        builder.AppendFormat((block.set3 == 0) ? "" : (" " + Sizes[block.set3 - 1]), new object[0]);
        builder.AppendFormat((block.set4 == 0) ? "" : (" " + Sizes[block.set4 - 1]), new object[0]);
        builder.AppendFormat((block.set5 == 0) ? "" : (" " + Sizes[block.set5 - 1]), new object[0]);
        builder.Append(" )\r\n");
        return builder.ToString();
    }

    private static void GenerateComposite()
    {
        CompositeBlocks.Clear();
        CompositeComparer cc = new CompositeComparer();
        List<CompositeBlock> first = new List<CompositeBlock>(((int) Math.Pow((double) N, 3.0)) + 1);
        List<CompositeBlock> second = new List<CompositeBlock>(((int) Math.Pow((double) N, 3.0)) + 1);
        for (byte i = 0; i < N; i = (byte) (i + 1))
        {
            first.Add(new CompositeBlock(Math.Round(Sizes2[i], 4), (byte) (i + 1), 0, 0, 0, 0));
        }
        byte index = 0;
        while (index < N)
        {
            byte num3 = (byte) (index + 1);
            while (true)
            {
                if (num3 >= N)
                {
                    index = (byte) (index + 1);
                    break;
                }
                second.Add(new CompositeBlock(Math.Round((double) (Sizes2[index] + Sizes2[num3]), 4), (byte) (index + 1), (byte) (num3 + 1), 0, 0, 0));
                num3 = (byte) (num3 + 1);
            }
        }
        first = first.Union<CompositeBlock>(second, cc).ToList<CompositeBlock>();
        second.Clear();
        GC.Collect();
        byte num4 = 0;
        while (num4 < N)
        {
            byte num5 = (byte) (num4 + 1);
            while (true)
            {
                if (num5 >= N)
                {
                    num4 = (byte) (num4 + 1);
                    break;
                }
                byte num6 = (byte) (num5 + 1);
                while (true)
                {
                    if (num6 >= N)
                    {
                        num5 = (byte) (num5 + 1);
                        break;
                    }
                    second.Add(new CompositeBlock(Math.Round((double) ((Sizes2[num4] + Sizes2[num5]) + Sizes2[num6]), 4), (byte) (num4 + 1), (byte) (num5 + 1), (byte) (num6 + 1), 0, 0));
                    num6 = (byte) (num6 + 1);
                }
            }
        }
        first = first.Union<CompositeBlock>(second, cc).ToList<CompositeBlock>();
        second.Clear();
        GC.Collect();
        CompositeBlocks = BigGenerateComposite().Aggregate<List<CompositeBlock>, List<CompositeBlock>>(first, (current, test) => current.Union<CompositeBlock>(test, cc).ToList<CompositeBlock>());
        CompositeBlocks.Sort(cc);
    }

    private static IEnumerable<int[]> GenerateSet(object param)
    {
        int iteratorVariable5;
        int iteratorVariable4;
        int iteratorVariable3;
        int[] iteratorVariable0 = (int[]) param;
        int[] source = new int[5];
        int iteratorVariable2 = iteratorVariable0[0];
        goto TR_0017;
    TR_0004:
        iteratorVariable5++;
    TR_000E:
        while (true)
        {
            if (iteratorVariable5 <= (N - 4))
            {
                int iteratorVariable6 = 1;
                while (true)
                {
                    while (true)
                    {
                        if (iteratorVariable6 <= (N - 4))
                        {
                            source = new int[] { iteratorVariable2, iteratorVariable3, iteratorVariable4, iteratorVariable5, iteratorVariable6 };
                            if (source.Sum() >= N)
                            {
                                if (source.Sum() <= N)
                                {
                                    yield return source;
                                    break;
                                }
                                goto TR_0004;
                            }
                        }
                        else
                        {
                            goto TR_0004;
                        }
                        break;
                    }
                    iteratorVariable6++;
                }
            }
            else
            {
                iteratorVariable4++;
            }
            break;
        }
    TR_0011:
        while (true)
        {
            if (iteratorVariable4 > (N - 4))
            {
                iteratorVariable3++;
                break;
            }
            iteratorVariable5 = 1;
            goto TR_000E;
        }
    TR_0014:
        while (true)
        {
            if (iteratorVariable3 > (N - 4))
            {
                pBar.Value = iteratorVariable2;
                iteratorVariable2++;
                break;
            }
            iteratorVariable4 = 1;
            goto TR_0011;
        }
    TR_0017:
        while (true)
        {
            if (iteratorVariable2 < iteratorVariable0[1])
            {
                iteratorVariable3 = 1;
            }
            else
            {
                goto TR_0004;
            }
            break;
        }
        goto TR_0014;
    }

    private static bool IsDivisible(double a0, double[] delta, double amax, int[] set) => 
        ((((Math.Abs((double) ((((a0 + ((set[0] - 1) * delta[0])) + delta[1]) / delta[1]) % 1.0)) < 1E-10) && (Math.Abs((double) ((((((a0 + ((set[0] - 1) * delta[0])) + ((set[1] - 1) * delta[1])) + delta[1]) + delta[2]) / delta[2]) % 1.0)) < 1E-10)) && (Math.Abs((double) ((((((((a0 + ((set[0] - 1) * delta[0])) + ((set[1] - 1) * delta[1])) + ((set[2] - 1) * delta[2])) + delta[1]) + delta[2]) + delta[3]) / delta[3]) % 1.0)) < 1E-10)) && (Math.Abs((double) ((((((((((a0 + ((set[0] - 1) * delta[0])) + ((set[1] - 1) * delta[1])) + ((set[2] - 1) * delta[2])) + ((set[3] - 1) * delta[3])) + delta[1]) + delta[2]) + delta[3]) + delta[4]) / delta[4]) % 1.0)) < 1E-10)) && ((((((((((a0 + ((set[0] - 1) * delta[0])) + ((set[1] - 1) * delta[1])) + ((set[2] - 1) * delta[2])) + ((set[3] - 1) * delta[3])) + ((set[4] - 1) * delta[4])) + delta[1]) + delta[2]) + delta[3]) + delta[4]) <= amax);

    public static string Print(int[] Ns, bool full)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("\nЗначение универсальной функции F = " + F + "\r\n");
        builder.Append("Количество мер в первой группе = " + Ns[0] + "\r\n");
        builder.Append("Количество мер во второй группе = " + Ns[1] + "\r\n");
        builder.Append("Количество мер в третьей группе = " + Ns[2] + "\r\n");
        builder.Append("Количество мер в четвертой группе = " + Ns[3] + "\r\n");
        builder.Append("Количество мер в пятой группе = " + Ns[4] + "\r\n\n");
        foreach (double num in Sizes)
        {
            builder.AppendFormat("{0:0.###}  ", num);
        }
        builder.AppendFormat("\r\n\nСуммарная длина мер набора = {0} мм\r\n", SummarySizes);
        if (full)
        {
            builder.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", SummaryCount);
            builder.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом (КСР) = {0}\r\n", SequenceCount);
            builder.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", SequenceBegin);
            builder.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", SequenceEnd);
            builder.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
            builder.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", KE);
        }
        builder.Append("\n\n");
        return builder.ToString();
    }

    public static string Print1(int[] Ns, bool full, bool complete)
    {
        StringBuilder builder = new StringBuilder();
        if (complete)
        {
            builder.Append("\nЗначение универсальной функции F = " + F + "\r\n");
            builder.Append("Количество мер в первой группе = " + Ns[0] + "\r\n");
            builder.Append("Количество мер во второй группе = " + Ns[1] + "\r\n");
            builder.Append("Количество мер в третьей группе = " + Ns[2] + "\r\n");
            builder.Append("Количество мер в четвертой группе = " + Ns[3] + "\r\n");
            builder.Append("Количество мер в пятой группе = " + Ns[4] + "\r\n\n");
        }
        foreach (double num in Sizes)
        {
            builder.AppendFormat("{0:0.###}  ", num);
        }
        builder.AppendFormat("\r\n\nСуммарная длина мер набора = {0} мм\r\n", SummarySizes);
        if (full)
        {
            builder.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", SummaryCount);
            builder.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом (КСР) = {0}\r\n", SequenceCount);
            builder.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", SequenceBegin);
            builder.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", SequenceEnd);
            builder.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
            builder.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", KE);
        }
        builder.Append("\n\n");
        return builder.ToString();
    }

    public static IEnumerable<string> PrintComposite(List<CompositeBlock> cbList)
    {
        StringBuilder iteratorVariable0 = new StringBuilder();
        iteratorVariable0.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", SummaryCount);
        iteratorVariable0.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом(КСР) = {0}\r\n", SequenceCount);
        iteratorVariable0.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", SequenceBegin);
        iteratorVariable0.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", SequenceEnd);
        iteratorVariable0.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
        iteratorVariable0.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", KE);
        yield return iteratorVariable0.ToString();
        iteratorVariable0.Clear();
        List<CompositeBlock>.Enumerator enumerator = cbList.GetEnumerator();
        while (true)
        {
            if (!enumerator.MoveNext())
            {
                yield break;
            }
            CompositeBlock current = enumerator.Current;
            iteratorVariable0.AppendFormat("\r\n{0:0.####} (", current.sum);
            iteratorVariable0.AppendFormat((current.set1 == 0) ? "" : (" " + Sizes[current.set1 - 1]), new object[0]);
            iteratorVariable0.AppendFormat((current.set2 == 0) ? "" : (" " + Sizes[current.set2 - 1]), new object[0]);
            iteratorVariable0.AppendFormat((current.set3 == 0) ? "" : (" " + Sizes[current.set3 - 1]), new object[0]);
            iteratorVariable0.AppendFormat((current.set4 == 0) ? "" : (" " + Sizes[current.set4 - 1]), new object[0]);
            iteratorVariable0.AppendFormat((current.set5 == 0) ? "" : (" " + Sizes[current.set5 - 1]), new object[0]);
            iteratorVariable0.Append(" )\r\n");
            yield return iteratorVariable0.ToString();
            iteratorVariable0.Clear();
        }
    }

    public static string PrintSet(int[] Ns)
    {
        Sizes = DefineSizes(Ns);
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0}\r\n", N);
        foreach (double num in Sizes)
        {
            builder.AppendFormat("{0:0.####} ", num);
        }
        return builder.ToString();
    }

    private static void ProgressIdentifier(int beg, int end, string name)
    {
        lblStatus.Text = name;
        lblStatus.Update();
        pBar.Minimum = beg;
        pBar.Maximum = end;
        pBar.Value = beg;
    }

    public static void SearchBestSet(bool auto, ProgressBar pBar1, Label lblStatus1, Label lblOperation1, RichTextBox rtbMain)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int[] param = new int[] { 1, N + 1 };
        Name = "Поиск набора с лучшими значениями универсальной функции F и коэффициента эффективности (КЕ)";
        lblOperation = lblOperation1;
        lblStatus = lblStatus1;
        pBar = pBar1;
        ProgressIdentifier(1, N, Name);
        R2 = 0.0;
        F = 0.0;
        CurrentStep = 0L;
        MaximalStep = (long) Math.Pow((double) N, 5.0);
        FSets.Clear();
        RSets.Clear();
        Sets.Clear();
        foreach (string str in TempFiles)
        {
            File.Delete(str);
        }
        TempFiles.Clear();
        foreach (int[] numArray2 in GenerateSet(param))
        {
            if (TestConditions(numArray2))
            {
                if (((int) (F2(numArray2) / 0x3e8)) > ((int) (F / 0x3e8)))
                {
                    FSets.Clear();
                    F = F2(numArray2);
                }
                FSets.Add(numArray2);
            }
            CurrentStep += 1L;
        }
        if (FSets.Count == 0)
        {
            MessageBox.Show("Наборов, удовлетворяющих условию целочисленности (любой член группы делится нацело на значение разницы ? размеров в группе) не найдено.");
        }
        else
        {
            foreach (int[] numArray3 in FSets)
            {
                RSets.Add(numArray3);
            }
            StringBuilder builder = new StringBuilder();
            KE = 0.0;
            byte num = 0;
            foreach (int[] numArray4 in RSets)
            {
                num = (byte) (num + 1);
                Sizes2 = DefineSizes(numArray4);
                SummarySizes2 = Sizes2.Sum();
                if (!auto)
                {
                    Sets.Add(numArray4);
                    Sizes = Sizes2;
                    SummarySizes = SummarySizes2;
                }
                else
                {
                    Zeroable();
                    if (CompositeBlocks != null)
                    {
                        CompositeBlocks.Clear();
                    }
                    DefineParameters(pBar1, lblStatus1, lblOperation1, num, RSets.Count);
                    if (KE2 >= KE)
                    {
                        if (Math.Abs((double) (KE2 - KE)) > 0.0001)
                        {
                            Sets.Clear();
                            builder.Clear();
                            foreach (string str2 in TempFiles)
                            {
                                File.Delete(str2);
                            }
                            TempFiles.Clear();
                        }
                        Sets.Add(numArray4);
                        Sizes = (double[]) Sizes2.Clone();
                        SummarySizes = SummarySizes2;
                        KE = KE2;
                        SequenceCount = SequenceCount2;
                        SequenceBegin = SequenceBegin2;
                        SequenceEnd = SequenceEnd2;
                        SummaryCount = SummaryCount2;
                    }
                }
                if (Sets.Count > 0)
                {
                    F = F2(Sets.Last<int[]>());
                    builder.Append(Print(Sets.Last<int[]>(), auto));
                }
                if (auto)
                {
                    string tempFileName = Path.GetTempFileName();
                    TempFiles.Add(tempFileName);
                    StreamWriter writer = new StreamWriter(tempFileName, false);
                    foreach (string str4 in PrintComposite(CompositeBlocks))
                    {
                        writer.Write(str4);
                    }
                    writer.Close();
                }
            }
            rtbMain.Text = rtbMain.Text + builder.ToString();
            stopwatch.Stop();
            SummaryTime = stopwatch.Elapsed;
        }
    }

    private static void SearchMaxSequence(List<CompositeBlock> compositeBlocks)
    {
        double sum = compositeBlocks[0].sum;
        int num3 = 1;
        SequenceCount2 = 0;
        BitArray array = new BitArray(compositeBlocks.Count, true);
        for (int i = 0; i < compositeBlocks.Count; i++)
        {
            if (array[i])
            {
                double num;
                int num4 = i;
                array[num4] = false;
                if (num3 == 1)
                {
                    num = compositeBlocks[num4].sum;
                }
                bool flag = true;
                int num6 = num4;
                while (flag && (num6 < (compositeBlocks.Count - 1)))
                {
                    num6++;
                    if (Math.Abs((double) (compositeBlocks[num6].sum - compositeBlocks[num4].sum)) <= (CompositeStep + 1E-06))
                    {
                        if (Math.Abs((double) ((compositeBlocks[num6].sum - compositeBlocks[num4].sum) - CompositeStep)) >= 1E-06)
                        {
                            continue;
                        }
                        array[num6] = false;
                        num3++;
                        continue;
                    }
                    flag = false;
                    sum = compositeBlocks[num4].sum;
                    if (num3 > SequenceCount2)
                    {
                        SequenceCount2 = num3;
                        SequenceBegin2 = num;
                        SequenceEnd2 = sum;
                    }
                    num3 = 1;
                }
            }
        }
    }

    public static string ShowCompositeBlock(int i)
    {
        if (CompositeBlocks.Count == 0)
        {
            return "Составные меры еще не сгенерированы, массив пуст.";
        }
        if (((i - 1) >= CompositeBlocks.Count) || ((i - 1) < 0))
        {
            return "Такой меры в массиве нет!";
        }
        CompositeBlock block = CompositeBlocks[i - 1];
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0}/{1}", i, CompositeBlocks.Count);
        builder.AppendFormat("\r\n{0:0.####} (", block.sum);
        builder.AppendFormat((block.set1 == 0) ? "" : (" " + Sizes[block.set1 - 1]), new object[0]);
        builder.AppendFormat((block.set2 == 0) ? "" : (" " + Sizes[block.set2 - 1]), new object[0]);
        builder.AppendFormat((block.set3 == 0) ? "" : (" " + Sizes[block.set3 - 1]), new object[0]);
        builder.AppendFormat((block.set4 == 0) ? "" : (" " + Sizes[block.set4 - 1]), new object[0]);
        builder.AppendFormat((block.set5 == 0) ? "" : (" " + Sizes[block.set5 - 1]), new object[0]);
        builder.Append(" )\r\n");
        return builder.ToString();
    }

    private static bool TestConditions(int[] s)
    {
        if (!IsDivisible(A0, Delta, Amax, s))
        {
            return false;
        }
        if (((int) (F2(s) / 0x3e8)) < ((int) (F / 0x3e8)))
        {
            return false;
        }
        Fprev = F;
        return true;
    }

    public static int[] TranslationFrom10ToN(long n)
    {
        long num2 = n;
        List<int> list = new List<int>(5);
        while (true)
        {
            list.Add((int) (n % ((long) N)));
            n /= (long) N;
            if (n == 0L)
            {
                return list.ToArray();
            }
        }
    }

    private static void Zeroable()
    {
        SequenceCount = 0;
        SequenceCount2 = 0;
        SequenceBegin = 0.0;
        SequenceBegin2 = 0.0;
        SequenceEnd = 0.0;
        SequenceEnd2 = 0.0;
        KE = 0.0;
        KE2 = 0.0;
        SummarySizes = 0.0;
        SummarySizes2 = 0.0;
        SummaryCount = 0.0;
        SummaryCount2 = 0.0;
    }

    // Nested Types
    [CompilerGenerated]
    private sealed class <AdditionalGenerateComposite>d__1c : IEnumerable<List<CompositeBlock>>, IEnumerable, IEnumerator<List<CompositeBlock>>, IEnumerator, IDisposable
    {
        // Fields
        private List<CompositeBlock> <>2__current;
        private int <>1__state;
        private int <>l__initialThreadId;
        public List<CompositeBlock> <test2>5__1d;
        public byte <i>5__1e;
        public byte <i>5__1f;
        public byte <j>5__20;

        // Methods
        [DebuggerHidden]
        public <AdditionalGenerateComposite>d__1c(int <>1__state)
        {
            this.<>1__state = <>1__state;
            this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        private bool MoveNext()
        {
            switch (this.<>1__state)
            {
                case 0:
                    this.<>1__state = -1;
                    this.<test2>5__1d = new List<CompositeBlock>(((int) Math.Pow((double) GaugeCalculations.N, 3.0)) + 1);
                    this.<i>5__1e = 0;
                    break;

                case 1:
                    this.<>1__state = -1;
                    this.<test2>5__1d.Clear();
                    GC.Collect();
                    this.<i>5__1e = (byte) (this.<i>5__1e + 1);
                    break;

                case 2:
                    this.<>1__state = -1;
                    this.<test2>5__1d.Clear();
                    GC.Collect();
                    this.<j>5__20 = (byte) (this.<j>5__20 + 1);
                    goto TR_000F;

                default:
                    goto TR_0000;
            }
            if (this.<i>5__1e < GaugeCalculations.N)
            {
                byte index = (byte) (this.<i>5__1e + 1);
                while (index < GaugeCalculations.N)
                {
                    byte num2 = (byte) (index + 1);
                    while (true)
                    {
                        if (num2 >= GaugeCalculations.N)
                        {
                            index = (byte) (index + 1);
                            break;
                        }
                        byte num3 = (byte) (num2 + 1);
                        while (true)
                        {
                            if (num3 >= GaugeCalculations.N)
                            {
                                num2 = (byte) (num2 + 1);
                                break;
                            }
                            this.<test2>5__1d.Add(new CompositeBlock(Math.Round((double) (((GaugeCalculations.Sizes2[this.<i>5__1e] + GaugeCalculations.Sizes2[index]) + GaugeCalculations.Sizes2[num2]) + GaugeCalculations.Sizes2[num3]), 4), (byte) (this.<i>5__1e + 1), (byte) (index + 1), (byte) (num2 + 1), (byte) (num3 + 1), 0));
                            num3 = (byte) (num3 + 1);
                        }
                    }
                }
                GaugeCalculations.pBar.Value = this.<i>5__1e + 1;
                this.<>2__current = this.<test2>5__1d;
                this.<>1__state = 1;
                return true;
            }
            this.<i>5__1f = 0;
            goto TR_0012;
        TR_0000:
            return false;
        TR_000F:
            if (this.<j>5__20 < GaugeCalculations.N)
            {
                byte index = (byte) (this.<j>5__20 + 1);
                while (index < GaugeCalculations.N)
                {
                    byte num5 = (byte) (index + 1);
                    while (true)
                    {
                        if (num5 >= GaugeCalculations.N)
                        {
                            index = (byte) (index + 1);
                            break;
                        }
                        byte num6 = (byte) (num5 + 1);
                        while (true)
                        {
                            if (num6 >= GaugeCalculations.N)
                            {
                                num5 = (byte) (num5 + 1);
                                break;
                            }
                            this.<test2>5__1d.Add(new CompositeBlock(Math.Round((double) ((((GaugeCalculations.Sizes2[this.<i>5__1f] + GaugeCalculations.Sizes2[this.<j>5__20]) + GaugeCalculations.Sizes2[index]) + GaugeCalculations.Sizes2[num5]) + GaugeCalculations.Sizes2[num6]), 4), (byte) (this.<i>5__1f + 1), (byte) (this.<j>5__20 + 1), (byte) (index + 1), (byte) (num5 + 1), (byte) (num6 + 1)));
                            num6 = (byte) (num6 + 1);
                        }
                    }
                }
                GaugeCalculations.pBar.Value = this.<i>5__1f + 1;
                this.<>2__current = this.<test2>5__1d;
                this.<>1__state = 2;
                return true;
            }
            this.<i>5__1f = (byte) (this.<i>5__1f + 1);
        TR_0012:
            while (true)
            {
                if (this.<i>5__1f < GaugeCalculations.N)
                {
                    this.<j>5__20 = (byte) (this.<i>5__1f + 1);
                }
                else
                {
                    goto TR_0000;
                }
                break;
            }
            goto TR_000F;
        }

        [DebuggerHidden]
        IEnumerator<List<CompositeBlock>> IEnumerable<List<CompositeBlock>>.GetEnumerator()
        {
            GaugeCalculations.<AdditionalGenerateComposite>d__1c d__c;
            if ((Environment.CurrentManagedThreadId != this.<>l__initialThreadId) || (this.<>1__state != -2))
            {
                d__c = new GaugeCalculations.<AdditionalGenerateComposite>d__1c(0);
            }
            else
            {
                this.<>1__state = 0;
                d__c = this;
            }
            return d__c;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<System.Collections.Generic.List<GaugeBlockv3.CompositeBlock>>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        // Properties
        List<CompositeBlock> IEnumerator<List<CompositeBlock>>.Current =>
            this.<>2__current;

        object IEnumerator.Current =>
            this.<>2__current;
    }

    [CompilerGenerated]
    private sealed class <AverageGenerateComposite>d__15 : IEnumerable<List<CompositeBlock>>, IEnumerable, IEnumerator<List<CompositeBlock>>, IEnumerator, IDisposable
    {
        // Fields
        private List<CompositeBlock> <>2__current;
        private int <>1__state;
        private int <>l__initialThreadId;
        public int p;
        public int <>3__p;
        public List<CompositeBlock> <test2>5__16;
        public byte <i>5__17;
        public byte <i>5__18;
        public byte <j>5__19;

        // Methods
        [DebuggerHidden]
        public <AverageGenerateComposite>d__15(int <>1__state)
        {
            this.<>1__state = <>1__state;
            this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        private bool MoveNext()
        {
            switch (this.<>1__state)
            {
                case 0:
                    this.<>1__state = -1;
                    this.<test2>5__16 = new List<CompositeBlock>(((int) (Math.Pow((double) GaugeCalculations.N, 4.0) / ((double) this.p))) + 1);
                    this.<i>5__17 = 0;
                    break;

                case 1:
                    this.<>1__state = -1;
                    this.<test2>5__16.Clear();
                    GC.Collect();
                    goto TR_0018;

                case 2:
                    this.<>1__state = -1;
                    this.<test2>5__16.Clear();
                    GC.Collect();
                    goto TR_0003;

                default:
                    goto TR_0000;
            }
            goto TR_0028;
        TR_0000:
            return false;
        TR_0003:
            this.<j>5__19 = (byte) (this.<j>5__19 + 1);
        TR_0012:
            while (true)
            {
                if (this.<j>5__19 >= GaugeCalculations.N)
                {
                    GaugeCalculations.pBar.Value = this.<i>5__18 + 1;
                    this.<i>5__18 = (byte) (this.<i>5__18 + 1);
                    break;
                }
                byte index = (byte) (this.<j>5__19 + 1);
                while (true)
                {
                    if (index >= GaugeCalculations.N)
                    {
                        if (this.<test2>5__16.Count <= (this.<test2>5__16.Capacity / 2))
                        {
                            break;
                        }
                        this.<>2__current = this.<test2>5__16;
                        this.<>1__state = 2;
                        return true;
                    }
                    byte num5 = (byte) (index + 1);
                    while (true)
                    {
                        if (num5 >= GaugeCalculations.N)
                        {
                            index = (byte) (index + 1);
                            break;
                        }
                        byte num6 = (byte) (num5 + 1);
                        while (true)
                        {
                            if (num6 >= GaugeCalculations.N)
                            {
                                num5 = (byte) (num5 + 1);
                                break;
                            }
                            this.<test2>5__16.Add(new CompositeBlock(Math.Round((double) ((((GaugeCalculations.Sizes2[this.<i>5__18] + GaugeCalculations.Sizes2[this.<j>5__19]) + GaugeCalculations.Sizes2[index]) + GaugeCalculations.Sizes2[num5]) + GaugeCalculations.Sizes2[num6]), 4), (byte) (this.<i>5__18 + 1), (byte) (this.<j>5__19 + 1), (byte) (index + 1), (byte) (num5 + 1), (byte) (num6 + 1)));
                            num6 = (byte) (num6 + 1);
                        }
                    }
                }
                goto TR_0003;
            }
        TR_0015:
            while (true)
            {
                if (this.<i>5__18 < GaugeCalculations.N)
                {
                    this.<j>5__19 = (byte) (this.<i>5__18 + 1);
                }
                else
                {
                    goto TR_0000;
                }
                break;
            }
            goto TR_0012;
        TR_0018:
            this.<i>5__17 = (byte) (this.<i>5__17 + 1);
        TR_0028:
            while (true)
            {
                if (this.<i>5__17 < GaugeCalculations.N)
                {
                    byte index = (byte) (this.<i>5__17 + 1);
                    while (true)
                    {
                        if (index >= GaugeCalculations.N)
                        {
                            GaugeCalculations.pBar.Value = this.<i>5__17 + 1;
                            if (this.<test2>5__16.Count <= (this.<test2>5__16.Capacity / 2))
                            {
                                break;
                            }
                            this.<>2__current = this.<test2>5__16;
                            this.<>1__state = 1;
                            return true;
                        }
                        byte num2 = (byte) (index + 1);
                        while (true)
                        {
                            if (num2 >= GaugeCalculations.N)
                            {
                                index = (byte) (index + 1);
                                break;
                            }
                            byte num3 = (byte) (num2 + 1);
                            while (true)
                            {
                                if (num3 >= GaugeCalculations.N)
                                {
                                    num2 = (byte) (num2 + 1);
                                    break;
                                }
                                this.<test2>5__16.Add(new CompositeBlock(Math.Round((double) (((GaugeCalculations.Sizes2[this.<i>5__17] + GaugeCalculations.Sizes2[index]) + GaugeCalculations.Sizes2[num2]) + GaugeCalculations.Sizes2[num3]), 4), (byte) (this.<i>5__17 + 1), (byte) (index + 1), (byte) (num2 + 1), (byte) (num3 + 1), 0));
                                num3 = (byte) (num3 + 1);
                            }
                        }
                    }
                }
                else
                {
                    this.<i>5__18 = 0;
                    goto TR_0015;
                }
                break;
            }
            goto TR_0018;
        }

        [DebuggerHidden]
        IEnumerator<List<CompositeBlock>> IEnumerable<List<CompositeBlock>>.GetEnumerator()
        {
            GaugeCalculations.<AverageGenerateComposite>d__15 d__;
            if ((Environment.CurrentManagedThreadId != this.<>l__initialThreadId) || (this.<>1__state != -2))
            {
                d__ = new GaugeCalculations.<AverageGenerateComposite>d__15(0);
            }
            else
            {
                this.<>1__state = 0;
                d__ = this;
            }
            d__.p = this.<>3__p;
            return d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<System.Collections.Generic.List<GaugeBlockv3.CompositeBlock>>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        // Properties
        List<CompositeBlock> IEnumerator<List<CompositeBlock>>.Current =>
            this.<>2__current;

        object IEnumerator.Current =>
            this.<>2__current;
    }

    [CompilerGenerated]
    private sealed class <BigGenerateComposite>d__10 : IEnumerable<List<CompositeBlock>>, IEnumerable, IEnumerator<List<CompositeBlock>>, IEnumerator, IDisposable
    {
        // Fields
        private List<CompositeBlock> <>2__current;
        private int <>1__state;
        private int <>l__initialThreadId;
        public List<CompositeBlock> <test2>5__11;
        public byte <i>5__12;

        // Methods
        [DebuggerHidden]
        public <BigGenerateComposite>d__10(int <>1__state)
        {
            this.<>1__state = <>1__state;
            this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        private bool MoveNext()
        {
            switch (this.<>1__state)
            {
                case 0:
                {
                    this.<>1__state = -1;
                    this.<test2>5__11 = new List<CompositeBlock>();
                    byte index = 0;
                    while (index < GaugeCalculations.N)
                    {
                        byte num2 = (byte) (index + 1);
                        while (true)
                        {
                            if (num2 >= GaugeCalculations.N)
                            {
                                GaugeCalculations.pBar.Value = index + 1;
                                index = (byte) (index + 1);
                                break;
                            }
                            byte num3 = (byte) (num2 + 1);
                            while (true)
                            {
                                if (num3 >= GaugeCalculations.N)
                                {
                                    num2 = (byte) (num2 + 1);
                                    break;
                                }
                                byte num4 = (byte) (num3 + 1);
                                while (true)
                                {
                                    if (num4 >= GaugeCalculations.N)
                                    {
                                        num3 = (byte) (num3 + 1);
                                        break;
                                    }
                                    this.<test2>5__11.Add(new CompositeBlock(Math.Round((double) (((GaugeCalculations.Sizes2[index] + GaugeCalculations.Sizes2[num2]) + GaugeCalculations.Sizes2[num3]) + GaugeCalculations.Sizes2[num4]), 4), (byte) (index + 1), (byte) (num2 + 1), (byte) (num3 + 1), (byte) (num4 + 1), 0));
                                    num4 = (byte) (num4 + 1);
                                }
                            }
                        }
                    }
                    this.<>2__current = this.<test2>5__11;
                    this.<>1__state = 1;
                    return true;
                }
                case 1:
                    this.<>1__state = -1;
                    this.<test2>5__11.Clear();
                    GC.Collect();
                    this.<i>5__12 = 0;
                    break;

                case 2:
                    this.<>1__state = -1;
                    this.<test2>5__11.Clear();
                    GC.Collect();
                    this.<i>5__12 = (byte) (this.<i>5__12 + 1);
                    break;

                default:
                    goto TR_0000;
            }
            if (this.<i>5__12 < GaugeCalculations.N)
            {
                byte index = (byte) (this.<i>5__12 + 1);
                while (index < GaugeCalculations.N)
                {
                    byte num6 = (byte) (index + 1);
                    while (true)
                    {
                        if (num6 >= GaugeCalculations.N)
                        {
                            index = (byte) (index + 1);
                            break;
                        }
                        byte num7 = (byte) (num6 + 1);
                        while (true)
                        {
                            if (num7 >= GaugeCalculations.N)
                            {
                                num6 = (byte) (num6 + 1);
                                break;
                            }
                            byte num8 = (byte) (num7 + 1);
                            while (true)
                            {
                                if (num8 >= GaugeCalculations.N)
                                {
                                    num7 = (byte) (num7 + 1);
                                    break;
                                }
                                this.<test2>5__11.Add(new CompositeBlock(Math.Round((double) ((((GaugeCalculations.Sizes2[this.<i>5__12] + GaugeCalculations.Sizes2[index]) + GaugeCalculations.Sizes2[num6]) + GaugeCalculations.Sizes2[num7]) + GaugeCalculations.Sizes2[num8]), 4), (byte) (this.<i>5__12 + 1), (byte) (index + 1), (byte) (num6 + 1), (byte) (num7 + 1), (byte) (num8 + 1)));
                                num8 = (byte) (num8 + 1);
                            }
                        }
                    }
                }
                GaugeCalculations.pBar.Value = this.<i>5__12 + 1;
                this.<>2__current = this.<test2>5__11;
                this.<>1__state = 2;
                return true;
            }
        TR_0000:
            return false;
        }

        [DebuggerHidden]
        IEnumerator<List<CompositeBlock>> IEnumerable<List<CompositeBlock>>.GetEnumerator()
        {
            GaugeCalculations.<BigGenerateComposite>d__10 d__;
            if ((Environment.CurrentManagedThreadId != this.<>l__initialThreadId) || (this.<>1__state != -2))
            {
                d__ = new GaugeCalculations.<BigGenerateComposite>d__10(0);
            }
            else
            {
                this.<>1__state = 0;
                d__ = this;
            }
            return d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<System.Collections.Generic.List<GaugeBlockv3.CompositeBlock>>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        // Properties
        List<CompositeBlock> IEnumerator<List<CompositeBlock>>.Current =>
            this.<>2__current;

        object IEnumerator.Current =>
            this.<>2__current;
    }

    [CompilerGenerated]
    private sealed class <GenerateSet>d__0 : IEnumerable<int[]>, IEnumerable, IEnumerator<int[]>, IEnumerator, IDisposable
    {
        // Fields
        private int[] <>2__current;
        private int <>1__state;
        private int <>l__initialThreadId;
        public object param;
        public object <>3__param;
        public int[] <ranges>5__1;
        public int[] <result>5__2;
        public int <i>5__3;
        public int <j>5__4;
        public int <k>5__5;
        public int <l>5__6;
        public int <m>5__7;

        // Methods
        [DebuggerHidden]
        public <GenerateSet>d__0(int <>1__state)
        {
            this.<>1__state = <>1__state;
            this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        private bool MoveNext()
        {
            switch (this.<>1__state)
            {
                case 0:
                    this.<>1__state = -1;
                    this.<ranges>5__1 = (int[]) this.param;
                    this.<result>5__2 = new int[5];
                    this.<i>5__3 = this.<ranges>5__1[0];
                    break;

                case 1:
                    this.<>1__state = -1;
                    goto TR_0007;

                default:
                    goto TR_0000;
            }
            goto TR_0017;
        TR_0000:
            return false;
        TR_0004:
            this.<l>5__6++;
            goto TR_000E;
        TR_0007:
            this.<m>5__7++;
        TR_000B:
            while (true)
            {
                if (this.<m>5__7 <= (GaugeCalculations.N - 4))
                {
                    this.<result>5__2 = new int[] { this.<i>5__3, this.<j>5__4, this.<k>5__5, this.<l>5__6, this.<m>5__7 };
                    if (this.<result>5__2.Sum() >= GaugeCalculations.N)
                    {
                        if (this.<result>5__2.Sum() <= GaugeCalculations.N)
                        {
                            this.<>2__current = this.<result>5__2;
                            this.<>1__state = 1;
                            return true;
                        }
                        goto TR_0004;
                    }
                }
                else
                {
                    goto TR_0004;
                }
                break;
            }
            goto TR_0007;
        TR_000E:
            while (true)
            {
                if (this.<l>5__6 <= (GaugeCalculations.N - 4))
                {
                    this.<m>5__7 = 1;
                    goto TR_000B;
                }
                else
                {
                    this.<k>5__5++;
                }
                break;
            }
        TR_0011:
            while (true)
            {
                if (this.<k>5__5 > (GaugeCalculations.N - 4))
                {
                    this.<j>5__4++;
                    break;
                }
                this.<l>5__6 = 1;
                goto TR_000E;
            }
        TR_0014:
            while (true)
            {
                if (this.<j>5__4 > (GaugeCalculations.N - 4))
                {
                    GaugeCalculations.pBar.Value = this.<i>5__3;
                    this.<i>5__3++;
                    break;
                }
                this.<k>5__5 = 1;
                goto TR_0011;
            }
        TR_0017:
            while (true)
            {
                if (this.<i>5__3 < this.<ranges>5__1[1])
                {
                    this.<j>5__4 = 1;
                }
                else
                {
                    goto TR_0000;
                }
                break;
            }
            goto TR_0014;
        }

        [DebuggerHidden]
        IEnumerator<int[]> IEnumerable<int[]>.GetEnumerator()
        {
            GaugeCalculations.<GenerateSet>d__0 d__;
            if ((Environment.CurrentManagedThreadId != this.<>l__initialThreadId) || (this.<>1__state != -2))
            {
                d__ = new GaugeCalculations.<GenerateSet>d__0(0);
            }
            else
            {
                this.<>1__state = 0;
                d__ = this;
            }
            d__.param = this.<>3__param;
            return d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<System.Int32[]>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        // Properties
        int[] IEnumerator<int[]>.Current =>
            this.<>2__current;

        object IEnumerator.Current =>
            this.<>2__current;
    }

    [CompilerGenerated]
    private sealed class <PrintComposite>d__23 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
    {
        // Fields
        private string <>2__current;
        private int <>1__state;
        private int <>l__initialThreadId;
        public List<CompositeBlock> cbList;
        public List<CompositeBlock> <>3__cbList;
        public StringBuilder <sb>5__24;
        public CompositeBlock <compositeBlock>5__25;
        public List<CompositeBlock>.Enumerator <>7__wrap26;

        // Methods
        [DebuggerHidden]
        public <PrintComposite>d__23(int <>1__state)
        {
            this.<>1__state = <>1__state;
            this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
        }

        private void <>m__Finally27()
        {
            this.<>1__state = -1;
            this.<>7__wrap26.Dispose();
        }

        private bool MoveNext()
        {
            bool flag;
            try
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        this.<sb>5__24 = new StringBuilder();
                        this.<sb>5__24.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", GaugeCalculations.SummaryCount);
                        this.<sb>5__24.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом(КСР) = {0}\r\n", GaugeCalculations.SequenceCount);
                        this.<sb>5__24.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", GaugeCalculations.SequenceBegin);
                        this.<sb>5__24.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", GaugeCalculations.SequenceEnd);
                        this.<sb>5__24.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
                        this.<sb>5__24.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", GaugeCalculations.KE);
                        this.<>2__current = this.<sb>5__24.ToString();
                        this.<>1__state = 1;
                        return true;

                    case 1:
                        this.<>1__state = -1;
                        this.<sb>5__24.Clear();
                        this.<>7__wrap26 = this.cbList.GetEnumerator();
                        this.<>1__state = 2;
                        break;

                    case 3:
                        this.<>1__state = 2;
                        this.<sb>5__24.Clear();
                        break;

                    default:
                        goto TR_0002;
                }
                if (this.<>7__wrap26.MoveNext())
                {
                    this.<compositeBlock>5__25 = this.<>7__wrap26.Current;
                    this.<sb>5__24.AppendFormat("\r\n{0:0.####} (", this.<compositeBlock>5__25.sum);
                    this.<sb>5__24.AppendFormat((this.<compositeBlock>5__25.set1 == 0) ? "" : (" " + GaugeCalculations.Sizes[this.<compositeBlock>5__25.set1 - 1]), new object[0]);
                    this.<sb>5__24.AppendFormat((this.<compositeBlock>5__25.set2 == 0) ? "" : (" " + GaugeCalculations.Sizes[this.<compositeBlock>5__25.set2 - 1]), new object[0]);
                    this.<sb>5__24.AppendFormat((this.<compositeBlock>5__25.set3 == 0) ? "" : (" " + GaugeCalculations.Sizes[this.<compositeBlock>5__25.set3 - 1]), new object[0]);
                    this.<sb>5__24.AppendFormat((this.<compositeBlock>5__25.set4 == 0) ? "" : (" " + GaugeCalculations.Sizes[this.<compositeBlock>5__25.set4 - 1]), new object[0]);
                    this.<sb>5__24.AppendFormat((this.<compositeBlock>5__25.set5 == 0) ? "" : (" " + GaugeCalculations.Sizes[this.<compositeBlock>5__25.set5 - 1]), new object[0]);
                    this.<sb>5__24.Append(" )\r\n");
                    this.<>2__current = this.<sb>5__24.ToString();
                    this.<>1__state = 3;
                    return true;
                }
                else
                {
                    this.<>m__Finally27();
                }
            TR_0002:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

        [DebuggerHidden]
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            GaugeCalculations.<PrintComposite>d__23 d__;
            if ((Environment.CurrentManagedThreadId != this.<>l__initialThreadId) || (this.<>1__state != -2))
            {
                d__ = new GaugeCalculations.<PrintComposite>d__23(0);
            }
            else
            {
                this.<>1__state = 0;
                d__ = this;
            }
            d__.cbList = this.<>3__cbList;
            return d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator() => 
            this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
            switch (this.<>1__state)
            {
                case 2:
                case 3:
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally27();
                    }
                    return;
            }
        }

        // Properties
        string IEnumerator<string>.Current =>
            this.<>2__current;

        object IEnumerator.Current =>
            this.<>2__current;
    }

    private class CompositeComparer : IEqualityComparer<CompositeBlock>, IComparer<CompositeBlock>
    {
        // Methods
        public int Compare(CompositeBlock x, CompositeBlock y) => 
            !x.Equals(y) ? ((x.sum <= y.sum) ? -1 : 1) : 0;

        public bool Equals(CompositeBlock x, CompositeBlock y) => 
            Math.Abs((double) (x.sum - y.sum)) < 1E-06;

        public int GetHashCode(CompositeBlock block) => 
            block.sum.GetHashCode();
    }
}
*/