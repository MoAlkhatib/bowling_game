using System;
using System.Collections.Generic;
namespace bowling
{
    class Bolwing_Game
    {
        private static void Current_Game_Pins()
        {
            Console.WriteLine();
            Console.WriteLine("Current pins:\n");
            for (int points = 7; points <= 10; points++)
            {
                Console.Write(GameStandingPins.Contains(points) ? "O" : " ");
                Console.Write("   ");
            }
            Console.Write("\n\n  ");
            for (int pinsInGame = 4; pinsInGame <= 6; pinsInGame++)
            {
                Console.Write(GameStandingPins.Contains(pinsInGame) ? "O" : " ");
                Console.Write("   ");
            }
            Console.Write("\n\n    ");
            for (int Gamepin = 2; Gamepin <= 3; Gamepin++)
            {
                Console.Write(GameStandingPins.Contains(Gamepin) ? "O" : " ");
                Console.Write("   ");
            }
            Console.Write("\n\n      ");
            Console.WriteLine(GameStandingPins.Contains(1) ? "O" : " ");
            Console.WriteLine();
        }
        static void restore_Game_Pins()
        {
            bool Game_Pins_reset = false;

            if (Game_Roll == 1) Game_Pins_reset = true;
            if (Game_Frame == 10)
            {
                if (Game_Roll == 2 && Game_PinsCount[9][0] == 10) Game_Pins_reset = true;
                if (Game_Roll == 3 && (Game_PinsCount[9][1] == 10 || Game_PinsCount[9][0] + Game_PinsCount[9][1] == 10)) Game_Pins_reset = true;
            }
            if (Game_Pins_reset)
            {
                GameStandingPins.Clear();
                for (int i = 1; i <= 10; i++) GameStandingPins.Add(i);
            }
        }
        static void Update_Game_scores()
        {
            for (int frameCurrentPoints = 0; frameCurrentPoints < 10; frameCurrentPoints++)
            {
                int[] GamePins = Game_PinsCount[frameCurrentPoints];
                // Make sure this roll has already happened.
                if (GamePins != null)
                {
                    // Get the first two rolls.
                    int RollOne = GamePins[0];
                    int Rolltwo = -1;
                    if (GamePins.Length > 1) Rolltwo = GamePins[1];
                    if (frameCurrentPoints < 9)
                    {
                        int[] PinsGameCountInNextFrame = Game_PinsCount[frameCurrentPoints + 1];
                        if (RollOne == 10)
                        {
                            if (PinsGameCountInNextFrame != null)
                            {
                                if (PinsGameCountInNextFrame[0] == 10 && frameCurrentPoints < 9)
                                {
                                    int[] NextFrame = Game_PinsCount[frameCurrentPoints + 2];
                                    if (NextFrame != null)
                                    {
                                        Game_Points[frameCurrentPoints] = 20 + NextFrame[0];
                                    }
                                }
                                else
                                {
                                    if (PinsGameCountInNextFrame[1] != -1)
                                    {
                                        Game_Points[frameCurrentPoints] = 10 + PinsGameCountInNextFrame[0] + PinsGameCountInNextFrame[1];
                                    }
                                }
                            }
                        }
                        else if (RollOne + Rolltwo == 10)
                        {
                            if (PinsGameCountInNextFrame != null)
                            {
                                Game_Points[frameCurrentPoints] = 10 + PinsGameCountInNextFrame[0];
                            }
                        }
                        else if (GamePins[1] > -1)
                        {
                            Game_Points[frameCurrentPoints] = GamePins[0] + GamePins[1];
                        }
                    }
                    else
                    {
                        int Three = GamePins[2];
                        if (Rolltwo != -1)
                        {
                            if (RollOne == 10 || RollOne + Rolltwo == 10)
                            {
                                if (Three != -1)
                                {
                                    Game_Points[frameCurrentPoints] = RollOne + Rolltwo + Three;
                                }
                            }
                            else
                            {
                                Game_Points[frameCurrentPoints] = RollOne + Rolltwo;
                            }
                        }
                    }

                }
                if (Game_Points[frameCurrentPoints] != -1)
                {
                    Game_Scores[frameCurrentPoints] = Game_Points[frameCurrentPoints];
                    if (frameCurrentPoints > 0) Game_Scores[frameCurrentPoints] += Game_Scores[frameCurrentPoints - 1];
                }
            }
        }
        static void Game_Scoreboard()
        {

            Console.Write("┌─┬─┬─");
            for (int GameIndex = 1; GameIndex < 10; GameIndex++)
            {
                Console.Write("┬─┬─┬─");
                if (GameIndex == 9) Console.Write("┬─");
            }
            Console.WriteLine("┐");
            for (int Gameframe = 0; Gameframe < 10; Gameframe++)
            {
                string RollOne = " ";
                string RollTwo = " ";
                string RollThree = " ";
                int[] knockedGamePinsCount = Game_PinsCount[Gameframe];
                if (knockedGamePinsCount != null)
                {
                    if (knockedGamePinsCount[0] == 0)
                    {
                        RollOne = "-";
                    }
                    else if (knockedGamePinsCount[0] == 10)
                    {
                        RollOne = "X";
                    }
                    else
                    {
                        RollOne = knockedGamePinsCount[0].ToString();
                    }
                    if (knockedGamePinsCount.Length >= 2)
                    {
                        if (knockedGamePinsCount[1] == 0)
                        {
                            RollTwo = "-";
                        }
                        else if (Gameframe == 9 && knockedGamePinsCount[1] == 10)
                        {
                            RollTwo = "X";
                        }
                        else if (knockedGamePinsCount[0] + knockedGamePinsCount[1] == 10)
                        {
                            RollTwo = "/";
                        }
                        else if (knockedGamePinsCount[1] != -1)
                        {
                            RollTwo = knockedGamePinsCount[1].ToString();
                        }
                    }
                    if (knockedGamePinsCount.Length == 3)
                    {
                        bool FirstTwoaspare = knockedGamePinsCount[0] + knockedGamePinsCount[1] == 10;
                        bool TwoThreeAspare = knockedGamePinsCount[1] + knockedGamePinsCount[2] == 10;
                        if (knockedGamePinsCount[2] == 0)
                        {
                            RollThree = "-";
                        }
                        else if (knockedGamePinsCount[2] == 10 && (knockedGamePinsCount[1] == 10 || FirstTwoaspare))
                        {
                            RollThree = "X";
                        }
                        else if (knockedGamePinsCount[0] == 10 && TwoThreeAspare)
                        {
                            RollThree = "/";
                        }
                        // Make sure the roll even happened (otherwise it will be -1).
                        else if (knockedGamePinsCount[2] != -1)
                        {
                            RollThree = knockedGamePinsCount[2].ToString();
                        }
                    }
                }

                // Write out the 2 symbols (or 3 in the 10th frame).
                Console.Write($"│ │{RollOne}│{RollTwo}");
                if (Gameframe == 9) Console.Write($"│{RollThree}");
            }
            Console.WriteLine("│");

            // Draw the middle row.
            Console.Write("│ └─┴─");
            for (int frameIndex = 1; frameIndex < 10; frameIndex++)
            {
                Console.Write("┤ └─┴─");
                if (frameIndex == 9) Console.Write("┴─");
            }
            Console.WriteLine("┤");

            // Draw the row with scores.
            for (int Gamepoint = 0; Gamepoint < 10; Gamepoint++)
            {
                int subGameScore = Game_Scores[Gamepoint];
                if (subGameScore != -1)
                {
                    Console.Write($"│ {subGameScore,3} ");
                }
                else
                {
                    Console.Write($"│     ");
                }
                if (Gamepoint == 9) Console.Write("  ");
            }
            Console.WriteLine("│");

            // Draw the last row (bottom border).
            Console.Write("└─────");
            for (int subGameinsideIndex = 1; subGameinsideIndex < 10; subGameinsideIndex++)
            {
                Console.Write("┴─────");
                if (subGameinsideIndex == 9) Console.Write("──");
            }
            Console.WriteLine("┘");
        }
        static int GameDirectionpath(int direction)
        {
            int GamePinsTobePalyed = 0;

            if (direction == 1)
            {
                if (GameStandingPins.Contains(7)) GamePinsTobePalyed = 7;
            }
            else if (direction == 2)
            {
                if (GameStandingPins.Contains(4)) GamePinsTobePalyed = 4;
            }
            else if (direction == 3)
            {
                if (GameStandingPins.Contains(2)) GamePinsTobePalyed = 2;
                else if (GameStandingPins.Contains(8)) GamePinsTobePalyed = 8;
            }
            else if (direction == 4)
            {
                if (GameStandingPins.Contains(1)) GamePinsTobePalyed = 1;
                else if (GameStandingPins.Contains(5)) GamePinsTobePalyed = 5;
            }
            else if (direction == 5)
            {
                if (GameStandingPins.Contains(3)) GamePinsTobePalyed = 3;
                else if (GameStandingPins.Contains(9)) GamePinsTobePalyed = 9;
            }
            else if (direction == 6)
            {
                if (GameStandingPins.Contains(6)) GamePinsTobePalyed = 6;
            }
            else if (direction == 7)
            {
                if (GameStandingPins.Contains(10)) GamePinsTobePalyed = 10;
            }

            if (GamePinsTobePalyed == 0) return 0;
            GameStandingPins.Remove(GamePinsTobePalyed);
            int GameplayedPinsCount = 1;
            for (int i = 0; i < 2; i++)
            {
                int TotalAvg = randNumber.Next(100);
                if (TotalAvg < 45)
                {
                    GameplayedPinsCount += GameDirectionpath(direction - 1);
                }
                else if (TotalAvg < 90)
                {
                    GameplayedPinsCount += GameDirectionpath(direction + 1);
                }
                else
                {
                    GameplayedPinsCount += GameDirectionpath(direction);
                }
            }
            return GameplayedPinsCount;
        }

