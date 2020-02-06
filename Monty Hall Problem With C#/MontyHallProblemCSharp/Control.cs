﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MontyGame
{
    Random random = new Random();

    /// <summary>
    /// When called will return bool[] with all set false
    /// except 1 randomly chosen door which will be set true
    /// </summary>
    /// <returns> bool[] of doors</returns>
    private bool[] StartRandomDoorPositions()
    {
        int randomInt = random.Next(0, 2);
        bool[] arrayOfDoors = new bool[3] { false, false, false };
        arrayOfDoors[randomInt] = true;
        return arrayOfDoors;
    }

    private int PlayerGuess()
    {
        int potentialPlayerGuess;
        string playerInput;
        int playerGuess;
        while (true)
        {
            Console.WriteLine("Please pick a door.... 1 | 2 | 3 |");
            playerInput = Console.ReadLine();

            if (int.TryParse(playerInput, out potentialPlayerGuess))
            {
                if (3 >= potentialPlayerGuess && potentialPlayerGuess >= 1)
                {
                    playerGuess = potentialPlayerGuess - 1;
                    return playerGuess;
                }
                else
                {
                    Console.WriteLine("That is not a door, please enter 1, 2, or 3...");
                }
            }
            else
            {
                Console.WriteLine("That was not a number, please enter a number...");
            }
        }
    }

    private int MontyRevealedDoor(bool[] doors, int playerGuess)
    {
        int montyDoor = 0;
        for (int i = 0; i < doors.Length; i++)
        {
            if (playerGuess == i)
            {
                continue;
            }
            else if (!doors[i])
            {
                montyDoor = i;
                break;
            }
        }
        return montyDoor;
    }

    private int SwitchRequest(bool[] doors, int montysRevealedDoor, int oldGuess)
    {

        char parseResult;
        string playerSwapResponse;
        bool playerWantsToSwap;
        int newGuess = 0;

        Console.WriteLine(string.Format("you have chosen door {0}, Monty has revealed that door {1} is a  Goat", oldGuess + 1, montysRevealedDoor + 1));

        while (true)
        {
            Console.WriteLine("Would you like to swap doors? y/n");
            playerSwapResponse = Console.ReadLine().ToLower();

            if (char.TryParse(playerSwapResponse, out parseResult))
            {
                if (parseResult == 'y' || parseResult == 'n')
                {
                    if (parseResult == 'y')
                    {
                        playerWantsToSwap = true;
                    }
                    else
                    {
                        playerWantsToSwap = false;
                    }
                    break;

                }
                else
                {
                    Console.WriteLine("Please enter y or n for your decision");
                }
            }
            else
            {
                Console.WriteLine("Please enter y or n for your decision");
            }

        }
        if (playerWantsToSwap)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (i == montysRevealedDoor)
                {
                    continue;
                }
                else if (i == oldGuess)
                {
                    continue;
                }
                else
                {
                    newGuess = i;
                }
            }
        }
        else
        {
            newGuess = oldGuess;
        }
        return newGuess;
    }

    private string ResultPrint(bool[] doors, int newGuess)
    {
        string result = "";
        if (doors[newGuess])
        {
            result = " New Car!!";
        }
        else
        {
            result = " Old Goat ";
        }

        return string.Format("you got the {0}", result);
    }
    private bool Result(bool[] doors, int newGuess)
    {
        return doors[newGuess];
    }

    public void Play()
    {
        var doors = StartRandomDoorPositions();
        var oldGuess = PlayerGuess();
        var montyDoors = MontyRevealedDoor(doors, oldGuess);
        var newGuess = SwitchRequest(doors, montyDoors, oldGuess);
        var result = ResultPrint(doors, newGuess);
        Console.WriteLine(result);
        Console.Read();
    }

    public void Simulate()
    {
        int switchWinCount = 0;
        int noSwitchWinCount = 0;
        string requestedCyclesIntput = "";
        int requestedCycles = 1000;

        for (int cycles = 1; cycles < requestedCycles + 1; cycles++)
        {
            var doorsNoSwitch = StartRandomDoorPositions();
            var guessNoSwitch = random.Next(0,3);
            float noSwitchWinPercentage;

            if (Result(doorsNoSwitch,guessNoSwitch))
            {
                noSwitchWinCount++;
            }

            noSwitchWinPercentage = 100 * (noSwitchWinCount / cycles);

            if (cycles % (requestedCycles/10) == 0)
            {
                Console.WriteLine(String.Format("After {0} cycles, by always staying you win: {1} percent of the time", cycles, noSwitchWinPercentage));
            }

            var doorsSwitch = StartRandomDoorPositions();
            var guessSwitch = random.Next(0, 3);
            var revealedDoor = MontyRevealedDoor(doorsSwitch, guessSwitch);
            int newGuess;
            float switchWinPercentage;

            if (true)
            {
                for (int i = 1; i < doorsSwitch.Length + 1; i++)
                {
                    if (i == revealedDoor)
                    {
                        continue;
                    }
                    else if (i == guessSwitch)
                    {
                        continue;
                    }
                    else
                    {
                        newGuess = i;
                    }
                }
            }

            if (Result(doorsSwitch,guessSwitch))
            {
                switchWinCount++;
            }
            switchWinPercentage = 100 * (switchWinCount / cycles);
            if (cycles % (requestedCycles / 10) == 0)
            {
                Console.WriteLine(String.Format("After {0} cycles, by always switching you win: {1} percent of the time", cycles, switchWinPercentage));
            }
            
        }
}

    public void ChooseMode()
    {
        string playerModeResponse;
        char parseResult;

        while (true)
        {
            Console.WriteLine("Would you like play or simulate? p/s");
            playerModeResponse = Console.ReadLine().ToLower();

            if (char.TryParse(playerModeResponse, out parseResult))
            {
                if (parseResult == 's' || parseResult == 'p')
                {
                    if (parseResult == 'p')
                    {
                        Play();
                    }
                    else
                    {
                        Simulate();
                        Console.Read();
                    }
                    break;

                }
                else
                {
                    Console.WriteLine("Would you like play or simulate? p/s");
                }
            }
            else
            {
                Console.WriteLine("Would you like play or simulate? p/s");
            }
        }
    }
}

