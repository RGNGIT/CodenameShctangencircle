using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodenameShctangencircle //полностью скопированный класс из прошлой программы подсчета
{
	// Token: 0x02000005 RID: 5
	static class GaugeCalculations
	{

		internal struct CompositeBlock
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000013 RID: 19 RVA: 0x000020E0 File Offset: 0x000002E0
			// (set) Token: 0x06000014 RID: 20 RVA: 0x000020E8 File Offset: 0x000002E8
			public double sum { get; set; }

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000015 RID: 21 RVA: 0x000020F1 File Offset: 0x000002F1
			// (set) Token: 0x06000016 RID: 22 RVA: 0x000020F9 File Offset: 0x000002F9
			public byte set1 { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000017 RID: 23 RVA: 0x00002102 File Offset: 0x00000302
			// (set) Token: 0x06000018 RID: 24 RVA: 0x0000210A File Offset: 0x0000030A
			public byte set2 { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000019 RID: 25 RVA: 0x00002113 File Offset: 0x00000313
			// (set) Token: 0x0600001A RID: 26 RVA: 0x0000211B File Offset: 0x0000031B
			public byte set3 { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001B RID: 27 RVA: 0x00002124 File Offset: 0x00000324
			// (set) Token: 0x0600001C RID: 28 RVA: 0x0000212C File Offset: 0x0000032C
			public byte set4 { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00002135 File Offset: 0x00000335
			// (set) Token: 0x0600001E RID: 30 RVA: 0x0000213D File Offset: 0x0000033D
			public byte set5 { get; set; }

			// Token: 0x0600001F RID: 31 RVA: 0x00002146 File Offset: 0x00000346
			public CompositeBlock(double s, byte i1, byte i2, byte i3, byte i4, byte i5)
			{
				this = default(CompositeBlock);
				this.sum = s;
				this.set1 = i1;
				this.set2 = i2;
				this.set3 = i3;
				this.set4 = i4;
				this.set5 = i5;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00004974 File Offset: 0x00002B74
		public static void DefineVariables(int n, double a0, double amax, double[] d, double step)
		{
			if (n < 25)
			{
				//throw new MyException("Число мер ниже предельного значения, измените значение N на большее!");
			}
			if (n > 122)
			{
				//throw new MyException("Число мер выше предельного значения, измените значение N на меньшее!");
			}
			if (a0 < 0.5)
			{
				//throw new MyException("Размер наименьшей меры меньше предельного значения, измените значение меры на большее!");
			}
			if (d[0] >= d[1] || d[1] >= d[2] || d[2] >= d[3] || d[3] >= d[4])
			{
				//throw new MyException("Размер шагов в группах не удовлетворяют условию, измените их значения, чтобы сохранялся порядок возрастания!");
			}
			GaugeCalculations.N = n;
			GaugeCalculations.A0 = a0;
			GaugeCalculations.Amax = amax;
			for (int i = 0; i < GaugeCalculations.Delta.Length; i++)
			{
				GaugeCalculations.Delta[i] = d[i];
			}
			GaugeCalculations.CompositeStep = step;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00004A18 File Offset: 0x00002C18
		public static void DefineVariables1(int n, double a0, double amax, double[] d, double step, bool complete)
		{
			GaugeCalculations.N = n;
			GaugeCalculations.A0 = a0;
			GaugeCalculations.Amax = amax;
			for (int i = 0; i < GaugeCalculations.Delta.Length; i++)
			{
				GaugeCalculations.Delta[i] = d[i];
			}
			GaugeCalculations.CompositeStep = step;
			if (complete)
			{
				GaugeCalculations.F = GaugeCalculations.F2(GaugeCalculations.Sets.Last<int[]>());
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00004A74 File Offset: 0x00002C74
		private static IEnumerable<int[]> GenerateSet(object param)
		{
			int[] ranges = (int[])param;
			int[] result = new int[5];
			for (int i = ranges[0]; i < ranges[1]; i++)
			{
				for (int j = 1; j <= GaugeCalculations.N - 4; j++)
				{
					for (int k = 1; k <= GaugeCalculations.N - 4; k++)
					{
						for (int l = 1; l <= GaugeCalculations.N - 4; l++)
						{
							for (int m = 1; m <= GaugeCalculations.N - 4; m++)
							{
								result = new int[]
								{
									i,
									j,
									k,
									l,
									m
								};
								if (result.Sum() >= GaugeCalculations.N)
								{
									if (result.Sum() > GaugeCalculations.N)
									{
										break;
									}
									yield return result;
								}
							}
						}
					}
				}
				GaugeCalculations.pBar.Value = i;
			}
			yield break;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004A94 File Offset: 0x00002C94
		private static bool TestConditions(int[] s)
		{
			if (!GaugeCalculations.IsDivisible(GaugeCalculations.A0, GaugeCalculations.Delta, GaugeCalculations.Amax, s))
			{
				return false;
			}
			double num = GaugeCalculations.F2(s);
			if ((int)(num / 1000.0) >= (int)(GaugeCalculations.F / 1000.0))
			{
				GaugeCalculations.Fprev = GaugeCalculations.F;
				return true;
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public static void SearchBestSet(bool auto, ProgressBar pBar1, Label lblStatus1, Label lblOperation1, RichTextBox rtbMain)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int[] param = new int[]
			{
				1,
				GaugeCalculations.N + 1
			};
			GaugeCalculations.Name = "Поиск набора с лучшими значениями универсальной функции F и коэффициента эффективности (КЕ)";
			GaugeCalculations.lblOperation = lblOperation1;
			GaugeCalculations.lblStatus = lblStatus1;
			GaugeCalculations.pBar = pBar1;
			GaugeCalculations.ProgressIdentifier(1, GaugeCalculations.N, GaugeCalculations.Name);
			GaugeCalculations.R2 = 0.0;
			GaugeCalculations.F = 0.0;
			GaugeCalculations.CurrentStep = 0L;
			GaugeCalculations.MaximalStep = (long)Math.Pow((double)GaugeCalculations.N, 5.0);
			GaugeCalculations.FSets.Clear();
			GaugeCalculations.RSets.Clear();
			GaugeCalculations.Sets.Clear();
			foreach (string path in GaugeCalculations.TempFiles)
			{
				File.Delete(path);
			}
			GaugeCalculations.TempFiles.Clear();
			foreach (int[] array in GaugeCalculations.GenerateSet(param))
			{
				if (GaugeCalculations.TestConditions(array))
				{
					if ((int)(GaugeCalculations.F2(array) / 1000.0) > (int)(GaugeCalculations.F / 1000.0))
					{
						GaugeCalculations.FSets.Clear();
						GaugeCalculations.F = GaugeCalculations.F2(array);
					}
					GaugeCalculations.FSets.Add(array);
				}
				GaugeCalculations.CurrentStep += 1L;
			}
			if (GaugeCalculations.FSets.Count == 0)
			{
				MessageBox.Show("Наборов, удовлетворяющих условию целочисленности (любой член группы делится нацело на значение разницы ∆ размеров в группе) не найдено.");
				return;
			}
			foreach (int[] item in GaugeCalculations.FSets)
			{
				GaugeCalculations.RSets.Add(item);
			}
			StringBuilder stringBuilder = new StringBuilder();
			GaugeCalculations.KE = 0.0;
			byte b = 0;
			foreach (int[] array2 in GaugeCalculations.RSets)
			{
				b += 1;
				GaugeCalculations.Sizes2 = GaugeCalculations.DefineSizes(array2);
				GaugeCalculations.SummarySizes2 = GaugeCalculations.Sizes2.Sum();
				if (auto)
				{
					GaugeCalculations.Zeroable();
					if (GaugeCalculations.CompositeBlocks != null)
					{
						GaugeCalculations.CompositeBlocks.Clear();
					}
					GaugeCalculations.DefineParameters(b, GaugeCalculations.RSets.Count);
					if (GaugeCalculations.KE2 >= GaugeCalculations.KE)
					{
						if (Math.Abs(GaugeCalculations.KE2 - GaugeCalculations.KE) > 0.0001)
						{
							GaugeCalculations.Sets.Clear();
							stringBuilder.Clear();
							foreach (string path2 in GaugeCalculations.TempFiles)
							{
								File.Delete(path2);
							}
							GaugeCalculations.TempFiles.Clear();
						}
						GaugeCalculations.Sets.Add(array2);
						GaugeCalculations.Sizes = (double[])GaugeCalculations.Sizes2.Clone();
						GaugeCalculations.SummarySizes = GaugeCalculations.SummarySizes2;
						GaugeCalculations.KE = GaugeCalculations.KE2;
						GaugeCalculations.SequenceCount = GaugeCalculations.SequenceCount2;
						GaugeCalculations.SequenceBegin = GaugeCalculations.SequenceBegin2;
						GaugeCalculations.SequenceEnd = GaugeCalculations.SequenceEnd2;
						GaugeCalculations.SummaryCount = GaugeCalculations.SummaryCount2;
					}
				}
				else
				{
					GaugeCalculations.Sets.Add(array2);
					GaugeCalculations.Sizes = GaugeCalculations.Sizes2;
					GaugeCalculations.SummarySizes = GaugeCalculations.SummarySizes2;
				}
				if (GaugeCalculations.Sets.Count > 0)
				{
					GaugeCalculations.F = GaugeCalculations.F2(GaugeCalculations.Sets.Last<int[]>());
					stringBuilder.Append(GaugeCalculations.Print(GaugeCalculations.Sets.Last<int[]>(), auto));
				}
				if (auto)
				{
					string tempFileName = Path.GetTempFileName();
					GaugeCalculations.TempFiles.Add(tempFileName);
					StreamWriter streamWriter = new StreamWriter(tempFileName, false);
					foreach (string value in GaugeCalculations.PrintComposite(GaugeCalculations.CompositeBlocks))
					{
						streamWriter.Write(value);
					}
					streamWriter.Close();
				}
			}
			rtbMain.Text += stringBuilder.ToString();
			stopwatch.Stop();
			GaugeCalculations.SummaryTime = stopwatch.Elapsed;
		}
		/*
		// Token: 0x06000025 RID: 37 RVA: 0x00004FBC File Offset: 0x000031BC
		public static void DefineParameters(ProgressBar pBar1, Label lblStatus1, Label lblOperation1, byte num, int count)
		{
			GaugeCalculations.Name = string.Concat(new object[]
			{
				"Генерация составленных мер для набора ",
				num,
				" из ",
				count
			});
			GaugeCalculations.lblOperation = lblOperation1;
			GaugeCalculations.lblStatus = lblStatus1;
			GaugeCalculations.pBar = pBar1;
			GaugeCalculations.ProgressIdentifier(1, GaugeCalculations.N, GaugeCalculations.Name);
			GaugeCalculations.SequenceCount2 = 0;
			GaugeCalculations.SequenceBegin2 = 0.0;
			GaugeCalculations.SequenceEnd2 = 0.0;
			GaugeCalculations.KE2 = 0.0;
			GaugeCalculations.SummaryCount2 = 0.0;
			GaugeCalculations.SummarySizes2 = GaugeCalculations.Sizes2.Sum();
			GaugeCalculations.CurrentStep = 0L;
			GaugeCalculations.GenerateComposite();
			GaugeCalculations.SummaryCount2 = (double)GaugeCalculations.CompositeBlocks.Count;
			GaugeCalculations.SearchMaxSequence(GaugeCalculations.CompositeBlocks);
			GaugeCalculations.KE2 = (double)GaugeCalculations.SequenceCount2 / GaugeCalculations.SummarySizes2 * (GaugeCalculations.SequenceEnd2 - GaugeCalculations.SequenceBegin2);
		}
		*/
		public static void DefineParameters(byte num, int count)
		{
			GaugeCalculations.Name = string.Concat(new object[]
			{
				"Генерация составленных мер для набора ",
				num,
				" из ",
				count
			});
			//GaugeCalculations.lblOperation = lblOperation1;
			//GaugeCalculations.lblStatus = lblStatus1;
			//GaugeCalculations.pBar = pBar1;
			//GaugeCalculations.ProgressIdentifier(1, GaugeCalculations.N, GaugeCalculations.Name);
			GaugeCalculations.SequenceCount2 = 0;
			GaugeCalculations.SequenceBegin2 = 0.0;
			GaugeCalculations.SequenceEnd2 = 0.0;
			GaugeCalculations.KE2 = 0.0;
			GaugeCalculations.SummaryCount2 = 0.0;
			GaugeCalculations.SummarySizes2 = GaugeCalculations.Sizes2.Sum();
			GaugeCalculations.CurrentStep = 0L;
			GaugeCalculations.GenerateComposite();
			GaugeCalculations.SummaryCount2 = (double)GaugeCalculations.CompositeBlocks.Count;
			GaugeCalculations.SearchMaxSequence(GaugeCalculations.CompositeBlocks);
			GaugeCalculations.KE2 = (double)GaugeCalculations.SequenceCount2 / GaugeCalculations.SummarySizes2 * (GaugeCalculations.SequenceEnd2 - GaugeCalculations.SequenceBegin2);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000050B0 File Offset: 0x000032B0
		private static void SearchMaxSequence(List<CompositeBlock> compositeBlocks)
		{
			double sum = compositeBlocks[0].sum;
			int num = 1;
			GaugeCalculations.SequenceCount2 = 0;
			BitArray bitArray = new BitArray(compositeBlocks.Count, true);
			for (int i = 0; i < compositeBlocks.Count; i++)
			{
				if (bitArray[i])
				{
					int num2 = i;
					bitArray[num2] = false;
					if (num == 1)
					{
						sum = compositeBlocks[num2].sum;
					}
					bool flag = true;
					int num3 = num2;
					while (flag && num3 < compositeBlocks.Count - 1)
					{
						num3++;
						if (Math.Abs(compositeBlocks[num3].sum - compositeBlocks[num2].sum) <= GaugeCalculations.CompositeStep + 1E-06)
						{
							if (Math.Abs(compositeBlocks[num3].sum - compositeBlocks[num2].sum - GaugeCalculations.CompositeStep) < 1E-06)
							{
								num2 = num3;
								bitArray[num2] = false;
								num++;
							}
						}
						else
						{
							flag = false;
							double sum2 = compositeBlocks[num2].sum;
							if (num > GaugeCalculations.SequenceCount2)
							{
								GaugeCalculations.SequenceCount2 = num;
								GaugeCalculations.SequenceBegin2 = sum;
								GaugeCalculations.SequenceEnd2 = sum2;
							}
							num = 1;
						}
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00005208 File Offset: 0x00003408
		private static bool IsDivisible(double a0, double[] delta, double amax, int[] set)
		{
			bool flag = true;
			flag = (flag && Math.Abs((a0 + (double)(set[0] - 1) * delta[0] + delta[1]) / delta[1] % 1.0) < 1E-10);
			flag = (flag && Math.Abs((a0 + (double)(set[0] - 1) * delta[0] + (double)(set[1] - 1) * delta[1] + delta[1] + delta[2]) / delta[2] % 1.0) < 1E-10);
			flag = (flag && Math.Abs((a0 + (double)(set[0] - 1) * delta[0] + (double)(set[1] - 1) * delta[1] + (double)(set[2] - 1) * delta[2] + delta[1] + delta[2] + delta[3]) / delta[3] % 1.0) < 1E-10);
			flag = (flag && Math.Abs((a0 + (double)(set[0] - 1) * delta[0] + (double)(set[1] - 1) * delta[1] + (double)(set[2] - 1) * delta[2] + (double)(set[3] - 1) * delta[3] + delta[1] + delta[2] + delta[3] + delta[4]) / delta[4] % 1.0) < 1E-10);
			return flag && a0 + (double)(set[0] - 1) * delta[0] + (double)(set[1] - 1) * delta[1] + (double)(set[2] - 1) * delta[2] + (double)(set[3] - 1) * delta[3] + (double)(set[4] - 1) * delta[4] + delta[1] + delta[2] + delta[3] + delta[4] <= amax;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000539C File Offset: 0x0000359C
		public static double DefineEquationCoefficients(int[] set)
		{
			double[] coefficients = new double[2];
			double num = 0.0;
			double num2 = (double)set.Sum();
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			for (int j = 0; j < set.Length; j++)
			{
				num3 += (double)(set[j] * (j + 1));
				num += (double)(j + 1);
				num4 += (double)((j + 1) * (j + 1));
				num5 += (double)(set[j] * set[j]);
			}
			coefficients[0] = ((double)set.Length * num3 - num * num2) / ((double)set.Length * num4 - num * num);
			coefficients[1] = (num2 - coefficients[0] * num) / (double)set.Length;
			double num6 = num5 - num2 * num2 / (double)set.Length;
			double num7 = set.Select((int t, int i) => Math.Pow((double)t - (coefficients[0] * (double)(i + 1) + coefficients[1]), 2.0)).Sum();
			return 1.0 - num7 / num6;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000054B0 File Offset: 0x000036B0
		public static double F2(int[] gaugeSet)
		{
			double num = Math.Pow(5.0 * GaugeCalculations.A0 / GaugeCalculations.Delta[0] - 4.0 + (double)(5 * gaugeSet[0]) + (double)(5 * gaugeSet[1]) * GaugeCalculations.Delta[1] / GaugeCalculations.Delta[0] + (double)(5 * gaugeSet[2]) * GaugeCalculations.Delta[2] / GaugeCalculations.Delta[0] + (double)(5 * gaugeSet[3]) * GaugeCalculations.Delta[3] / GaugeCalculations.Delta[0] + (double)(5 * gaugeSet[4]) * GaugeCalculations.Delta[4] / GaugeCalculations.Delta[0] - 10.0 * GaugeCalculations.Delta[4] / GaugeCalculations.Delta[0], 2.0) * GaugeCalculations.Delta[0];
			double num2 = GaugeCalculations.A0 * (double)(gaugeSet[0] + gaugeSet[1] + gaugeSet[2] + gaugeSet[3] + gaugeSet[4]) + (double)(gaugeSet[0] - 1) * (0.5 * (double)gaugeSet[0] + (double)gaugeSet[1] + (double)gaugeSet[2] + (double)gaugeSet[3] + (double)gaugeSet[4]) * GaugeCalculations.Delta[0] + (double)gaugeSet[1] * (0.5 * (double)gaugeSet[1] + (double)gaugeSet[2] + (double)gaugeSet[3] + (double)gaugeSet[4]) * GaugeCalculations.Delta[1] + (double)gaugeSet[2] * (0.5 * (double)gaugeSet[2] + (double)gaugeSet[3] + (double)gaugeSet[4]) * GaugeCalculations.Delta[2] + (double)gaugeSet[3] * (0.5 * (double)gaugeSet[3] + (double)gaugeSet[4]) * GaugeCalculations.Delta[3] + 0.5 * (double)gaugeSet[4] * (double)gaugeSet[4] * GaugeCalculations.Delta[4] + 0.5 * (GaugeCalculations.Delta[1] * (double)gaugeSet[1] + GaugeCalculations.Delta[2] * (double)gaugeSet[2] + GaugeCalculations.Delta[3] * (double)gaugeSet[3] + GaugeCalculations.Delta[4] * (double)gaugeSet[4]);
			return num / num2;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00005690 File Offset: 0x00003890
		public static double[] DefineSizes(int[] set)
		{
			double num = GaugeCalculations.A0;
			double[] array = new double[set.Sum()];
			int num2 = 0;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < set[i]; j++)
				{
					array[num2] = num + (double)j * GaugeCalculations.Delta[i];
					num2++;
				}
				if (i != 4)
				{
					num = array[num2 - 1] + GaugeCalculations.Delta[i + 1];
				}
			}
			return array;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000056F8 File Offset: 0x000038F8
		private static void GenerateComposite()
		{
			GaugeCalculations.CompositeBlocks.Clear();
			GaugeCalculations.CompositeComparer cc = new GaugeCalculations.CompositeComparer();
			List<CompositeBlock> list = new List<CompositeBlock>((int)Math.Pow((double)GaugeCalculations.N, 3.0) + 1);
			List<CompositeBlock> list2 = new List<CompositeBlock>((int)Math.Pow((double)GaugeCalculations.N, 3.0) + 1);
			byte b = 0;
			while ((int)b < GaugeCalculations.N)
			{
				list.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)b], 4), (byte)(b + 1), 0, 0, 0, 0));
				b += 1;
			}
			byte b2 = 0;
			while ((int)b2 < GaugeCalculations.N)
			{
				byte b3 = (byte)(b2 + 1);
				while ((int)b3 < GaugeCalculations.N)
				{
					list2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)b2] + GaugeCalculations.Sizes2[(int)b3], 4), (byte)(b2 + 1), (byte)(b3 + 1), 0, 0, 0));
					b3 += 1;
				}
				b2 += 1;
			}
			list = list.Union(list2, cc).ToList<CompositeBlock>();
			list2.Clear();
			GC.Collect();
			byte b4 = 0;
			while ((int)b4 < GaugeCalculations.N)
			{
				byte b5 = (byte)(b4 + 1);
				while ((int)b5 < GaugeCalculations.N)
				{
					byte b6 = (byte)(b5 + 1);
					while ((int)b6 < GaugeCalculations.N)
					{
						list2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)b4] + GaugeCalculations.Sizes2[(int)b5] + GaugeCalculations.Sizes2[(int)b6], 4), (byte)(b4 + 1), (byte)(b5 + 1), (byte)(b6 + 1), 0, 0));
						b6 += 1;
					}
					b5 += 1;
				}
				b4 += 1;
			}
			list = list.Union(list2, cc).ToList<CompositeBlock>();
			list2.Clear();
			GC.Collect();
			list = GaugeCalculations.BigGenerateComposite().Aggregate(list, (List<CompositeBlock> current, List<CompositeBlock> test) => current.Union(test, cc).ToList<CompositeBlock>());
			GaugeCalculations.CompositeBlocks = list;
			GaugeCalculations.CompositeBlocks.Sort(cc);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000058D4 File Offset: 0x00003AD4
		private static IEnumerable<List<CompositeBlock>> BigGenerateComposite()
		{
			List<CompositeBlock> test2 = new List<CompositeBlock>();
			byte b = 0;
			while ((int)b < GaugeCalculations.N)
			{
				byte b2 = (byte)(b + 1);
				while ((int)b2 < GaugeCalculations.N)
				{
					byte b3 = (byte)(b2 + 1);
					while ((int)b3 < GaugeCalculations.N)
					{
						byte b4 = (byte)(b3 + 1);
						while ((int)b4 < GaugeCalculations.N)
						{
							test2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)b] + GaugeCalculations.Sizes2[(int)b2] + GaugeCalculations.Sizes2[(int)b3] + GaugeCalculations.Sizes2[(int)b4], 4), (byte)(b + 1), (byte)(b2 + 1), (byte)(b3 + 1), (byte)(b4 + 1), 0));
							b4 += 1;
						}
						b3 += 1;
					}
					b2 += 1;
				}
				//GaugeCalculations.pBar.Value = (int)(b + 1);
				b += 1;
			}
			yield return test2;
			test2.Clear();
			GC.Collect();
			byte i = 0;
			while ((int)i < GaugeCalculations.N)
			{
				byte b5 = (byte)(i + 1);
				while ((int)b5 < GaugeCalculations.N)
				{
					byte b6 = (byte)(b5 + 1);
					while ((int)b6 < GaugeCalculations.N)
					{
						byte b7 = (byte)(b6 + 1);
						while ((int)b7 < GaugeCalculations.N)
						{
							byte b8 = (byte)(b7 + 1);
							while ((int)b8 < GaugeCalculations.N)
							{
								test2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)i] + GaugeCalculations.Sizes2[(int)b5] + GaugeCalculations.Sizes2[(int)b6] + GaugeCalculations.Sizes2[(int)b7] + GaugeCalculations.Sizes2[(int)b8], 4), (byte)(i + 1), (byte)(b5 + 1), (byte)(b6 + 1), (byte)(b7 + 1), (byte)(b8 + 1)));
								b8 += 1;
							}
							b7 += 1;
						}
						b6 += 1;
					}
					b5 += 1;
				}
				//GaugeCalculations.pBar.Value = (int)(i + 1);
				yield return test2;
				test2.Clear();
				GC.Collect();
				i += 1;
			}
			yield break;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000058EC File Offset: 0x00003AEC
		private static IEnumerable<List<CompositeBlock>> AverageGenerateComposite(int p)
		{
			List<CompositeBlock> test2 = new List<CompositeBlock>((int)(Math.Pow((double)GaugeCalculations.N, 4.0) / (double)p) + 1);
			byte i = 0;
			while ((int)i < GaugeCalculations.N)
			{
				byte b = (byte)(i + 1);
				while ((int)b < GaugeCalculations.N)
				{
					byte b2 = (byte)(b + 1);
					while ((int)b2 < GaugeCalculations.N)
					{
						byte b3 = (byte)(b2 + 1);
						while ((int)b3 < GaugeCalculations.N)
						{
							test2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)i] + GaugeCalculations.Sizes2[(int)b] + GaugeCalculations.Sizes2[(int)b2] + GaugeCalculations.Sizes2[(int)b3], 4), (byte)(i + 1), (byte)(b + 1), (byte)(b2 + 1), (byte)(b3 + 1), 0));
							b3 += 1;
						}
						b2 += 1;
					}
					b += 1;
				}
				//GaugeCalculations.pBar.Value = (int)(i + 1);
				if (test2.Count > test2.Capacity / 2)
				{
					yield return test2;
					test2.Clear();
					GC.Collect();
				}
				i += 1;
			}
			byte j = 0;
			while ((int)j < GaugeCalculations.N)
			{
				byte k = (byte)(j + 1);
				while ((int)k < GaugeCalculations.N)
				{
					byte b4 = (byte)(k + 1);
					while ((int)b4 < GaugeCalculations.N)
					{
						byte b5 = (byte)(b4 + 1);
						while ((int)b5 < GaugeCalculations.N)
						{
							byte b6 = (byte)(b5 + 1);
							while ((int)b6 < GaugeCalculations.N)
							{
								test2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)j] + GaugeCalculations.Sizes2[(int)k] + GaugeCalculations.Sizes2[(int)b4] + GaugeCalculations.Sizes2[(int)b5] + GaugeCalculations.Sizes2[(int)b6], 4), (byte)(j + 1), (byte)(k + 1), (byte)(b4 + 1), (byte)(b5 + 1), (byte)(b6 + 1)));
								b6 += 1;
							}
							b5 += 1;
						}
						b4 += 1;
					}
					if (test2.Count > test2.Capacity / 2)
					{
						yield return test2;
						test2.Clear();
						GC.Collect();
					}
					k += 1;
				}
				GaugeCalculations.pBar.Value = (int)(j + 1);
				j += 1;
			}
			yield break;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000590C File Offset: 0x00003B0C
		private static IEnumerable<List<CompositeBlock>> AdditionalGenerateComposite()
		{
			List<CompositeBlock> test2 = new List<CompositeBlock>((int)Math.Pow((double)GaugeCalculations.N, 3.0) + 1);
			byte i = 0;
			while ((int)i < GaugeCalculations.N)
			{
				byte b = (byte)(i + 1);
				while ((int)b < GaugeCalculations.N)
				{
					byte b2 = (byte)(b + 1);
					while ((int)b2 < GaugeCalculations.N)
					{
						byte b3 = (byte)(b2 + 1);
						while ((int)b3 < GaugeCalculations.N)
						{
							test2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)i] + GaugeCalculations.Sizes2[(int)b] + GaugeCalculations.Sizes2[(int)b2] + GaugeCalculations.Sizes2[(int)b3], 4), (byte)(i + 1), (byte)(b + 1), (byte)(b2 + 1), (byte)(b3 + 1), 0));
							b3 += 1;
						}
						b2 += 1;
					}
					b += 1;
				}
				GaugeCalculations.pBar.Value = (int)(i + 1);
				yield return test2;
				test2.Clear();
				GC.Collect();
				i += 1;
			}
			byte j = 0;
			while ((int)j < GaugeCalculations.N)
			{
				byte k = (byte)(j + 1);
				while ((int)k < GaugeCalculations.N)
				{
					byte b4 = (byte)(k + 1);
					while ((int)b4 < GaugeCalculations.N)
					{
						byte b5 = (byte)(b4 + 1);
						while ((int)b5 < GaugeCalculations.N)
						{
							byte b6 = (byte)(b5 + 1);
							while ((int)b6 < GaugeCalculations.N)
							{
								test2.Add(new CompositeBlock(Math.Round(GaugeCalculations.Sizes2[(int)j] + GaugeCalculations.Sizes2[(int)k] + GaugeCalculations.Sizes2[(int)b4] + GaugeCalculations.Sizes2[(int)b5] + GaugeCalculations.Sizes2[(int)b6], 4), (byte)(j + 1), (byte)(k + 1), (byte)(b4 + 1), (byte)(b5 + 1), (byte)(b6 + 1)));
								b6 += 1;
							}
							b5 += 1;
						}
						b4 += 1;
					}
					GaugeCalculations.pBar.Value = (int)(j + 1);
					yield return test2;
					test2.Clear();
					GC.Collect();
					k += 1;
				}
				j += 1;
			}
			yield break;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00005924 File Offset: 0x00003B24
		public static int[] TranslationFrom10ToN(long n)
		{
			List<int> list = new List<int>(5);
			do
			{
				list.Add((int)(n % (long)GaugeCalculations.N));
				n /= (long)GaugeCalculations.N;
			}
			while (n != 0L);
			return list.ToArray();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000217C File Offset: 0x0000037C
		private static void ProgressIdentifier(int beg, int end, string name)
		{
			GaugeCalculations.lblStatus.Text = name;
			GaugeCalculations.lblStatus.Update();
			GaugeCalculations.pBar.Minimum = beg;
			GaugeCalculations.pBar.Maximum = end;
			GaugeCalculations.pBar.Value = beg;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00005964 File Offset: 0x00003B64
		public static string Print(int[] Ns, bool full)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\nЗначение универсальной функции F = " + GaugeCalculations.F + "\r\n");
			stringBuilder.Append("Количество мер в первой группе = " + Ns[0] + "\r\n");
			stringBuilder.Append("Количество мер во второй группе = " + Ns[1] + "\r\n");
			stringBuilder.Append("Количество мер в третьей группе = " + Ns[2] + "\r\n");
			stringBuilder.Append("Количество мер в четвертой группе = " + Ns[3] + "\r\n");
			stringBuilder.Append("Количество мер в пятой группе = " + Ns[4] + "\r\n\n");
			foreach (double num in GaugeCalculations.Sizes)
			{
				stringBuilder.AppendFormat("{0:0.###}  ", num);
			}
			stringBuilder.AppendFormat("\r\n\nСуммарная длина мер набора = {0} мм\r\n", GaugeCalculations.SummarySizes);
			if (full)
			{
				stringBuilder.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", GaugeCalculations.SummaryCount);
				stringBuilder.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом (КСР) = {0}\r\n", GaugeCalculations.SequenceCount);
				stringBuilder.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", GaugeCalculations.SequenceBegin);
				stringBuilder.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", GaugeCalculations.SequenceEnd);
				stringBuilder.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
				stringBuilder.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", GaugeCalculations.KE);
			}
			stringBuilder.Append("\n\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005B08 File Offset: 0x00003D08
		public static string Print1(int[] Ns, bool full, bool complete)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (complete)
			{
				stringBuilder.Append("\nЗначение универсальной функции F = " + GaugeCalculations.F + "\r\n");
				stringBuilder.Append("Количество мер в первой группе = " + Ns[0] + "\r\n");
				stringBuilder.Append("Количество мер во второй группе = " + Ns[1] + "\r\n");
				stringBuilder.Append("Количество мер в третьей группе = " + Ns[2] + "\r\n");
				stringBuilder.Append("Количество мер в четвертой группе = " + Ns[3] + "\r\n");
				stringBuilder.Append("Количество мер в пятой группе = " + Ns[4] + "\r\n\n");
			}
			foreach (double num in GaugeCalculations.Sizes)
			{
				stringBuilder.AppendFormat("{0:0.###}  ", num);
			}
			stringBuilder.AppendFormat("\r\n\nСуммарная длина мер набора = {0} мм\r\n", GaugeCalculations.SummarySizes);
			if (full)
			{
				stringBuilder.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", GaugeCalculations.SummaryCount);
				stringBuilder.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом (КСР) = {0}\r\n", GaugeCalculations.SequenceCount);
				stringBuilder.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", GaugeCalculations.SequenceBegin);
				stringBuilder.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", GaugeCalculations.SequenceEnd);
				stringBuilder.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
				stringBuilder.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", GaugeCalculations.KE);
			}
			stringBuilder.Append("\n\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00005CB0 File Offset: 0x00003EB0
		public static string PrintSet(int[] Ns)
		{
			GaugeCalculations.Sizes = GaugeCalculations.DefineSizes(Ns);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}\r\n", GaugeCalculations.N);
			foreach (double num in GaugeCalculations.Sizes)
			{
				stringBuilder.AppendFormat("{0:0.####} ", num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00005D18 File Offset: 0x00003F18
		public static IEnumerable<string> PrintComposite(List<CompositeBlock> cbList)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("\nКоличество составленных мер (без повторений) = {0}\r\n", GaugeCalculations.SummaryCount);
			sb.AppendFormat("\nКоличество составленных мер наиболее длинного участка с заданным шагом(КСР) = {0}\r\n", GaugeCalculations.SequenceCount);
			sb.AppendFormat("\nНижняя граница выбранного участка (наименьший составленный размер, НГУ) = {0} мм\r\n", GaugeCalculations.SequenceBegin);
			sb.AppendFormat("\nВерхняя граница выбранного участка (наибольший составленный размер, ВГУ) = {0} мм\r\n", GaugeCalculations.SequenceEnd);
			sb.AppendFormat("\nКритерии эффективности набора:\r\n", new object[0]);
			sb.AppendFormat("КЭ = (КСР / СДМ) * (ВГУ - НГУ) = {0}\r\n", GaugeCalculations.KE);
			yield return sb.ToString();
			sb.Clear();
			foreach (CompositeBlock compositeBlock in cbList)
			{
				StringBuilder stringBuilder = sb;
				string format = "\r\n{0:0.####} (";
				CompositeBlock compositeBlock2 = compositeBlock;
				stringBuilder.AppendFormat(format, compositeBlock2.sum);
				StringBuilder stringBuilder2 = sb;
				CompositeBlock compositeBlock3 = compositeBlock;
				string format2;
				if (compositeBlock3.set1 != 0)
				{
					object arg = " ";
					double[] sizes = GaugeCalculations.Sizes;
					CompositeBlock compositeBlock4 = compositeBlock;
					format2 = arg.ToString() + sizes[(int)(compositeBlock4.set1 - 1)];
				}
				else
				{
					format2 = "";
				}
				stringBuilder2.AppendFormat(format2, new object[0]);
				StringBuilder stringBuilder3 = sb;
				CompositeBlock compositeBlock5 = compositeBlock;
				string format3;
				if (compositeBlock5.set2 != 0)
				{
					object arg2 = " ";
					double[] sizes2 = GaugeCalculations.Sizes;
					CompositeBlock compositeBlock6 = compositeBlock;
					format3 = arg2.ToString() + sizes2[(int)(compositeBlock6.set2 - 1)];
				}
				else
				{
					format3 = "";
				}
				stringBuilder3.AppendFormat(format3, new object[0]);
				StringBuilder stringBuilder4 = sb;
				CompositeBlock compositeBlock7 = compositeBlock;
				string format4;
				if (compositeBlock7.set3 != 0)
				{
					object arg3 = " ";
					double[] sizes3 = GaugeCalculations.Sizes;
					CompositeBlock compositeBlock8 = compositeBlock;
					format4 = arg3.ToString() + sizes3[(int)(compositeBlock8.set3 - 1)];
				}
				else
				{
					format4 = "";
				}
				stringBuilder4.AppendFormat(format4, new object[0]);
				StringBuilder stringBuilder5 = sb;
				CompositeBlock compositeBlock9 = compositeBlock;
				string format5;
				if (compositeBlock9.set4 != 0)
				{
					object arg4 = " ";
					double[] sizes4 = GaugeCalculations.Sizes;
					CompositeBlock compositeBlock10 = compositeBlock;
					format5 = arg4.ToString() + sizes4[(int)(compositeBlock10.set4 - 1)];
				}
				else
				{
					format5 = "";
				}
				stringBuilder5.AppendFormat(format5, new object[0]);
				StringBuilder stringBuilder6 = sb;
				CompositeBlock compositeBlock11 = compositeBlock;
				string format6;
				if (compositeBlock11.set5 != 0)
				{
					object arg5 = " ";
					double[] sizes5 = GaugeCalculations.Sizes;
					CompositeBlock compositeBlock12 = compositeBlock;
					format6 = arg5.ToString() + sizes5[(int)(compositeBlock12.set5 - 1)];
				}
				else
				{
					format6 = "";
				}
				stringBuilder6.AppendFormat(format6, new object[0]);
				sb.Append(" )\r\n");
				yield return sb.ToString();
				sb.Clear();
			}
			yield break;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00005D38 File Offset: 0x00003F38
		private static void Zeroable()
		{
			GaugeCalculations.SequenceCount = 0;
			GaugeCalculations.SequenceCount2 = 0;
			GaugeCalculations.SequenceBegin = 0.0;
			GaugeCalculations.SequenceBegin2 = 0.0;
			GaugeCalculations.SequenceEnd = 0.0;
			GaugeCalculations.SequenceEnd2 = 0.0;
			GaugeCalculations.KE = 0.0;
			GaugeCalculations.KE2 = 0.0;
			GaugeCalculations.SummarySizes = 0.0;
			GaugeCalculations.SummarySizes2 = 0.0;
			GaugeCalculations.SummaryCount = 0.0;
			GaugeCalculations.SummaryCount2 = 0.0;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public static string FindCompositeBlock(double i)
		{
			if (GaugeCalculations.CompositeBlocks.Count == 0)
			{
				return "Составные меры еще не сгенерированы, массив пуст.";
			}
			CompositeBlock item2 = GaugeCalculations.CompositeBlocks.FirstOrDefault((CompositeBlock item) => Math.Abs(item.sum - i) < 1E-06);
			if (item2.Equals(GaugeCalculations.CompositeBlocks.ElementAtOrDefault(-1)))
			{
				return "Меры такой длины не существует!";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}/{1}", GaugeCalculations.CompositeBlocks.IndexOf(item2), GaugeCalculations.CompositeBlocks.Count);
			stringBuilder.AppendFormat("\r\n{0:0.####} (", item2.sum);
			stringBuilder.AppendFormat((item2.set1 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(item2.set1 - 1)]), new object[0]);
			stringBuilder.AppendFormat((item2.set2 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(item2.set2 - 1)]), new object[0]);
			stringBuilder.AppendFormat((item2.set3 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(item2.set3 - 1)]), new object[0]);
			stringBuilder.AppendFormat((item2.set4 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(item2.set4 - 1)]), new object[0]);
			stringBuilder.AppendFormat((item2.set5 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(item2.set5 - 1)]), new object[0]);
			stringBuilder.Append(" )\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00005FCC File Offset: 0x000041CC
		public static string ShowCompositeBlock(int i)
		{
			if (GaugeCalculations.CompositeBlocks.Count == 0)
			{
				return "Составные меры еще не сгенерированы, массив пуст.";
			}
			if (i - 1 >= GaugeCalculations.CompositeBlocks.Count || i - 1 < 0)
			{
				return "Такой меры в массиве нет!";
			}
			CompositeBlock compositeBlock = GaugeCalculations.CompositeBlocks[i - 1];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}/{1}", i, GaugeCalculations.CompositeBlocks.Count);
			stringBuilder.AppendFormat("\r\n{0:0.####} (", compositeBlock.sum);
			stringBuilder.AppendFormat((compositeBlock.set1 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(compositeBlock.set1 - 1)]), new object[0]);
			stringBuilder.AppendFormat((compositeBlock.set2 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(compositeBlock.set2 - 1)]), new object[0]);
			stringBuilder.AppendFormat((compositeBlock.set3 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(compositeBlock.set3 - 1)]), new object[0]);
			stringBuilder.AppendFormat((compositeBlock.set4 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(compositeBlock.set4 - 1)]), new object[0]);
			stringBuilder.AppendFormat((compositeBlock.set5 == 0) ? "" : (" " + GaugeCalculations.Sizes[(int)(compositeBlock.set5 - 1)]), new object[0]);
			stringBuilder.Append(" )\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x04000037 RID: 55
		private const double E = 1E-06;

		// Token: 0x04000038 RID: 56
		public const int MINIMAL_VALUE = 25;

		// Token: 0x04000039 RID: 57
		public const int MAXIMAL_VALUE = 112;

		// Token: 0x0400003A RID: 58
		public const int GROUP_COUNT = 5;

		// Token: 0x0400003B RID: 59
		public static int N = 83;

		// Token: 0x0400003C RID: 60
		private static double A0 = 0.5;

		// Token: 0x0400003D RID: 61
		private static double Amax = 100.0;

		// Token: 0x0400003E RID: 62
		public static double[] Delta = new double[]
		{
			0.005,
			0.01,
			0.1,
			0.5,
			10.0
		};

		// Token: 0x0400003F RID: 63
		public static double R2 = 0.0;

		// Token: 0x04000040 RID: 64
		public static double R2prev = 0.0;

		// Token: 0x04000041 RID: 65
		public static double F = 0.0;

		// Token: 0x04000042 RID: 66
		public static double Fprev = 0.0;

		// Token: 0x04000043 RID: 67
		public static List<int[]> FSets = new List<int[]>();

		// Token: 0x04000044 RID: 68
		public static List<int[]> RSets = new List<int[]>();

		// Token: 0x04000045 RID: 69
		public static List<int[]> Sets = new List<int[]>();

		// Token: 0x04000046 RID: 70
		public static double[] Sizes;

		// Token: 0x04000047 RID: 71
		public static double[] Sizes2;

		// Token: 0x04000048 RID: 72
		public static List<CompositeBlock> CompositeBlocks = new List<CompositeBlock>();

		// Token: 0x04000049 RID: 73
		public static long CurrentStep = 0L;

		// Token: 0x0400004A RID: 74
		public static string Name;

		// Token: 0x0400004B RID: 75
		public static long MaximalStep = 0L;

		// Token: 0x0400004C RID: 76
		public static double CompositeStep = 0.005;

		// Token: 0x0400004D RID: 77
		public static int SequenceCount = 0;

		// Token: 0x0400004E RID: 78
		public static int SequenceCount2 = 0;

		// Token: 0x0400004F RID: 79
		public static double SequenceBegin = 0.0;

		// Token: 0x04000050 RID: 80
		public static double SequenceBegin2 = 0.0;

		// Token: 0x04000051 RID: 81
		public static double SequenceEnd = 0.0;

		// Token: 0x04000052 RID: 82
		public static double SequenceEnd2 = 0.0;

		// Token: 0x04000053 RID: 83
		public static double KE = 0.0;

		// Token: 0x04000054 RID: 84
		public static double KE2 = 0.0;

		// Token: 0x04000055 RID: 85
		public static double SummarySizes = 0.0;

		// Token: 0x04000056 RID: 86
		public static double SummarySizes2 = 0.0;

		// Token: 0x04000057 RID: 87
		public static double SummaryCount = 0.0;

		// Token: 0x04000058 RID: 88
		public static double SummaryCount2 = 0.0;

		// Token: 0x04000059 RID: 89
		public static TimeSpan SummaryTime;

		// Token: 0x0400005A RID: 90
		public static ProgressBar pBar;

		// Token: 0x0400005B RID: 91
		public static Label lblOperation;

		// Token: 0x0400005C RID: 92
		public static Label lblStatus;

		// Token: 0x0400005D RID: 93
		public static List<string> TempFiles = new List<string>();

		// Token: 0x02000006 RID: 6
		private class CompositeComparer : IEqualityComparer<CompositeBlock>, IComparer<CompositeBlock>
		{
			// Token: 0x0600003A RID: 58 RVA: 0x000021BC File Offset: 0x000003BC
			public bool Equals(CompositeBlock x, CompositeBlock y)
			{
				return Math.Abs(x.sum - y.sum) < 1E-06;
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000062F0 File Offset: 0x000044F0
			public int GetHashCode(CompositeBlock block)
			{
				return block.sum.GetHashCode();
			}

			// Token: 0x0600003C RID: 60 RVA: 0x000021DD File Offset: 0x000003DD
			public int Compare(CompositeBlock x, CompositeBlock y)
			{
				if (x.Equals(y))
				{
					return 0;
				}
				if (x.sum > y.sum)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
