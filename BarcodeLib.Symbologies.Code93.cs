// BarcodeLib, Version=1.0.0.11, Culture=neutral, PublicKeyToken=null
// BarcodeLib.Symbologies.Code93
using System.Data;
using BarcodeLib;

internal class Code93 : BarcodeCommon, IBarcode
{
	private DataTable C93_Code = new DataTable("C93_Code");

	public string Encoded_Value => Encode_Code93();

	public Code93(string input)
	{
		Raw_Data = input;
	}

	private string Encode_Code93()
	{
		init_Code93();
		string text = Add_CheckDigits(Raw_Data);
		string text2 = C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();
		string text3 = text;
		for (int i = 0; i < text3.Length; i++)
		{
			char c = text3[i];
			try
			{
				text2 += C93_Code.Select("Character = '" + c + "'")[0]["Encoding"].ToString();
			}
			catch
			{
				Error("EC93-1: Invalid data.");
			}
		}
		text2 += C93_Code.Select("Character = '*'")[0]["Encoding"].ToString();
		text2 += "1";
		C93_Code.Clear();
		return text2;
	}

	private void init_Code93()
	{
		C93_Code.Rows.Clear();
		C93_Code.Columns.Clear();
		C93_Code.Columns.Add("Value");
		C93_Code.Columns.Add("Character");
		C93_Code.Columns.Add("Encoding");
		C93_Code.Rows.Add("0", "0", "100010100");
		C93_Code.Rows.Add("1", "1", "101001000");
		C93_Code.Rows.Add("2", "2", "101000100");
		C93_Code.Rows.Add("3", "3", "101000010");
		C93_Code.Rows.Add("4", "4", "100101000");
		C93_Code.Rows.Add("5", "5", "100100100");
		C93_Code.Rows.Add("6", "6", "100100010");
		C93_Code.Rows.Add("7", "7", "101010000");
		C93_Code.Rows.Add("8", "8", "100010010");
		C93_Code.Rows.Add("9", "9", "100001010");
		C93_Code.Rows.Add("10", "A", "110101000");
		C93_Code.Rows.Add("11", "B", "110100100");
		C93_Code.Rows.Add("12", "C", "110100010");
		C93_Code.Rows.Add("13", "D", "110010100");
		C93_Code.Rows.Add("14", "E", "110010010");
		C93_Code.Rows.Add("15", "F", "110001010");
		C93_Code.Rows.Add("16", "G", "101101000");
		C93_Code.Rows.Add("17", "H", "101100100");
		C93_Code.Rows.Add("18", "I", "101100010");
		C93_Code.Rows.Add("19", "J", "100110100");
		C93_Code.Rows.Add("20", "K", "100011010");
		C93_Code.Rows.Add("21", "L", "101011000");
		C93_Code.Rows.Add("22", "M", "101001100");
		C93_Code.Rows.Add("23", "N", "101000110");
		C93_Code.Rows.Add("24", "O", "100101100");
		C93_Code.Rows.Add("25", "P", "100010110");
		C93_Code.Rows.Add("26", "Q", "110110100");
		C93_Code.Rows.Add("27", "R", "110110010");
		C93_Code.Rows.Add("28", "S", "110101100");
		C93_Code.Rows.Add("29", "T", "110100110");
		C93_Code.Rows.Add("30", "U", "110010110");
		C93_Code.Rows.Add("31", "V", "110011010");
		C93_Code.Rows.Add("32", "W", "101101100");
		C93_Code.Rows.Add("33", "X", "101100110");
		C93_Code.Rows.Add("34", "Y", "100110110");
		C93_Code.Rows.Add("35", "Z", "100111010");
		C93_Code.Rows.Add("36", "-", "100101110");
		C93_Code.Rows.Add("37", ".", "111010100");
		C93_Code.Rows.Add("38", " ", "111010010");
		C93_Code.Rows.Add("39", "$", "111001010");
		C93_Code.Rows.Add("40", "/", "101101110");
		C93_Code.Rows.Add("41", "+", "101110110");
		C93_Code.Rows.Add("42", "%", "110101110");
		C93_Code.Rows.Add("43", "(", "100100110");
		C93_Code.Rows.Add("44", ")", "111011010");
		C93_Code.Rows.Add("45", "#", "111010110");
		C93_Code.Rows.Add("46", "@", "100110010");
		C93_Code.Rows.Add("-", "*", "101011110");
	}

	private string Add_CheckDigits(string input)
	{
		int[] array = new int[input.Length];
		int num = 1;
		for (int num2 = input.Length - 1; num2 >= 0; num2--)
		{
			if (num > 20)
			{
				num = 1;
			}
			array[num2] = num;
			num++;
		}
		int[] array2 = new int[input.Length + 1];
		num = 1;
		for (int num2 = input.Length; num2 >= 0; num2--)
		{
			if (num > 15)
			{
				num = 1;
			}
			array2[num2] = num;
			num++;
		}
		int num3 = 0;
		for (int num2 = 0; num2 < input.Length; num2++)
		{
			num3 += array[num2] * int.Parse(C93_Code.Select("Character = '" + input[num2] + "'")[0]["Value"].ToString());
		}
		int num4 = num3 % 47;
		input += C93_Code.Select("Value = '" + num4 + "'")[0]["Character"].ToString();
		num3 = 0;
		for (int num2 = 0; num2 < input.Length; num2++)
		{
			num3 += array2[num2] * int.Parse(C93_Code.Select("Character = '" + input[num2] + "'")[0]["Value"].ToString());
		}
		num4 = num3 % 47;
		input += C93_Code.Select("Value = '" + num4 + "'")[0]["Character"].ToString();
		return input;
	}
}