        static Random randNumber = new Random();       
        static int Game_Frame = 1;
        static int Game_Roll = 1;
        static int[][] Game_PinsCount = new int[10][];
        static int[] Game_Points = new int[10];
        static int[] Game_Scores = new int[10];
        static List<int> GameStandingPins = new List<int>();      
        static void Main()
        {
            for (int z = 0; z < 10; z++)
            {
                Game_Points[z] = -1;
                Game_Scores[z] = -1;
            }
            while (Game_Frame <= 10)
            {
                restore_Game_Pins();
                Console.Clear();
                Console.Write("".PadLeft((Game_Frame - 1) * 6));
                Console.WriteLine($"FRAME {Game_Frame}");
                Update_Game_scores();
                Game_Scoreboard();
                Current_Game_Pins();
                int GamerollingFrame = 2;
                if (Game_Frame == 10 && Game_Roll >= 3)
                {
                    bool frameHasAStrike = Game_PinsCount[9][0] == 10;
                    bool frameHasASpare = Game_PinsCount[9][0] + Game_PinsCount[9][1] == 10;
                    if (frameHasASpare || frameHasAStrike)
                    {
                        GamerollingFrame = 3;
                    }
                }
                if (Game_Roll <= GamerollingFrame)
                {
                    Console.WriteLine("1 2 3 4 5 6 7");
                    Console.WriteLine();
                    Console.Write("Enter where to roll the ball (1-7): ");
                    int Direction = 0;
                    while (Direction == 0)
                    {
                        try
                        {
                            Direction = Int32.Parse(Console.ReadLine());
                        }
                        catch { }
                    }
                    int currentRoll = GameDirectionpath(Direction);
                    if (Game_Roll == 1)
                    {
                        if (Game_Frame == 10)
                        {
                            Game_PinsCount[Game_Frame - 1] = new int[] { currentRoll, -1, -1 };
                        }
                        else
                        {
                            if (currentRoll == 10)
                            {
                                Game_PinsCount[Game_Frame - 1] = new int[] { 10 };
                            }
                            else
                            {
                                Game_PinsCount[Game_Frame - 1] = new int[] { currentRoll, -1 };
                            }
                        }
                    }
                    else
                    {
                        Game_PinsCount[Game_Frame - 1][Game_Roll - 1] = currentRoll;
                    }
                    Game_Roll++;
                    if (Game_Frame < 10 && GameStandingPins.Count == 0)
                    {
                        Game_Roll = 3;
                    }
                }
                else
                {
                    if (Game_Frame < 10)
                    {
                        Console.WriteLine("Press Enter to continue.");
                        Game_Roll = 1;
                    }
                    else
                    {
                        Console.WriteLine($"Game over! Your score was {Game_Scores[9]}.");
                    }
                    Console.ReadLine();
                    Game_Frame++;
                }
            }
        }
    }
}