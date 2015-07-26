using System;
using System.Collections;
using System.Text;
using System.Timers;

namespace ClaimAcademySolutions
{
	class MainClass
	{
		private static string DetermineOddEven(int number)
		{
			string ret = (number % 2 == 0)? "Even": "Odd";
			return ret;
		}

		private static Boolean DetermineDivisibility(int number)
		{
			string message;
			Boolean dividableBy3 = (number % 3 == 0)? true: false;
			Boolean dividableBy5 = (number % 5 == 0)? true: false;

			if (dividableBy3 && dividableBy5)
				message = "Divisible by both 3 and 5";
			else if (dividableBy3 && !dividableBy5)
				message = "Divisible by 3";
			else if (!dividableBy3 && dividableBy5)
				message = "Divisible by 5";
			else {
				Console.WriteLine("Indivisible by 3 or 5");
				return false;
			}

			Console.WriteLine(message);
			return true;
		}

		private static StringBuilder ChangeNumbers(int number)
		{
			if (number == 0)
				return new StringBuilder ("zero");

			StringBuilder stringBuilder = new StringBuilder ();
			int hundred, ten, one;
			int currentNumber;
			ArrayList dividedByThousand = new ArrayList();

			Hashtable ones = new Hashtable {
				{ 1, "one" },
				{ 2, "two" },
				{ 3, "three" },
				{ 4, "four" },
				{ 5, "five" },
				{ 6, "six" },
				{ 7, "seven" },
				{ 8, "eight" },
				{ 9, "nine" }
			};

			Hashtable teens = new Hashtable {
				{ 10, "ten"},
				{ 11, "evelen"},
				{ 12, "twelve"},
				{ 13, "thirteen"},
				{ 14, "fourteen"},
				{ 15, "fifteen"},
				{ 16, "sixteen"},
				{ 17, "seventeen"},
				{ 18, "eighteen"},
				{ 19, "nineteen"}
			};

			Hashtable tens = new Hashtable {
				{ 2, "twenty"},
				{ 3, "thirty"},
				{ 4, "fourty"},
				{ 5, "fifty"},
				{ 6, "sixty"},
				{ 7, "seventy"},
				{ 8, "eighty"},
				{ 9, "ninty"}
			};

			Hashtable thousands = new Hashtable {
				{ 1, "thousand"},
				{ 2, "million"},
				{ 3, "billion"},
			};

			if (number < 0)
				stringBuilder.Append ("minus ");

			int absoluteValue = Math.Abs(number);

			while (absoluteValue != 0) 
			{
				currentNumber = absoluteValue % 1000;
				dividedByThousand.Add (currentNumber);
				absoluteValue = (absoluteValue - currentNumber) / 1000;
			}

			for (int i = dividedByThousand.Count-1; i >= 0; i--) 
			{
				StringBuilder currentString = new StringBuilder ();
				currentNumber = (int)dividedByThousand[i];
				hundred = currentNumber / 100; currentNumber %= 100;

				if (hundred != 0)
					currentString.Append (ones[hundred]  + " hundred");

				if (teens.ContainsKey (currentNumber)) 
					currentString.Append (" " + teens [currentNumber]);

				else {
					ten = currentNumber / 10; currentNumber %= 10; 
					if (ten != 0) 
						currentString.Append (" " + tens [ten]);

					one = currentNumber;
					if (one != 0)
						currentString.Append (" " + ones [one]);	
				}

				stringBuilder.Append (currentString);

				if (i != 0) 
					stringBuilder = stringBuilder.Append (" " + thousands[i] + ", ");

			}
			return stringBuilder;
		}

		private static long SumNaive(long boundary, int divider)
		{
			long sum = 0;
			for (int i = 0; i <= boundary / divider; i++) 
				sum += i;
			return sum * divider;
		}

		private static long SumRevised(long boundary, int divider)
		{
			long sum = 0;
			long count = boundary / divider + 1;
		 	long lastNumber = boundary - boundary % divider;
			sum = lastNumber * count / 2;
			return sum;
		}

		private static void ShowResult(long boundary, Func<long, int, long> funcName)
		{
			long sum = 0;
			Func<long, int, long> func = funcName;
			DateTime start = new DateTime();
			DateTime end = new DateTime();
			start = DateTime.Now;
			sum = func (boundary, 3) + func (boundary, 5) - func (boundary, 15);
			end = DateTime.Now;
			Console.WriteLine ("   {0} Time: {1} milliseconds ({2})", sum, (end - start).TotalMilliseconds,funcName.Method.Name);

		}

		private static void ShowResult(long boundary)
		{
			long sum = 0;
			DateTime start = new DateTime();
			DateTime end = new DateTime();
			start = DateTime.Now;
			for (int i = 0; i <= boundary; i++) {
				if (i % 3 ==0 || i % 5 ==0)
					sum += i;
			}
			end = DateTime.Now;
			Console.WriteLine ("   {0} Time: {1} milliseconds ({2})", sum, (end - start).TotalMilliseconds,"Original");
		}

		public static void Main (string[] args)
		{
			Console.WriteLine("Please type in a number. (-2147483648 ~ 2147483647)");
			var userInput = Console.ReadLine();
			int number;
			while (!Int32.TryParse (userInput, out number)) {
				Console.WriteLine("You did not type in a number bewteen -2147483648 and 2147483647 , please try again.");
				userInput = Console.ReadLine();
			}

			//1.Check for Odd or Even
			Console.WriteLine("1. Number {0} is {1}", number, DetermineOddEven(number));

			//2.Check for divisibility by 3 or 5 
			Console.Write("2. Number {0} is ", number);
			Boolean dividable = DetermineDivisibility(number);

			//3.Change numbers
			if (!dividable) 
				Console.WriteLine ("3. Indivisible, with Liberty and Justice for All");
			else 
				Console.WriteLine ("3. Number {0} is written as \"{1}\"", number, ChangeNumbers(number));

			//4.Find the sum of all the multiples of 3 or 5 below 100000000
			Console.WriteLine ("4. Sum of all the multiples of 3 or 5 below 100000000 is");

			long boundary = 100000000;
			//Time Complexity: O(N)
			ShowResult (boundary); 
			//Time Complexity: O(N)
			ShowResult (boundary, SumNaive);
			//Time Complexity: O(1)
			ShowResult (boundary, SumRevised);

		}
	}
}