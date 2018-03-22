using DungeonsAndCodeWizards.AbstractClasses;
using DungeonsAndCodeWizards.BagClasses;
using System;
using System.Linq;

namespace DungeonsAndCodeWizards
{
	public class StartUp
	{
		// DO NOT rename this file's namespace or class name.
		// However, you ARE allowed to use your own namespaces (or no namespaces at all if you prefer) in other classes.
		public static void Main(string[] args)
		{
            DungeonMaster dungeon = new DungeonMaster();
            bool isGameOver = false;
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == null || cmd == string.Empty || String.IsNullOrWhiteSpace(cmd))
                {
                    break;
                }
                else
                {
                    string[] command = cmd.Split();
                    try
                    {
                        switch (command[0])
                        {
                            case "JoinParty":
                                string[] input = new string[command.Length - 1];
                                for (int i = 1; i < command.Length; i++)
                                {
                                    input[i - 1] = command[i];
                                }
                                Console.WriteLine(dungeon.JoinParty(input));
                                break;
                            case "AddItemToPool":
                                string[] input1 = new string[1];
                                input1[0] = command[1];
                                Console.WriteLine(dungeon.AddItemToPool(input1));
                                break;
                            case "PickUpItem":
                                string[] input2 = new string[1];
                                input2[0] = command[1];
                                Console.WriteLine(dungeon.PickUpItem(input2));
                                break;
                            case "UseItem":
                                string[] input3 = new string[2];
                                input3[0] = command[1];
                                input3[1] = command[2];
                                Console.WriteLine(dungeon.UseItem(input3));
                                break;
                            case "UseItemOn":
                                string[] input4 = new string[3];
                                input4[0] = command[1];
                                input4[1] = command[2];
                                input4[2] = command[3];
                                Console.WriteLine(dungeon.UseItemOn(input4));
                                break;
                            case "GiveCharacterItem":
                                string[] input5 = new string[3];
                                input5[0] = command[1];
                                input5[1] = command[2];
                                input5[2] = command[3];
                                Console.WriteLine(dungeon.GiveCharacterItem(input5));
                                break;
                            case "GetStats":
                                Console.WriteLine(dungeon.GetStats());
                                break;
                            case "Attack":
                                string[] input6 = new string[2];
                                input6[0] = command[1];
                                input6[1] = command[2];
                                Console.WriteLine(dungeon.Attack(input6));
                                break;
                            case "Heal":
                                string[] input7 = new string[2];
                                input7[0] = command[1];
                                input7[1] = command[2];
                                Console.WriteLine(dungeon.Heal(input7));
                                break;
                            case "EndTurn":
                                Console.WriteLine(dungeon.EndTurn(new string[0]));
                                break;
                            case "IsGameOver":
                                isGameOver = dungeon.IsGameOver();
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        if (isGameOver)
                        {
                            break;
                        }
                    }
                    catch (ArgumentException ArgEx)
                    {
                        Console.WriteLine("Parameter Error: " + ArgEx.Message);
                    }
                    catch (InvalidOperationException OpEx)
                    {
                        Console.WriteLine("Invalid Operation: " + OpEx.Message);
                    }
                }
            }
            Console.WriteLine("Final stats:");
            Console.WriteLine(dungeon.GetStats());
        }
	}
}