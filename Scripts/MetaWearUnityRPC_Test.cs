﻿using UnityEngine;

namespace MetaWearRPC.Unity
{
	public sealed class MetaWearUnityRPC_Test : MetaWearUnityRPC
	{
		/// <summary>
		/// The list of reachable MetaWear boards.
		/// </summary>
		public string[] boardsMac = new string[]
		{
			"F6:E9:DD:B4:CF:4A",
			"D2:80:93:BC:8C:FD",
			"DF:16:4D:D1:5D:58",
			"C2:48:ED:96:3B:74"
		};

		/// <summary>
		/// The board that currently receive commands.
		/// </summary>
		[Range(0, 3)]
		public int currentBoardIndex = 0;
		[Range(0.0f, 100.0f)]
		public float vibrationIntensity = 100.0f;
		public ushort vibrationBuzzDurationMs = 200;
		public ushort vibrationPatternSleepDurationMs = 110;
		public int vibrationPatternIterations = 5;

		protected override void Update()
		{
			base.Update();

			string boardStr = boardsMac[currentBoardIndex];
			ulong board = Global.MacFromString(boardStr);

			if (Input.GetKeyUp(KeyCode.Keypad0))
			{
				string model = Client.GetBoardModel(board);
				Debug.Log("[MetaWearUnityRPC_Test] Board " + boardStr + " model is : " + model);
			}
			else if (Input.GetKeyUp(KeyCode.Keypad1))
			{
				byte batLevel = Client.GetBatteryLevel(board);
				Debug.Log("[MetaWearUnityRPC_Test] Board " + boardStr + " battery left : " + batLevel);
			}
			else if(Input.GetKeyUp(KeyCode.Keypad2))
			{
				Client.StartBuzzer(board, vibrationBuzzDurationMs);
				Debug.Log("[MetaWearUnityRPC_Test] Board " + boardStr + " buzzering " + vibrationBuzzDurationMs + " ms");
			}
			else if (Input.GetKeyUp(KeyCode.Keypad4))
			{
				Client.StartMotor(board, vibrationBuzzDurationMs, vibrationIntensity);
				Debug.Log("[MetaWearUnityRPC_Test] Board " + boardStr + " vibrating " + vibrationBuzzDurationMs + " ms");
			}
			else if (Input.GetKeyUp(KeyCode.Keypad5))
			{
				Client.StartMotorPattern(board, vibrationBuzzDurationMs, vibrationIntensity, vibrationPatternSleepDurationMs, vibrationPatternIterations);
				Debug.Log("[MetaWearUnityRPC_Test] Board " + boardStr + " vibrating " + vibrationPatternIterations + " times");
			}

		}
	}
}